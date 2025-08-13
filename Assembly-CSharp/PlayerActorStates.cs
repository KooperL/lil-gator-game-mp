using System;
using UnityEngine;

// Token: 0x020001E1 RID: 481
public class PlayerActorStates : MonoBehaviour
{
	// Token: 0x06000A17 RID: 2583 RVA: 0x0002EBCD File Offset: 0x0002CDCD
	private void Awake()
	{
		this.actor = base.GetComponent<DialogueActor>();
		this.animator = base.GetComponent<Animator>();
		this.neutralStateInt = (int)this.neutralState;
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x0002EBF3 File Offset: 0x0002CDF3
	private void Start()
	{
		this.movement = Player.movement;
		this.itemManager = Player.itemManager;
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x0002EC0B File Offset: 0x0002CE0B
	private void OnEnable()
	{
		this.actor.State = this.neutralStateInt;
		this.UpdateNextFidgetTime();
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x0002EC24 File Offset: 0x0002CE24
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

	// Token: 0x06000A1B RID: 2587 RVA: 0x0002EE70 File Offset: 0x0002D070
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

	// Token: 0x06000A1C RID: 2588 RVA: 0x0002EFC3 File Offset: 0x0002D1C3
	private void UpdateNextFidgetTime()
	{
		this.nextFidgetTime = Time.time + Random.Range(8f, 16f);
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x0002EFE0 File Offset: 0x0002D1E0
	private void ClearFidget()
	{
		if (!this.isFidgeting)
		{
			return;
		}
		this.isFidgeting = false;
	}

	// Token: 0x04000C8B RID: 3211
	[HideInInspector]
	public MountedActor mountedActor;

	// Token: 0x04000C8C RID: 3212
	private DialogueActor actor;

	// Token: 0x04000C8D RID: 3213
	private Animator animator;

	// Token: 0x04000C8E RID: 3214
	private PlayerMovement movement;

	// Token: 0x04000C8F RID: 3215
	private PlayerItemManager itemManager;

	// Token: 0x04000C90 RID: 3216
	public float sitDownDelay = 15f;

	// Token: 0x04000C91 RID: 3217
	private float sitDownTimer;

	// Token: 0x04000C92 RID: 3218
	private bool isSitting;

	// Token: 0x04000C93 RID: 3219
	private int stateID = Animator.StringToHash("State");

	// Token: 0x04000C94 RID: 3220
	private int positionID = Animator.StringToHash("Position");

	// Token: 0x04000C95 RID: 3221
	public ActorState neutralState = ActorState.S_Happy;

	// Token: 0x04000C96 RID: 3222
	private int neutralStateInt;

	// Token: 0x04000C97 RID: 3223
	public float neutralStateFallbackDelay = 5f;

	// Token: 0x04000C98 RID: 3224
	private float neutralStateFallbackTimer;

	// Token: 0x04000C99 RID: 3225
	private int[] fidgets = new int[]
	{
		Animator.StringToHash("E_FidgetFlail"),
		Animator.StringToHash("E_FidgetYawnStretch"),
		Animator.StringToHash("E_FidgetBalance"),
		Animator.StringToHash("E_FidgetLook")
	};

	// Token: 0x04000C9A RID: 3226
	private int[] allowedEmotes = new int[]
	{
		0,
		Animator.StringToHash("E_Texting"),
		Animator.StringToHash("E_MegaphoneShout")
	};

	// Token: 0x04000C9B RID: 3227
	private const float minFidgetDelay = 8f;

	// Token: 0x04000C9C RID: 3228
	private const float maxFidgetDelay = 16f;

	// Token: 0x04000C9D RID: 3229
	private float nextFidgetTime = -1f;

	// Token: 0x04000C9E RID: 3230
	private int lastFidgetIndex = -1;

	// Token: 0x04000C9F RID: 3231
	[ReadOnly]
	public bool isFidgeting;

	// Token: 0x04000CA0 RID: 3232
	private bool hadControl;

	// Token: 0x04000CA1 RID: 3233
	private int state;
}
