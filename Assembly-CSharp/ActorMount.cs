using System;
using UnityEngine;
using UnityEngine.Events;

public class ActorMount : MonoBehaviour, ICustomPlayerMovement, ITransitionReciever
{
	// (get) Token: 0x06000312 RID: 786 RVA: 0x00011E12 File Offset: 0x00010012
	public bool isMounted
	{
		get
		{
			return this.gettingIntoPosition && this.lerp == 1f;
		}
	}

	// (get) Token: 0x06000313 RID: 787 RVA: 0x00011E2B File Offset: 0x0001002B
	public bool isFilled
	{
		get
		{
			return this.actor != null || (this.filledObject != null && this.filledObject.activeInHierarchy);
		}
	}

	// Token: 0x06000314 RID: 788 RVA: 0x00011E58 File Offset: 0x00010058
	public void StartTransition(float duration)
	{
		this.lerp = 0f;
		this.transitionSpeed = 1f / duration;
	}

	// Token: 0x06000315 RID: 789 RVA: 0x00011E74 File Offset: 0x00010074
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

	// Token: 0x06000316 RID: 790 RVA: 0x00011ED4 File Offset: 0x000100D4
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

	// Token: 0x06000317 RID: 791 RVA: 0x00011F64 File Offset: 0x00010164
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

	// Token: 0x06000318 RID: 792 RVA: 0x00011FCC File Offset: 0x000101CC
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

	// Token: 0x06000319 RID: 793 RVA: 0x000120F0 File Offset: 0x000102F0
	private Vector3 GetPosition(Transform target, float lerp)
	{
		float num = Mathf.Abs(lerp - 0.5f) * 2f;
		float num2 = 1f - num * num;
		return Vector3.Lerp(this.initialPosition, target.position, lerp) + 0.5f * num2 * Vector3.up;
	}

	// Token: 0x0600031A RID: 794 RVA: 0x00012144 File Offset: 0x00010344
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

	// Token: 0x0600031B RID: 795 RVA: 0x00012290 File Offset: 0x00010490
	private void GetIntoMount()
	{
		this.GetIntoMount(false);
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0001229C File Offset: 0x0001049C
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

	// Token: 0x0600031D RID: 797 RVA: 0x00012518 File Offset: 0x00010718
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

	// Token: 0x0600031E RID: 798 RVA: 0x00012588 File Offset: 0x00010788
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

	// Token: 0x0600031F RID: 799 RVA: 0x0001260C File Offset: 0x0001080C
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
		Vector2 vector = new Vector2(Player.movement.velocity.x, Player.movement.velocity.z);
		vector = Vector2.ClampMagnitude(vector, 7f);
		Player.movement.velocity.x = vector.x;
		Player.movement.velocity.z = vector.y;
		this.gettingIntoPosition = false;
	}

	// Token: 0x06000320 RID: 800 RVA: 0x000126EC File Offset: 0x000108EC
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

	public ActorPosition position = ActorPosition.P_Seated;

	public Transform mountAnchor;

	public Transform standingAnchor;

	private bool hasSnappedStandingAnchor;

	public GameObject filledObject;

	[ReadOnly]
	public DialogueActor actor;

	private MountedActor mountedActor;

	private AnimationEvents actorEvents;

	private Vector3 lastPosition;

	private Vector3 lastLastPosition;

	private Vector3 initialPosition;

	private Quaternion initialRotation;

	private bool gettingIntoPosition;

	private float lerp;

	private const float transitionTime = 0.4f;

	private float transitionSpeed;

	[HideInInspector]
	public UnityEvent onGottenInto;

	[HideInInspector]
	public UnityEvent onGottenOut;

	public UnityEvent onMount;

	public UnityEvent onDismount;

	private float movementCounter;

	public AnimationSet animationSet;

	public bool clearEquippedState = true;

	private static readonly int TransitionID = Animator.StringToHash("Transition");
}
