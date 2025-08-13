using System;
using UnityEngine;

// Token: 0x0200026A RID: 618
public class PlayerActorStates : MonoBehaviour
{
	// Token: 0x06000BCC RID: 3020 RVA: 0x0000B185 File Offset: 0x00009385
	private void Awake()
	{
		this.actor = base.GetComponent<DialogueActor>();
		this.animator = base.GetComponent<Animator>();
		this.neutralStateInt = (int)this.neutralState;
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x0000B1AB File Offset: 0x000093AB
	private void Start()
	{
		this.movement = Player.movement;
		this.itemManager = Player.itemManager;
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x0000B1C3 File Offset: 0x000093C3
	private void OnEnable()
	{
		this.actor.State = this.neutralStateInt;
		this.UpdateNextFidgetTime();
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x00040718 File Offset: 0x0003E918
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

	// Token: 0x06000BD0 RID: 3024 RVA: 0x00040964 File Offset: 0x0003EB64
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
			int num = Random.Range(0, this.fidgets.Length);
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

	// Token: 0x06000BD1 RID: 3025 RVA: 0x0000B1DC File Offset: 0x000093DC
	private void UpdateNextFidgetTime()
	{
		this.nextFidgetTime = Time.time + Random.Range(8f, 16f);
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x0000B1F9 File Offset: 0x000093F9
	private void ClearFidget()
	{
		if (!this.isFidgeting)
		{
			return;
		}
		this.isFidgeting = false;
	}

	// Token: 0x04000EC9 RID: 3785
	[HideInInspector]
	public MountedActor mountedActor;

	// Token: 0x04000ECA RID: 3786
	private DialogueActor actor;

	// Token: 0x04000ECB RID: 3787
	private Animator animator;

	// Token: 0x04000ECC RID: 3788
	private PlayerMovement movement;

	// Token: 0x04000ECD RID: 3789
	private PlayerItemManager itemManager;

	// Token: 0x04000ECE RID: 3790
	public float sitDownDelay = 15f;

	// Token: 0x04000ECF RID: 3791
	private float sitDownTimer;

	// Token: 0x04000ED0 RID: 3792
	private bool isSitting;

	// Token: 0x04000ED1 RID: 3793
	private int stateID = Animator.StringToHash("State");

	// Token: 0x04000ED2 RID: 3794
	private int positionID = Animator.StringToHash("Position");

	// Token: 0x04000ED3 RID: 3795
	public ActorState neutralState = ActorState.S_Happy;

	// Token: 0x04000ED4 RID: 3796
	private int neutralStateInt;

	// Token: 0x04000ED5 RID: 3797
	public float neutralStateFallbackDelay = 5f;

	// Token: 0x04000ED6 RID: 3798
	private float neutralStateFallbackTimer;

	// Token: 0x04000ED7 RID: 3799
	private int[] fidgets = new int[]
	{
		Animator.StringToHash("E_FidgetFlail"),
		Animator.StringToHash("E_FidgetYawnStretch"),
		Animator.StringToHash("E_FidgetBalance"),
		Animator.StringToHash("E_FidgetLook")
	};

	// Token: 0x04000ED8 RID: 3800
	private int[] allowedEmotes = new int[]
	{
		0,
		Animator.StringToHash("E_Texting"),
		Animator.StringToHash("E_MegaphoneShout")
	};

	// Token: 0x04000ED9 RID: 3801
	private const float minFidgetDelay = 8f;

	// Token: 0x04000EDA RID: 3802
	private const float maxFidgetDelay = 16f;

	// Token: 0x04000EDB RID: 3803
	private float nextFidgetTime = -1f;

	// Token: 0x04000EDC RID: 3804
	private int lastFidgetIndex = -1;

	// Token: 0x04000EDD RID: 3805
	[ReadOnly]
	public bool isFidgeting;

	// Token: 0x04000EDE RID: 3806
	private bool hadControl;

	// Token: 0x04000EDF RID: 3807
	private int state;
}
