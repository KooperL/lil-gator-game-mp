using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000106 RID: 262
public class SpringRiderMount : ActorMount
{
	// Token: 0x06000501 RID: 1281 RVA: 0x0002CA34 File Offset: 0x0002AC34
	protected override void HandlePlayerInput(Vector3 input, ref float animationIndex)
	{
		animationIndex = 0f;
		float num = base.transform.InverseTransformDirection(input).z;
		num = Mathf.Clamp(Mathf.Abs(num) * num * 2f, -1f, 1f);
		this.smoothDirectionality = Mathf.SmoothDamp(this.smoothDirectionality, num, ref this.directionalityVelocity, 0.25f);
		this.spring.AddForce(this.force * input);
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x0002CAAC File Offset: 0x0002ACAC
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
		if (skipToMount)
		{
			this.OnGetInto();
			return;
		}
		this.onGottenInto.AddListener(new UnityAction(this.OnGetInto));
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x000059EA File Offset: 0x00003BEA
	private void OnGetInto()
	{
		if (!this.ignoreSpringPhysics)
		{
			this.spring.AddImpulse(this.impulse * base.transform.forward);
		}
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x00005A15 File Offset: 0x00003C15
	public override void GetOut()
	{
		base.GetOut();
		if (!this.ignoreSpringPhysics)
		{
			this.spring.AddImpulse(-this.impulse * base.transform.forward);
		}
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00005A47 File Offset: 0x00003C47
	public override void Cancel()
	{
		if (!this.ignoreSpringPhysics)
		{
			this.spring.AddForce(-80f * base.transform.forward);
		}
		base.Cancel();
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x00005A77 File Offset: 0x00003C77
	public override void CancelMount()
	{
		if (!this.actor.isPlayer && !this.ignoreSpringPhysics)
		{
			this.spring.AddForce(-80f * base.transform.forward);
		}
		base.CancelMount();
	}

	// Token: 0x04000700 RID: 1792
	[Space]
	public HitSpring spring;

	// Token: 0x04000701 RID: 1793
	public float force = 500f;

	// Token: 0x04000702 RID: 1794
	public float impulse = 500f;

	// Token: 0x04000703 RID: 1795
	public bool ignoreSpringPhysics;

	// Token: 0x04000704 RID: 1796
	private float smoothDirectionality;

	// Token: 0x04000705 RID: 1797
	private float directionalityVelocity;
}
