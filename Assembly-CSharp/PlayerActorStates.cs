using System;
using UnityEngine;

public class PlayerActorStates : MonoBehaviour
{
	// Token: 0x06000C19 RID: 3097 RVA: 0x0000B497 File Offset: 0x00009697
	private void Awake()
	{
		this.actor = base.GetComponent<DialogueActor>();
		this.animator = base.GetComponent<Animator>();
		this.neutralStateInt = (int)this.neutralState;
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x0000B4BD File Offset: 0x000096BD
	private void Start()
	{
		this.movement = Player.movement;
		this.itemManager = Player.itemManager;
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x0000B4D5 File Offset: 0x000096D5
	private void OnEnable()
	{
		this.actor.State = this.neutralStateInt;
		this.UpdateNextFidgetTime();
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x00042568 File Offset: 0x00040768
	private void FixedUpdate()
	{
		if (Player.movement.modCustomMovement || !Game.HasControl)
		{
			this.UpdateNextFidgetTime();
			this.ClearFidget();
		}
		if (Player.movement.modCustomMovement)
		{
			this.sitDownTimer = 0f;
			this.isSitting = false;
			return;
		}
		if (Game.State == GameState.Dialogue)
		{
			if (this.hadControl)
			{
				this.hadControl = false;
				if (this.isSitting && this.actor.Position == 1)
				{
					this.sitDownTimer = 0f;
					this.isSitting = false;
					this.actor.Position = 0;
				}
				if (this.state == this.actor.State && this.state != this.neutralStateInt)
				{
					this.actor.State = this.neutralStateInt;
					this.neutralStateFallbackTimer = 0f;
				}
			}
			return;
		}
		this.hadControl = true;
		int position = this.actor.Position;
		this.state = this.actor.State;
		if (this.state != this.neutralStateInt)
		{
			this.neutralStateFallbackTimer += Time.deltaTime;
			if (this.neutralStateFallbackTimer >= this.neutralStateFallbackDelay || Game.State != GameState.Play)
			{
				this.actor.State = this.neutralStateInt;
				this.neutralStateFallbackTimer = 0f;
			}
		}
		else
		{
			this.neutralStateFallbackTimer = 0f;
		}
		this.isSitting = position != 0;
		switch (Game.State)
		{
		case GameState.Play:
		{
			bool flag = this.movement.IsGrounded && this.movement.velocity.sqrMagnitude < 0.25f && !this.itemManager.PrimaryInUse && !this.itemManager.SecondaryInUse && !this.itemManager.IsAiming;
			if (this.isSitting)
			{
				if (!flag)
				{
					this.isSitting = false;
				}
			}
			else
			{
				this.isSitting = false;
			}
			break;
		}
		case GameState.Menu:
			if (this.movement.IsGrounded)
			{
				this.isSitting = true;
			}
			break;
		case GameState.ItemMenu:
			this.isSitting = false;
			break;
		}
		if (this.isSitting)
		{
			if (position == 0)
			{
				this.actor.Position = 1;
			}
		}
		else if (position != 0)
		{
			this.actor.SetStateAndPosition(-1, 0, this.movement.JustJumped, false);
		}
		this.UpdateFidgets();
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x000427B4 File Offset: 0x000409B4
	private void UpdateFidgets()
	{
		bool flag = !this.movement.IsClimbing && !this.movement.IsSwimming && !this.movement.isGliding;
		bool flag2 = this.movement.velocity.sqrMagnitude <= 0.1f && !this.isSitting && Player.itemManager.equippedState == PlayerItemManager.EquippedState.None && flag;
		bool isCurrentlyEmoting = this.actor.IsCurrentlyEmoting;
		if (!isCurrentlyEmoting && this.isFidgeting)
		{
			this.ClearFidget();
		}
		if (isCurrentlyEmoting)
		{
			int currentEmoteHash = this.actor.CurrentEmoteHash;
			if (this.allowedEmotes.Contains(currentEmoteHash))
			{
				if (!flag)
				{
					this.actor.ClearEmote(true, false);
				}
			}
			else if (this.fidgets.Contains(currentEmoteHash))
			{
				if (!flag2)
				{
					this.actor.ClearEmote(true, false);
					this.ClearFidget();
				}
			}
			else
			{
				this.actor.ClearEmote(true, false);
			}
			this.UpdateNextFidgetTime();
			return;
		}
		if (!flag2)
		{
			this.UpdateNextFidgetTime();
			return;
		}
		if (Time.time > this.nextFidgetTime)
		{
			int num = global::UnityEngine.Random.Range(0, this.fidgets.Length);
			if (num == this.lastFidgetIndex)
			{
				num = (num + 1) % this.fidgets.Length;
			}
			this.actor.SetEmote(this.fidgets[num], false);
			this.lastFidgetIndex = num;
			this.UpdateNextFidgetTime();
			this.isFidgeting = true;
		}
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x0000B4EE File Offset: 0x000096EE
	private void UpdateNextFidgetTime()
	{
		this.nextFidgetTime = Time.time + global::UnityEngine.Random.Range(8f, 16f);
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x0000B50B File Offset: 0x0000970B
	private void ClearFidget()
	{
		if (!this.isFidgeting)
		{
			return;
		}
		this.isFidgeting = false;
	}

	[HideInInspector]
	public MountedActor mountedActor;

	private DialogueActor actor;

	private Animator animator;

	private PlayerMovement movement;

	private PlayerItemManager itemManager;

	public float sitDownDelay = 15f;

	private float sitDownTimer;

	private bool isSitting;

	private int stateID = Animator.StringToHash("State");

	private int positionID = Animator.StringToHash("Position");

	public ActorState neutralState = ActorState.S_Happy;

	private int neutralStateInt;

	public float neutralStateFallbackDelay = 5f;

	private float neutralStateFallbackTimer;

	private int[] fidgets = new int[]
	{
		Animator.StringToHash("E_FidgetFlail"),
		Animator.StringToHash("E_FidgetYawnStretch"),
		Animator.StringToHash("E_FidgetBalance"),
		Animator.StringToHash("E_FidgetLook")
	};

	private int[] allowedEmotes = new int[]
	{
		0,
		Animator.StringToHash("E_Texting"),
		Animator.StringToHash("E_MegaphoneShout")
	};

	private const float minFidgetDelay = 8f;

	private const float maxFidgetDelay = 16f;

	private float nextFidgetTime = -1f;

	private int lastFidgetIndex = -1;

	[ReadOnly]
	public bool isFidgeting;

	private bool hadControl;

	private int state;
}
