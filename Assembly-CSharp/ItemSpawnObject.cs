using System;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class ItemSpawnObject : MonoBehaviour, IItemBehaviour
{
	// Token: 0x17000081 RID: 129
	// (get) Token: 0x060009CB RID: 2507 RVA: 0x0002D836 File Offset: 0x0002BA36
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

	// Token: 0x060009CC RID: 2508 RVA: 0x0002D844 File Offset: 0x0002BA44
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

	// Token: 0x060009CD RID: 2509 RVA: 0x0002D938 File Offset: 0x0002BB38
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

	// Token: 0x060009CE RID: 2510 RVA: 0x0002D9C0 File Offset: 0x0002BBC0
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

	// Token: 0x060009CF RID: 2511 RVA: 0x0002DA68 File Offset: 0x0002BC68
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

	// Token: 0x060009D0 RID: 2512 RVA: 0x0002DADA File Offset: 0x0002BCDA
	public void OnRemove()
	{
		this.Cancel();
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0002DAE2 File Offset: 0x0002BCE2
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	// Token: 0x04000C13 RID: 3091
	public Renderer unequippedRenderer;

	// Token: 0x04000C14 RID: 3092
	public GameObject spawnedObjectPrefab;

	// Token: 0x04000C15 RID: 3093
	private GameObject spawnedObject;

	// Token: 0x04000C16 RID: 3094
	public Vector3 spawnedObjectPosition;

	// Token: 0x04000C17 RID: 3095
	public Vector3 spawnedObjectVelocity;

	// Token: 0x04000C18 RID: 3096
	public bool ragdollWhenSpawning;

	// Token: 0x04000C19 RID: 3097
	private bool hasRagdolled;

	// Token: 0x04000C1A RID: 3098
	public bool showItemBefore;

	// Token: 0x04000C1B RID: 3099
	public float minimumStamina = -1f;

	// Token: 0x04000C1C RID: 3100
	public GameObject despawnPrefab;

	// Token: 0x04000C1D RID: 3101
	private bool isOnRight;
}
