using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200010B RID: 267
public class SwingMount : ActorMount
{
	// Token: 0x06000520 RID: 1312 RVA: 0x0002CE2C File Offset: 0x0002B02C
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

	// Token: 0x06000521 RID: 1313 RVA: 0x0002CEBC File Offset: 0x0002B0BC
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

	// Token: 0x06000522 RID: 1314 RVA: 0x0002CFEC File Offset: 0x0002B1EC
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

	// Token: 0x06000523 RID: 1315 RVA: 0x0002D048 File Offset: 0x0002B248
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

	// Token: 0x06000524 RID: 1316 RVA: 0x0002D0BC File Offset: 0x0002B2BC
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

	// Token: 0x06000525 RID: 1317 RVA: 0x0002D148 File Offset: 0x0002B348
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

	// Token: 0x04000716 RID: 1814
	[Space]
	public HitSwing swing;

	// Token: 0x04000717 RID: 1815
	public float swingForce = 50f;

	// Token: 0x04000718 RID: 1816
	public float swingWidth = 0.4f;

	// Token: 0x04000719 RID: 1817
	private ActorHandIK handIK;

	// Token: 0x0400071A RID: 1818
	public bool ignoreSwingPhysics;

	// Token: 0x0400071B RID: 1819
	private float smoothDirectionality;

	// Token: 0x0400071C RID: 1820
	private float directionalityVelocity;
}
