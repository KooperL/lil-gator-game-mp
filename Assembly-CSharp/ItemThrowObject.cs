using System;
using UnityEngine;

// Token: 0x020001D8 RID: 472
public class ItemThrowObject : ItemThrowable
{
	// Token: 0x060009D6 RID: 2518 RVA: 0x0002DC5C File Offset: 0x0002BE5C
	public override void Input(bool isDown, bool isHeld)
	{
		if (this.spawnedObject != null && isDown)
		{
			this.ClearStuff();
			isDown = false;
		}
		base.Input(isDown, isHeld);
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x0002DC80 File Offset: 0x0002BE80
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

	// Token: 0x060009D8 RID: 2520 RVA: 0x0002DCF4 File Offset: 0x0002BEF4
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

	// Token: 0x060009D9 RID: 2521 RVA: 0x0002DD60 File Offset: 0x0002BF60
	public override void Cancel()
	{
		base.Cancel();
		this.ClearStuff();
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x0002DD70 File Offset: 0x0002BF70
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

	// Token: 0x060009DB RID: 2523 RVA: 0x0002DE26 File Offset: 0x0002C026
	public override void OnRemove()
	{
		base.OnRemove();
		this.ClearStuff();
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0002DE34 File Offset: 0x0002C034
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

	// Token: 0x060009DD RID: 2525 RVA: 0x0002DEAF File Offset: 0x0002C0AF
	public override float GetSolveSpeed(float charge = 1f)
	{
		return this.spawnedObjectSpeed;
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0002DEB8 File Offset: 0x0002C0B8
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

	// Token: 0x04000C25 RID: 3109
	public Renderer unequippedRenderer;

	// Token: 0x04000C26 RID: 3110
	public GameObject spawnedObjectPrefab;

	// Token: 0x04000C27 RID: 3111
	private GameObject spawnedObject;

	// Token: 0x04000C28 RID: 3112
	public Vector3 spawnedObjectPosition;

	// Token: 0x04000C29 RID: 3113
	public float spawnedObjectSpeed = 10f;

	// Token: 0x04000C2A RID: 3114
	public bool ragdollWhenSpawning;

	// Token: 0x04000C2B RID: 3115
	private bool hasRagdolled;

	// Token: 0x04000C2C RID: 3116
	public bool showItemBefore;

	// Token: 0x04000C2D RID: 3117
	private bool isEquipped;
}
