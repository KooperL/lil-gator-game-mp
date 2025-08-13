using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000C2 RID: 194
public class SpringRiderMount : ActorMount
{
	// Token: 0x06000436 RID: 1078 RVA: 0x00018498 File Offset: 0x00016698
	protected override void HandlePlayerInput(Vector3 input, ref float animationIndex)
	{
		animationIndex = 0f;
		float num = base.transform.InverseTransformDirection(input).z;
		num = Mathf.Clamp(Mathf.Abs(num) * num * 2f, -1f, 1f);
		this.smoothDirectionality = Mathf.SmoothDamp(this.smoothDirectionality, num, ref this.directionalityVelocity, 0.25f);
		this.spring.AddForce(this.force * input);
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x00018510 File Offset: 0x00016710
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

	// Token: 0x06000438 RID: 1080 RVA: 0x0001857F File Offset: 0x0001677F
	private void OnGetInto()
	{
		if (!this.ignoreSpringPhysics)
		{
			this.spring.AddImpulse(this.impulse * base.transform.forward);
		}
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x000185AA File Offset: 0x000167AA
	public override void GetOut()
	{
		base.GetOut();
		if (!this.ignoreSpringPhysics)
		{
			this.spring.AddImpulse(-this.impulse * base.transform.forward);
		}
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x000185DC File Offset: 0x000167DC
	public override void Cancel()
	{
		if (!this.ignoreSpringPhysics)
		{
			this.spring.AddForce(-80f * base.transform.forward);
		}
		base.Cancel();
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x0001860C File Offset: 0x0001680C
	public override void CancelMount()
	{
		if (!this.actor.isPlayer && !this.ignoreSpringPhysics)
		{
			this.spring.AddForce(-80f * base.transform.forward);
		}
		base.CancelMount();
	}

	// Token: 0x040005ED RID: 1517
	[Space]
	public HitSpring spring;

	// Token: 0x040005EE RID: 1518
	public float force = 500f;

	// Token: 0x040005EF RID: 1519
	public float impulse = 500f;

	// Token: 0x040005F0 RID: 1520
	public bool ignoreSpringPhysics;

	// Token: 0x040005F1 RID: 1521
	private float smoothDirectionality;

	// Token: 0x040005F2 RID: 1522
	private float directionalityVelocity;
}
