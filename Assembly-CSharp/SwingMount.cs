using System;
using UnityEngine;
using UnityEngine.Events;

public class SwingMount : ActorMount
{
	// Token: 0x0600054D RID: 1357 RVA: 0x0002E3F4 File Offset: 0x0002C5F4
	protected override void HandlePlayerInput(Vector3 input, ref float animationIndex)
	{
		if (!Game.HasControl)
		{
			animationIndex = -1f;
			return;
		}
		float num = base.transform.InverseTransformDirection(input).z;
		num = Mathf.Clamp(Mathf.Abs(num) * num * 2f, -1f, 1f);
		this.smoothDirectionality = Mathf.SmoothDamp(this.smoothDirectionality, num, ref this.directionalityVelocity, 0.25f);
		animationIndex = -10f + this.smoothDirectionality;
		this.swing.AddForce(this.swingForce * input);
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0002E484 File Offset: 0x0002C684
	protected override void GetIntoMount(bool skipToMount)
	{
		base.GetIntoMount(skipToMount);
		this.smoothDirectionality = 0f;
		if (this.actor.isPlayer)
		{
			Player.movement.modPrimaryRule = PlayerMovement.ModRule.Locked;
			Player.movement.modLockAnimatorSpeed = true;
			Player.itemManager.SetEquippedState(PlayerItemManager.EquippedState.None, false);
		}
		this.handIK = this.actor.animator.GetComponent<ActorHandIK>();
		if (this.handIK == null)
		{
			this.handIK = this.actor.animator.gameObject.AddComponent<ActorHandIK>();
		}
		float magnitude = (this.actor.head.position - this.actor.lowerSpine.position).magnitude;
		float num = magnitude / 0.364f;
		this.handIK.SetHandIK(false, new Vector3(this.swingWidth, magnitude, -0.1f), base.transform, true);
		this.handIK.SetHandIK(true, new Vector3(-this.swingWidth, magnitude, -0.1f), base.transform, true);
		if (skipToMount)
		{
			this.OnGetIntoSwing();
			return;
		}
		this.onGottenInto.AddListener(new UnityAction(this.OnGetIntoSwing));
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0002E5B4 File Offset: 0x0002C7B4
	private void OnGetIntoSwing()
	{
		if (!this.ignoreSwingPhysics)
		{
			this.swing.hasActor = true;
			if (this.actor.isPlayer)
			{
				this.swing.hasPlayer = true;
			}
			this.swing.AddForce(-80f * base.transform.forward);
		}
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0002E610 File Offset: 0x0002C810
	public override void GetOut()
	{
		base.GetOut();
		if (!this.ignoreSwingPhysics)
		{
			this.swing.hasPlayer = false;
			this.swing.hasActor = false;
			this.swing.AddForce(-80f * base.transform.forward);
		}
		if (this.handIK != null)
		{
			this.handIK.ClearAndRemove();
			this.handIK = null;
		}
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x0002E684 File Offset: 0x0002C884
	public override void Cancel()
	{
		if (this == null || this.swing == null)
		{
			return;
		}
		if (!this.ignoreSwingPhysics)
		{
			this.swing.hasPlayer = false;
			this.swing.hasActor = false;
			this.swing.AddForce(-80f * base.transform.forward);
		}
		if (this.handIK != null)
		{
			this.handIK.ClearAndRemove();
			this.handIK = null;
		}
		base.Cancel();
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x0002E710 File Offset: 0x0002C910
	public override void CancelMount()
	{
		if (!this.actor.isPlayer && !this.ignoreSwingPhysics)
		{
			this.swing.hasPlayer = false;
			this.swing.hasActor = false;
			this.swing.AddForce(-80f * base.transform.forward);
		}
		if (this.handIK != null)
		{
			this.handIK.ClearAndRemove();
			this.handIK = null;
		}
		base.CancelMount();
	}

	[Space]
	public HitSwing swing;

	public float swingForce = 50f;

	public float swingWidth = 0.4f;

	private ActorHandIK handIK;

	public bool ignoreSwingPhysics;

	private float smoothDirectionality;

	private float directionalityVelocity;
}
