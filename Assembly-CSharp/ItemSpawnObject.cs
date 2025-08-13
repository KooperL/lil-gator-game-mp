using System;
using UnityEngine;

// Token: 0x0200025D RID: 605
public class ItemSpawnObject : MonoBehaviour, IItemBehaviour
{
	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0000ACD3 File Offset: 0x00008ED3
	private PlayerItemManager.EquippedState EquippedState
	{
		get
		{
			if (!this.isOnRight)
			{
				return PlayerItemManager.EquippedState.Item;
			}
			return PlayerItemManager.EquippedState.ItemR;
		}
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x0003F6F4 File Offset: 0x0003D8F4
	public void Input(bool isDown, bool isHeld)
	{
		if (isDown)
		{
			if (this.spawnedObject != null)
			{
				this.Cancel();
				return;
			}
			if (Player.movement.Stamina > this.minimumStamina)
			{
				Player.itemManager.SetEquippedState(this.EquippedState, false);
				Player.itemManager.SetItemInUse(this, true);
				this.spawnedObject = Object.Instantiate<GameObject>(this.spawnedObjectPrefab, Player.RawPosition + Player.transform.rotation * this.spawnedObjectPosition, Player.transform.rotation);
				Vector3 vector = Player.rigidbody.velocity + Player.transform.rotation * this.spawnedObjectVelocity;
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
			}
		}
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x0003F7E8 File Offset: 0x0003D9E8
	private void LateUpdate()
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
			if (base.transform.position.y >= 150f)
			{
				flag = true;
			}
			if (Player.itemManager.equippedState != this.EquippedState)
			{
				flag = true;
			}
			if (flag)
			{
				this.Cancel();
			}
		}
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x0003F870 File Offset: 0x0003DA70
	public void Cancel()
	{
		Player.movement.isModified = false;
		if (this.spawnedObject != null)
		{
			if (this.despawnPrefab != null)
			{
				Object.Instantiate<GameObject>(this.despawnPrefab, this.spawnedObject.transform.position, this.spawnedObject.transform.rotation);
			}
			Object.Destroy(this.spawnedObject);
		}
		this.spawnedObject = null;
		if (Player.movement.isRagdolling)
		{
			Player.movement.ClearMods();
		}
		if (Player.itemManager.equippedState == this.EquippedState)
		{
			Player.itemManager.SetEquippedState(PlayerItemManager.EquippedState.None, false);
		}
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x0003F918 File Offset: 0x0003DB18
	public void SetEquipped(bool isEquipped)
	{
		Transform transform = (this.isOnRight ? Player.itemManager.hipAnchor_r : Player.itemManager.hipAnchor);
		if (transform != base.transform.parent)
		{
			base.transform.ApplyParent(transform);
		}
		if (this.unequippedRenderer != null)
		{
			this.unequippedRenderer.enabled = !isEquipped;
		}
		if (!isEquipped)
		{
			this.Cancel();
		}
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x0000ACE0 File Offset: 0x00008EE0
	public void OnRemove()
	{
		this.Cancel();
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x0000ACE8 File Offset: 0x00008EE8
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	// Token: 0x04000E4B RID: 3659
	public Renderer unequippedRenderer;

	// Token: 0x04000E4C RID: 3660
	public GameObject spawnedObjectPrefab;

	// Token: 0x04000E4D RID: 3661
	private GameObject spawnedObject;

	// Token: 0x04000E4E RID: 3662
	public Vector3 spawnedObjectPosition;

	// Token: 0x04000E4F RID: 3663
	public Vector3 spawnedObjectVelocity;

	// Token: 0x04000E50 RID: 3664
	public bool ragdollWhenSpawning;

	// Token: 0x04000E51 RID: 3665
	private bool hasRagdolled;

	// Token: 0x04000E52 RID: 3666
	public bool showItemBefore;

	// Token: 0x04000E53 RID: 3667
	public float minimumStamina = -1f;

	// Token: 0x04000E54 RID: 3668
	public GameObject despawnPrefab;

	// Token: 0x04000E55 RID: 3669
	private bool isOnRight;
}
