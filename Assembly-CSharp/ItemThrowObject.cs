using System;
using UnityEngine;

// Token: 0x0200025F RID: 607
public class ItemThrowObject : ItemThrowable
{
	// Token: 0x06000B7F RID: 2943 RVA: 0x0000AD5A File Offset: 0x00008F5A
	public override void Input(bool isDown, bool isHeld)
	{
		if (this.spawnedObject != null && isDown)
		{
			this.ClearStuff();
			isDown = false;
		}
		base.Input(isDown, isHeld);
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x0003FA94 File Offset: 0x0003DC94
	public override void LateUpdate()
	{
		if (this.spawnedObject != null)
		{
			bool flag = false;
			if (this.hasRagdolled || this.ragdollWhenSpawning)
			{
				if (!Player.movement.isModified)
				{
					flag = true;
				}
			}
			else if (Player.movement.isRagdolling)
			{
				this.hasRagdolled = true;
			}
			if (Player.itemManager.equippedState != base.EquippedState)
			{
				flag = true;
			}
			if (flag)
			{
				this.ClearStuff();
				return;
			}
		}
		else
		{
			base.LateUpdate();
		}
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x0003FB08 File Offset: 0x0003DD08
	public override void SetCharging(bool isCharging)
	{
		if (UIMenus.reticle != null)
		{
			if (isCharging)
			{
				UIMenus.reticle.StartAiming(this.chargeTime);
			}
			else
			{
				UIMenus.reticle.StopAiming();
			}
		}
		if (isCharging)
		{
			this.itemManager.SetItemInUse(this, isCharging);
		}
		Player.itemManager.IsAiming = isCharging;
		this.animator.SetBool("Throwing", isCharging);
		this.charging = isCharging;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x0000AD7E File Offset: 0x00008F7E
	public override void Cancel()
	{
		base.Cancel();
		this.ClearStuff();
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x0003FB74 File Offset: 0x0003DD74
	public override void SetEquipped(bool isEquipped)
	{
		this.isEquipped = isEquipped;
		Transform transform = (this.isOnRight ? Player.itemManager.hipAnchor_r : Player.itemManager.hipAnchor);
		if (this.showItemBefore && isEquipped)
		{
			transform = this.itemManager.leftHandAnchor;
		}
		if (base.transform.parent != transform)
		{
			base.transform.ApplyParent(transform);
		}
		if (!isEquipped)
		{
			Player.itemManager.IsAiming = false;
		}
		if (!this.showItemBefore)
		{
			this.unequippedRenderer.enabled = !isEquipped;
			return;
		}
		if (!isEquipped)
		{
			this.unequippedRenderer.enabled = true;
			return;
		}
		this.unequippedRenderer.enabled = this.spawnedObject == null;
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x0000AD8C File Offset: 0x00008F8C
	public override void OnRemove()
	{
		base.OnRemove();
		this.ClearStuff();
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x0003FC2C File Offset: 0x0003DE2C
	private void ClearStuff()
	{
		if (Player.itemManager.itemInUse == this)
		{
			this.itemManager.SetItemInUse(this, false);
			Player.movement.isModified = false;
			if (Player.movement.isRagdolling)
			{
				Player.movement.ClearMods();
			}
		}
		this.hasRagdolled = false;
		if (this.spawnedObject != null)
		{
			Object.Destroy(this.spawnedObject);
		}
		this.spawnedObject = null;
		this.SetEquipped(this.isEquipped);
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x0000AD9A File Offset: 0x00008F9A
	public override float GetSolveSpeed(float charge = 1f)
	{
		return this.spawnedObjectSpeed;
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x0003FCA8 File Offset: 0x0003DEA8
	public override void Throw(float charge, Vector3 direction)
	{
		base.Throw(charge, direction);
		Player.itemManager.SetEquippedState(base.EquippedState, false);
		this.itemManager.SetItemInUse(this, true);
		this.spawnedObject = Object.Instantiate<GameObject>(this.spawnedObjectPrefab, Player.itemManager.thrownSpawnPoint.position, Player.transform.rotation);
		Vector3 vector = this.spawnedObjectSpeed * direction;
		Rigidbody component = this.spawnedObject.GetComponent<Rigidbody>();
		if (component != null)
		{
			component.velocity = vector;
		}
		Player.movement.isModified = true;
		if (this.ragdollWhenSpawning)
		{
			Player.movement.Ragdoll();
		}
		this.SetEquipped(this.isEquipped);
	}

	// Token: 0x04000E5D RID: 3677
	public Renderer unequippedRenderer;

	// Token: 0x04000E5E RID: 3678
	public GameObject spawnedObjectPrefab;

	// Token: 0x04000E5F RID: 3679
	private GameObject spawnedObject;

	// Token: 0x04000E60 RID: 3680
	public Vector3 spawnedObjectPosition;

	// Token: 0x04000E61 RID: 3681
	public float spawnedObjectSpeed = 10f;

	// Token: 0x04000E62 RID: 3682
	public bool ragdollWhenSpawning;

	// Token: 0x04000E63 RID: 3683
	private bool hasRagdolled;

	// Token: 0x04000E64 RID: 3684
	public bool showItemBefore;

	// Token: 0x04000E65 RID: 3685
	private bool isEquipped;
}
