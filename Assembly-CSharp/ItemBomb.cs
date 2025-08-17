using System;
using UnityEngine;

public class ItemBomb : ItemThrowable, IOnTimeout
{
	// Token: 0x06000ADC RID: 2780 RVA: 0x0000A5AF File Offset: 0x000087AF
	public override void Input(bool isDown, bool isHeld)
	{
		if (Game.HasControl && this.thrownBomb != null)
		{
			if (isDown)
			{
				this.PopBomb(true);
			}
			return;
		}
		base.Input(isDown, isHeld);
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x0003F434 File Offset: 0x0003D634
	public override void Throw(float charge, Vector3 direction)
	{
		this.thrownBomb = global::UnityEngine.Object.Instantiate<GameObject>(this.thrownPrefab, Player.itemManager.thrownSpawnPoint.position, base.transform.rotation);
		Rigidbody component = this.thrownBomb.GetComponent<Rigidbody>();
		Vector3 vector = Mathf.Lerp(this.minThrowSpeed, this.maxThrowSpeed, charge) * direction;
		component.velocity = vector;
		this.thrownBomb.GetComponent<DistanceTimeout>().callback = this;
		base.Throw(charge, direction);
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x0000A5D9 File Offset: 0x000087D9
	public override void Cancel()
	{
		this.PopBomb(true);
		base.Cancel();
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x0000A5E8 File Offset: 0x000087E8
	public override void OnRemove()
	{
		this.PopBomb(true);
		base.OnRemove();
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x0003F4B0 File Offset: 0x0003D6B0
	private void PopBomb(bool destroyBomb = true)
	{
		if (this.thrownBomb == null)
		{
			return;
		}
		global::UnityEngine.Object.Instantiate<GameObject>(this.popPrefab, this.thrownBomb.transform.position, Quaternion.identity);
		if (destroyBomb)
		{
			global::UnityEngine.Object.Destroy(this.thrownBomb);
		}
		this.itemManager.SetItemInUse(this, false);
		this.explodeTime = Time.time;
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x0000A5F7 File Offset: 0x000087F7
	protected override bool CanStartThrow(bool isDown, bool isHeld)
	{
		return isDown || Time.time - this.explodeTime >= this.heldDelayAfterExplode;
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x0003F514 File Offset: 0x0003D714
	public override void SetCharging(bool isCharging)
	{
		this.heldBomb.enabled = isCharging;
		ParticleSystem[] array = this.heldParticles;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].emission.enabled = isCharging;
		}
		base.SetCharging(isCharging);
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x0003F55C File Offset: 0x0003D75C
	public override void SetEquipped(bool isEquipped)
	{
		Transform leftHandAnchor = Player.itemManager.leftHandAnchor;
		if (base.transform.parent != leftHandAnchor)
		{
			base.transform.ApplyParent(leftHandAnchor);
		}
		if (!isEquipped)
		{
			this.Cancel();
		}
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x0000A615 File Offset: 0x00008815
	public void OnTimeout()
	{
		this.PopBomb(false);
	}

	public Renderer heldBomb;

	public ParticleSystem[] heldParticles;

	public GameObject thrownPrefab;

	private GameObject thrownBomb;

	public GameObject popPrefab;

	public float minThrowSpeed;

	public float maxThrowSpeed;

	public float heldDelayAfterExplode = 0.5f;

	private float explodeTime = -1f;
}
