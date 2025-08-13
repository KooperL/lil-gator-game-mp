using System;
using UnityEngine;

// Token: 0x02000230 RID: 560
public class ItemBomb : ItemThrowable, IOnTimeout
{
	// Token: 0x06000A90 RID: 2704 RVA: 0x0000A27B File Offset: 0x0000847B
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

	// Token: 0x06000A91 RID: 2705 RVA: 0x0003D92C File Offset: 0x0003BB2C
	public override void Throw(float charge, Vector3 direction)
	{
		this.thrownBomb = Object.Instantiate<GameObject>(this.thrownPrefab, Player.itemManager.thrownSpawnPoint.position, base.transform.rotation);
		Rigidbody component = this.thrownBomb.GetComponent<Rigidbody>();
		Vector3 vector = Mathf.Lerp(this.minThrowSpeed, this.maxThrowSpeed, charge) * direction;
		component.velocity = vector;
		this.thrownBomb.GetComponent<DistanceTimeout>().callback = this;
		base.Throw(charge, direction);
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x0000A2A5 File Offset: 0x000084A5
	public override void Cancel()
	{
		this.PopBomb(true);
		base.Cancel();
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x0000A2B4 File Offset: 0x000084B4
	public override void OnRemove()
	{
		this.PopBomb(true);
		base.OnRemove();
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x0003D9A8 File Offset: 0x0003BBA8
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

	// Token: 0x06000A95 RID: 2709 RVA: 0x0000A2C3 File Offset: 0x000084C3
	protected override bool CanStartThrow(bool isDown, bool isHeld)
	{
		return isDown || Time.time - this.explodeTime >= this.heldDelayAfterExplode;
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0003DA0C File Offset: 0x0003BC0C
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

	// Token: 0x06000A97 RID: 2711 RVA: 0x0003DA54 File Offset: 0x0003BC54
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

	// Token: 0x06000A98 RID: 2712 RVA: 0x0000A2E1 File Offset: 0x000084E1
	public void OnTimeout()
	{
		this.PopBomb(false);
	}

	// Token: 0x04000D71 RID: 3441
	public Renderer heldBomb;

	// Token: 0x04000D72 RID: 3442
	public ParticleSystem[] heldParticles;

	// Token: 0x04000D73 RID: 3443
	public GameObject thrownPrefab;

	// Token: 0x04000D74 RID: 3444
	private GameObject thrownBomb;

	// Token: 0x04000D75 RID: 3445
	public GameObject popPrefab;

	// Token: 0x04000D76 RID: 3446
	public float minThrowSpeed;

	// Token: 0x04000D77 RID: 3447
	public float maxThrowSpeed;

	// Token: 0x04000D78 RID: 3448
	public float heldDelayAfterExplode = 0.5f;

	// Token: 0x04000D79 RID: 3449
	private float explodeTime = -1f;
}
