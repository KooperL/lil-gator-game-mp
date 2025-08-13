using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000CE RID: 206
public class ActorMount : MonoBehaviour, ICustomPlayerMovement, ITransitionReciever
{
	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06000357 RID: 855 RVA: 0x00004952 File Offset: 0x00002B52
	public bool isMounted
	{
		get
		{
			return this.gettingIntoPosition && this.lerp == 1f;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000358 RID: 856 RVA: 0x0000496B File Offset: 0x00002B6B
	public bool isFilled
	{
		get
		{
			return this.actor != null || (this.filledObject != null && this.filledObject.activeInHierarchy);
		}
	}

	// Token: 0x06000359 RID: 857 RVA: 0x00004998 File Offset: 0x00002B98
	public void StartTransition(float duration)
	{
		this.lerp = 0f;
		this.transitionSpeed = 1f / duration;
	}

	// Token: 0x0600035A RID: 858 RVA: 0x000252A8 File Offset: 0x000234A8
	private void Update()
	{
		if (this.mountedActor == null)
		{
			base.enabled = false;
			return;
		}
		Vector3 vector;
		Quaternion quaternion;
		Vector3 vector2;
		this.UpdateTransform(out vector, out quaternion, out vector2);
		if (this.actor != null)
		{
			this.actor.transform.position = vector;
			this.actor.transform.rotation = quaternion;
		}
	}

	// Token: 0x0600035B RID: 859 RVA: 0x00025308 File Offset: 0x00023508
	public virtual void MovementUpdate(Vector3 input, ref Vector3 position, ref Vector3 velocity, ref Vector3 direction, ref Vector3 up, ref float animationIndex)
	{
		Vector3 vector;
		Quaternion quaternion;
		Vector3 vector2;
		this.UpdateTransform(out vector, out quaternion, out vector2);
		position = vector;
		direction = quaternion * Vector3.forward;
		up = quaternion * Vector3.up;
		velocity = 0.5f * (position - this.lastLastPosition) / Time.fixedDeltaTime;
		this.lastLastPosition = this.lastPosition;
		this.lastPosition = position;
		this.HandlePlayerInput(input, ref animationIndex);
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00025398 File Offset: 0x00023598
	protected virtual void HandlePlayerInput(Vector3 input, ref float animationIndex)
	{
		animationIndex = -1f;
		if (this.gettingIntoPosition && this.lerp >= 1f && input.sqrMagnitude >= 0.95f)
		{
			this.movementCounter += Time.deltaTime;
			if (this.movementCounter > 0.25f)
			{
				this.GetOut();
				return;
			}
		}
		else
		{
			this.movementCounter = 0f;
		}
	}

	// Token: 0x0600035D RID: 861 RVA: 0x00025400 File Offset: 0x00023600
	private void UpdateTransform(out Vector3 position, out Quaternion rotation, out Vector3 nextPosition)
	{
		Transform transform = (this.gettingIntoPosition ? this.mountAnchor : this.standingAnchor);
		if (this.lerp < 1f)
		{
			float num = this.lerp;
			this.lerp = Mathf.MoveTowards(this.lerp, 1f, this.transitionSpeed * Time.deltaTime);
			float num2 = Mathf.Clamp01(this.lerp + this.lerp - num);
			position = this.GetPosition(transform, this.lerp);
			nextPosition = this.GetPosition(transform, num2);
			rotation = MathUtils.SlerpFlat(this.initialRotation, transform.rotation, this.lerp);
			if (this.lerp == 1f)
			{
				this.transitionSpeed = 0f;
				if (this.gettingIntoPosition)
				{
					this.onMount.Invoke();
					if (this.onGottenInto != null)
					{
						this.onGottenInto.Invoke();
					}
					this.onGottenInto = new UnityEvent();
					return;
				}
				this.GottenOut();
				return;
			}
		}
		else
		{
			nextPosition = (position = transform.position);
			rotation = transform.rotation;
		}
	}

	// Token: 0x0600035E RID: 862 RVA: 0x00025524 File Offset: 0x00023724
	private Vector3 GetPosition(Transform target, float lerp)
	{
		float num = Mathf.Abs(lerp - 0.5f) * 2f;
		float num2 = 1f - num * num;
		return Vector3.Lerp(this.initialPosition, target.position, lerp) + 0.5f * num2 * Vector3.up;
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00025578 File Offset: 0x00023778
	public void InviteActor(DialogueActor actor, bool skipToMount = false, bool skipWalk = false)
	{
		if (this.isFilled)
		{
			return;
		}
		if (!this.hasSnappedStandingAnchor)
		{
			LayerUtil.SnapToGround(this.standingAnchor, 5f);
			this.hasSnappedStandingAnchor = true;
		}
		this.actor = actor;
		IActorMover actorMover = actor.GetComponent<IActorMover>();
		if (actorMover != null)
		{
			actorMover.CancelMove();
		}
		if (skipWalk || skipToMount || Vector3.SqrMagnitude(actor.transform.position - this.mountAnchor.position) < 1f || Vector3.SqrMagnitude(actor.transform.position - this.standingAnchor.position) < 1f)
		{
			if (skipToMount)
			{
				MountedActor component = actor.GetComponent<MountedActor>();
				if (component != null)
				{
					component.CancelMount();
				}
				if (actor.isPlayer)
				{
					Player.movement.ApplyTransform(this.standingAnchor);
				}
				else
				{
					actor.transform.ApplyTransform(this.standingAnchor);
				}
			}
			this.GetIntoMount(skipToMount);
			return;
		}
		if (actorMover == null)
		{
			actorMover = actor.gameObject.AddComponent<ActorMover>();
		}
		UnityEvent unityEvent = new UnityEvent();
		unityEvent.AddListener(new UnityAction(this.GetIntoMount));
		actorMover.SetMark(new Vector3[] { this.standingAnchor.position }, this.standingAnchor.rotation, 0f, unityEvent, false, true, true);
		base.enabled = false;
	}

	// Token: 0x06000360 RID: 864 RVA: 0x000049B2 File Offset: 0x00002BB2
	private void GetIntoMount()
	{
		this.GetIntoMount(false);
	}

	// Token: 0x06000361 RID: 865 RVA: 0x000256C4 File Offset: 0x000238C4
	protected virtual void GetIntoMount(bool skipToMount)
	{
		this.mountedActor = this.actor.gameObject.AddComponent<MountedActor>();
		this.mountedActor.mount = this;
		this.actorEvents = this.actor.animator.GetComponent<AnimationEvents>();
		if (this.actorEvents == null)
		{
			this.actorEvents = this.actor.animator.gameObject.AddComponent<AnimationEvents>();
		}
		this.actorEvents.transitionReciever = this;
		this.transitionSpeed = 0f;
		this.actor.SetStateAndPosition(-1, (int)this.position, skipToMount, skipToMount);
		if (skipToMount)
		{
			this.lerp = 1f;
		}
		else
		{
			this.lerp = 0f;
		}
		this.gettingIntoPosition = true;
		if (this.actor.isPlayer)
		{
			Player.movement.ForceModdedState();
			Player.movement.isModified = true;
			Player.movement.modIgnoreReady = true;
			Player.movement.modNoInteractions = true;
			Player.movement.moddedWithoutControl = true;
			Player.movement.modNoMovement = true;
			Player.movement.modCustomMovement = true;
			Player.movement.modCustomMovementScript = this;
			Player.movement.modDisableCollision = true;
			Player.movement.modNoClimbing = true;
			Player.movement.modJumpRule = PlayerMovement.ModRule.Cancels;
			Player.movement.modGlideRule = PlayerMovement.ModRule.Locked;
			Player.movement.modSecondaryRule = PlayerMovement.ModRule.Locked;
			Player.movement.modPrimaryRule = PlayerMovement.ModRule.Allowed;
			Player.movement.modItemRule = PlayerMovement.ModRule.Cancels;
			Player.movement.modIgnoreGroundedness = true;
			Player.movement.modLockAnimatorSpeed = true;
			if (this.animationSet != null)
			{
				Player.overrideAnimations.SetContextualAnimations(this.animationSet);
			}
			Player.footsteps.overrideSettings = true;
			Player.footsteps.overrideHasVisualFootsteps = false;
			Player.footsteps.overrideHasFootstepDust = false;
			Player.footIK.overrideIK = true;
			this.movementCounter = 0f;
			this.initialPosition = Player.RawPosition;
			this.lastLastPosition = (this.lastPosition = this.initialPosition);
			this.initialRotation = this.actor.transform.rotation;
			if (this.clearEquippedState)
			{
				Player.itemManager.equippedState = PlayerItemManager.EquippedState.None;
			}
		}
		else
		{
			base.enabled = true;
			this.initialPosition = this.actor.transform.position;
			this.initialRotation = this.actor.transform.rotation;
		}
		if (skipToMount)
		{
			if (this.onGottenInto != null)
			{
				this.onGottenInto.Invoke();
			}
			this.onGottenInto = new UnityEvent();
		}
	}

	// Token: 0x06000362 RID: 866 RVA: 0x00025940 File Offset: 0x00023B40
	public virtual void GetOut()
	{
		if (this.mountedActor == null || !this.gettingIntoPosition)
		{
			return;
		}
		this.actor.Position = 0;
		this.initialPosition = this.actor.transform.position;
		this.initialRotation = this.actor.transform.rotation;
		this.lerp = 0f;
		this.gettingIntoPosition = false;
	}

	// Token: 0x06000363 RID: 867 RVA: 0x000259B0 File Offset: 0x00023BB0
	private void GottenOut()
	{
		if (this.actor.isPlayer)
		{
			Player.movement.ClearMods();
		}
		else
		{
			base.enabled = false;
		}
		if (this.onDismount != null)
		{
			this.onDismount.Invoke();
		}
		if (this.onGottenOut != null)
		{
			this.onGottenOut.Invoke();
		}
		this.onGottenOut = new UnityEvent();
		this.mountedActor.GottenOut();
		Object.Destroy(this.mountedActor);
		this.mountedActor = null;
		this.actor = null;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x00025A34 File Offset: 0x00023C34
	public virtual void Cancel()
	{
		if (this == null)
		{
			return;
		}
		this.actor.SetStateAndPosition(-1, 0, true, false);
		if (this.gettingIntoPosition || this.lerp < 1f)
		{
			this.actor = null;
			Object.Destroy(this.mountedActor);
			this.onGottenOut = new UnityEvent();
			this.onGottenInto = new UnityEvent();
			base.enabled = false;
		}
		Player.footsteps.ClearOverride();
		Player.footIK.ClearOverrides();
		Vector2 vector;
		vector..ctor(Player.movement.velocity.x, Player.movement.velocity.z);
		vector = Vector2.ClampMagnitude(vector, 7f);
		Player.movement.velocity.x = vector.x;
		Player.movement.velocity.z = vector.y;
		this.gettingIntoPosition = false;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00025B14 File Offset: 0x00023D14
	public virtual void CancelMount()
	{
		if (this.actor.isPlayer)
		{
			Player.movement.ClearMods();
			Player.footsteps.ClearOverride();
			Player.footIK.ClearOverrides();
			Player.movement.ClampSpeedForABit();
		}
		else
		{
			this.actor.SetStateAndPosition(-1, 0, true, true);
			this.actor = null;
			Object.Destroy(this.mountedActor);
			this.onGottenOut = new UnityEvent();
			this.onGottenInto = new UnityEvent();
			base.enabled = false;
		}
		this.gettingIntoPosition = false;
	}

	// Token: 0x040004D4 RID: 1236
	public ActorPosition position = ActorPosition.P_Seated;

	// Token: 0x040004D5 RID: 1237
	public Transform mountAnchor;

	// Token: 0x040004D6 RID: 1238
	public Transform standingAnchor;

	// Token: 0x040004D7 RID: 1239
	private bool hasSnappedStandingAnchor;

	// Token: 0x040004D8 RID: 1240
	public GameObject filledObject;

	// Token: 0x040004D9 RID: 1241
	[ReadOnly]
	public DialogueActor actor;

	// Token: 0x040004DA RID: 1242
	private MountedActor mountedActor;

	// Token: 0x040004DB RID: 1243
	private AnimationEvents actorEvents;

	// Token: 0x040004DC RID: 1244
	private Vector3 lastPosition;

	// Token: 0x040004DD RID: 1245
	private Vector3 lastLastPosition;

	// Token: 0x040004DE RID: 1246
	private Vector3 initialPosition;

	// Token: 0x040004DF RID: 1247
	private Quaternion initialRotation;

	// Token: 0x040004E0 RID: 1248
	private bool gettingIntoPosition;

	// Token: 0x040004E1 RID: 1249
	private float lerp;

	// Token: 0x040004E2 RID: 1250
	private const float transitionTime = 0.4f;

	// Token: 0x040004E3 RID: 1251
	private float transitionSpeed;

	// Token: 0x040004E4 RID: 1252
	[HideInInspector]
	public UnityEvent onGottenInto;

	// Token: 0x040004E5 RID: 1253
	[HideInInspector]
	public UnityEvent onGottenOut;

	// Token: 0x040004E6 RID: 1254
	public UnityEvent onMount;

	// Token: 0x040004E7 RID: 1255
	public UnityEvent onDismount;

	// Token: 0x040004E8 RID: 1256
	private float movementCounter;

	// Token: 0x040004E9 RID: 1257
	public AnimationSet animationSet;

	// Token: 0x040004EA RID: 1258
	public bool clearEquippedState = true;

	// Token: 0x040004EB RID: 1259
	private static readonly int TransitionID = Animator.StringToHash("Transition");
}
