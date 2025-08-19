using System;
using UnityEngine;
using UnityEngine.Events;

public class SpringRiderMount : ActorMount
{
	// Token: 0x0600052E RID: 1326 RVA: 0x0002DFFC File Offset: 0x0002C1FC
	protected override void HandlePlayerInput(Vector3 input, ref float animationIndex)
	{
		animationIndex = 0f;
		float num = base.transform.InverseTransformDirection(input).z;
		num = Mathf.Clamp(Mathf.Abs(num) * num * 2f, -1f, 1f);
		this.smoothDirectionality = Mathf.SmoothDamp(this.smoothDirectionality, num, ref this.directionalityVelocity, 0.25f);
		this.spring.AddForce(this.force * input);
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x0002E074 File Offset: 0x0002C274
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

	// Token: 0x06000530 RID: 1328 RVA: 0x00005C5F File Offset: 0x00003E5F
	private void OnGetInto()
	{
		if (!this.ignoreSpringPhysics)
		{
			this.spring.AddImpulse(this.impulse * base.transform.forward);
		}
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00005C8A File Offset: 0x00003E8A
	public override void GetOut()
	{
		base.GetOut();
		if (!this.ignoreSpringPhysics)
		{
			this.spring.AddImpulse(-this.impulse * base.transform.forward);
		}
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00005CBC File Offset: 0x00003EBC
	public override void Cancel()
	{
		if (!this.ignoreSpringPhysics)
		{
			this.spring.AddForce(-80f * base.transform.forward);
		}
		base.Cancel();
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00005CEC File Offset: 0x00003EEC
	public override void CancelMount()
	{
		if (!this.actor.isPlayer && !this.ignoreSpringPhysics)
		{
			this.spring.AddForce(-80f * base.transform.forward);
		}
		base.CancelMount();
	}

	[Space]
	public HitSpring spring;

	public float force = 500f;

	public float impulse = 500f;

	public bool ignoreSpringPhysics;

	private float smoothDirectionality;

	private float directionalityVelocity;
}
