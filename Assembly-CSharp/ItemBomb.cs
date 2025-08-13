using System;
using UnityEngine;

// Token: 0x020001B5 RID: 437
public class ItemBomb : ItemThrowable, IOnTimeout
{
	// Token: 0x0600090B RID: 2315 RVA: 0x0002B534 File Offset: 0x00029734
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

	// Token: 0x0600090C RID: 2316 RVA: 0x0002B560 File Offset: 0x00029760
	public override void Throw(float charge, Vector3 direction)
	{
		this.thrownBomb = Object.Instantiate<GameObject>(this.thrownPrefab, Player.itemManager.thrownSpawnPoint.position, base.transform.rotation);
		Rigidbody component = this.thrownBomb.GetComponent<Rigidbody>();
		Vector3 vector = Mathf.Lerp(this.minThrowSpeed, this.maxThrowSpeed, charge) * direction;
		component.velocity = vector;
		this.thrownBomb.GetComponent<DistanceTimeout>().callback = this;
		base.Throw(charge, direction);
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0002B5DB File Offset: 0x000297DB
	public override void Cancel()
	{
		this.PopBomb(true);
		base.Cancel();
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0002B5EA File Offset: 0x000297EA
	public override void OnRemove()
	{
		this.PopBomb(true);
		base.OnRemove();
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x0002B5FC File Offset: 0x000297FC
	private void PopBomb(bool destroyBomb = true)
	{
		if (this.thrownBomb == null)
		{
			return;
		}
		Object.Instantiate<GameObject>(this.popPrefab, this.thrownBomb.transform.position, Quaternion.identity);
		if (destroyBomb)
		{
			Object.Destroy(this.thrownBomb);
		}
		this.itemManager.SetItemInUse(this, false);
		this.explodeTime = Time.time;
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x0002B65F File Offset: 0x0002985F
	protected override bool CanStartThrow(bool isDown, bool isHeld)
	{
		return isDown || Time.time - this.explodeTime >= this.heldDelayAfterExplode;
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x0002B680 File Offset: 0x00029880
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

	// Token: 0x06000912 RID: 2322 RVA: 0x0002B6C8 File Offset: 0x000298C8
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

	// Token: 0x06000913 RID: 2323 RVA: 0x0002B709 File Offset: 0x00029909
	public void OnTimeout()
	{
		this.PopBomb(false);
	}

	// Token: 0x04000B69 RID: 2921
	public Renderer heldBomb;

	// Token: 0x04000B6A RID: 2922
	public ParticleSystem[] heldParticles;

	// Token: 0x04000B6B RID: 2923
	public GameObject thrownPrefab;

	// Token: 0x04000B6C RID: 2924
	private GameObject thrownBomb;

	// Token: 0x04000B6D RID: 2925
	public GameObject popPrefab;

	// Token: 0x04000B6E RID: 2926
	public float minThrowSpeed;

	// Token: 0x04000B6F RID: 2927
	public float maxThrowSpeed;

	// Token: 0x04000B70 RID: 2928
	public float heldDelayAfterExplode = 0.5f;

	// Token: 0x04000B71 RID: 2929
	private float explodeTime = -1f;
}
