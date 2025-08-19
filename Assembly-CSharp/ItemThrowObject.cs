using System;
using UnityEngine;

public class ItemThrowObject : ItemThrowable
{
	// Token: 0x06000BCB RID: 3019 RVA: 0x0000B06C File Offset: 0x0000926C
	public override void Input(bool isDown, bool isHeld)
	{
		if (this.spawnedObject != null && isDown)
		{
			this.ClearStuff();
			isDown = false;
		}
		base.Input(isDown, isHeld);
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x000415F8 File Offset: 0x0003F7F8
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

	// Token: 0x06000BCD RID: 3021 RVA: 0x0004166C File Offset: 0x0003F86C
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

	// Token: 0x06000BCE RID: 3022 RVA: 0x0000B090 File Offset: 0x00009290
	public override void Cancel()
	{
		base.Cancel();
		this.ClearStuff();
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x000416D8 File Offset: 0x0003F8D8
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

	// Token: 0x06000BD0 RID: 3024 RVA: 0x0000B09E File Offset: 0x0000929E
	public override void OnRemove()
	{
		base.OnRemove();
		this.ClearStuff();
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x00041790 File Offset: 0x0003F990
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
			global::UnityEngine.Object.Destroy(this.spawnedObject);
		}
		this.spawnedObject = null;
		this.SetEquipped(this.isEquipped);
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x0000B0AC File Offset: 0x000092AC
	public override float GetSolveSpeed(float charge = 1f)
	{
		return this.spawnedObjectSpeed;
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x0004180C File Offset: 0x0003FA0C
	public override void Throw(float charge, Vector3 direction)
	{
		base.Throw(charge, direction);
		Player.itemManager.SetEquippedState(base.EquippedState, false);
		this.itemManager.SetItemInUse(this, true);
		this.spawnedObject = global::UnityEngine.Object.Instantiate<GameObject>(this.spawnedObjectPrefab, Player.itemManager.thrownSpawnPoint.position, Player.transform.rotation);
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

	public Renderer unequippedRenderer;

	public GameObject spawnedObjectPrefab;

	private GameObject spawnedObject;

	public Vector3 spawnedObjectPosition;

	public float spawnedObjectSpeed = 10f;

	public bool ragdollWhenSpawning;

	private bool hasRagdolled;

	public bool showItemBefore;

	private bool isEquipped;
}
