using System;
using UnityEngine;
using UnityEngine.Events;

public class SwingMount : ActorMount
{
	// Token: 0x06000449 RID: 1097 RVA: 0x0001873C File Offset: 0x0001693C
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

	// Token: 0x0600044A RID: 1098 RVA: 0x000187CC File Offset: 0x000169CC
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

	// Token: 0x0600044B RID: 1099 RVA: 0x000188FC File Offset: 0x00016AFC
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

	// Token: 0x0600044C RID: 1100 RVA: 0x00018958 File Offset: 0x00016B58
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

	// Token: 0x0600044D RID: 1101 RVA: 0x000189CC File Offset: 0x00016BCC
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

	// Token: 0x0600044E RID: 1102 RVA: 0x00018A58 File Offset: 0x00016C58
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
