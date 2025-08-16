using System;
using UnityEngine;

public class ItemSpawnObject : MonoBehaviour, IItemBehaviour
{
	// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0000AFC6 File Offset: 0x000091C6
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

	// Token: 0x06000BC1 RID: 3009 RVA: 0x000410E8 File Offset: 0x0003F2E8
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
				this.spawnedObject = global::UnityEngine.Object.Instantiate<GameObject>(this.spawnedObjectPrefab, Player.RawPosition + Player.transform.rotation * this.spawnedObjectPosition, Player.transform.rotation);
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

	// Token: 0x06000BC2 RID: 3010 RVA: 0x000411DC File Offset: 0x0003F3DC
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

	// Token: 0x06000BC3 RID: 3011 RVA: 0x00041264 File Offset: 0x0003F464
	public void Cancel()
	{
		Player.movement.isModified = false;
		if (this.spawnedObject != null)
		{
			if (this.despawnPrefab != null)
			{
				global::UnityEngine.Object.Instantiate<GameObject>(this.despawnPrefab, this.spawnedObject.transform.position, this.spawnedObject.transform.rotation);
			}
			global::UnityEngine.Object.Destroy(this.spawnedObject);
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

	// Token: 0x06000BC4 RID: 3012 RVA: 0x0004130C File Offset: 0x0003F50C
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

	// Token: 0x06000BC5 RID: 3013 RVA: 0x0000AFD3 File Offset: 0x000091D3
	public void OnRemove()
	{
		this.Cancel();
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x0000AFDB File Offset: 0x000091DB
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	public Renderer unequippedRenderer;

	public GameObject spawnedObjectPrefab;

	private GameObject spawnedObject;

	public Vector3 spawnedObjectPosition;

	public Vector3 spawnedObjectVelocity;

	public bool ragdollWhenSpawning;

	private bool hasRagdolled;

	public bool showItemBefore;

	public float minimumStamina = -1f;

	public GameObject despawnPrefab;

	private bool isOnRight;
}
