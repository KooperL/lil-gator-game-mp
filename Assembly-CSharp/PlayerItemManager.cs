using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001EA RID: 490
public class PlayerItemManager : MonoBehaviour
{
	// Token: 0x1700008A RID: 138
	// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00030E9E File Offset: 0x0002F09E
	public bool PrimaryInUse
	{
		get
		{
			return this.primaryBehaviour != null && this.itemInUse == this.primaryBehaviour;
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00030EB8 File Offset: 0x0002F0B8
	public bool SecondaryInUse
	{
		get
		{
			return this.secondaryBehaviour != null && this.itemInUse == this.secondaryBehaviour;
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00030ED2 File Offset: 0x0002F0D2
	public bool IsAnyItemInUse
	{
		get
		{
			return this.itemBehaviour != null && this.itemInUse == this.itemBehaviour;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00030EEC File Offset: 0x0002F0EC
	public bool IsItemInUse
	{
		get
		{
			return this.itemBehaviour != null && this.itemInUse == this.itemBehaviour;
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00030F06 File Offset: 0x0002F106
	public bool IsItemInUse_R
	{
		get
		{
			return this.itemBehaviour_r != null && this.itemInUse == this.itemBehaviour_r;
		}
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00030F20 File Offset: 0x0002F120
	public void SetItemInUse(IItemBehaviour item, bool isInUse)
	{
		if (isInUse)
		{
			if (item != this.itemInUse)
			{
				if (this.itemInUse != null)
				{
					this.itemInUse.Cancel();
				}
				this.itemInUse = item;
				return;
			}
		}
		else if (this.itemInUse == item)
		{
			this.itemInUse = null;
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00030F59 File Offset: 0x0002F159
	public bool RightHandBusy
	{
		get
		{
			return this.equippedState == PlayerItemManager.EquippedState.ShieldSled || this.equippedState == PlayerItemManager.EquippedState.SwordAndShield || Player.actorStates.isFidgeting;
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00030F79 File Offset: 0x0002F179
	public bool LeftHandBusy
	{
		get
		{
			return this.equippedState == PlayerItemManager.EquippedState.ShieldSled || this.equippedState == PlayerItemManager.EquippedState.SwordAndShield || Player.actorStates.isFidgeting;
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00030F99 File Offset: 0x0002F199
	// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00030FA1 File Offset: 0x0002F1A1
	public bool IsAiming
	{
		get
		{
			return this.isAiming;
		}
		set
		{
			this.isAiming = value;
			Player.reaction.isAiming = value;
		}
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x00030FB5 File Offset: 0x0002F1B5
	private void Awake()
	{
		this.movement = base.GetComponent<PlayerMovement>();
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x00030FC3 File Offset: 0x0002F1C3
	private void OnEnable()
	{
		PlayerItemManager.p = this;
		this.Refresh();
		this.framesUntilRefresh = 2;
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x00030FD8 File Offset: 0x0002F1D8
	private void Start()
	{
		this.bareHead.SetActive(false);
		this.framesUntilRefresh = 2;
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x00030FF0 File Offset: 0x0002F1F0
	public void Refresh()
	{
		if (ItemManager.i == null)
		{
			return;
		}
		this.framesUntilRefresh = -1;
		if (this.usePersistentItems)
		{
			string text;
			GameObject gameObject;
			ItemManager.i.GetPrimary(out text, out gameObject);
			this.RefreshVariant(ref this.primaryObject, ref this.primaryID, ref this.primaryBehaviour, text, gameObject, this.leftHandAnchor, true, 0);
			ItemManager.i.GetSecondary(out text, out gameObject);
			this.RefreshVariant(ref this.secondaryObject, ref this.secondaryID, ref this.secondaryBehaviour, text, gameObject, this.rightHandAnchor, true, 0);
			ItemManager.i.GetItem(out text, out gameObject);
			this.RefreshVariant(ref this.itemObject, ref this.itemID, ref this.itemBehaviour, text, gameObject, this.leftHandAnchor, true, 0);
			ItemManager.i.GetItem_R(out text, out gameObject);
			this.RefreshVariant(ref this.itemObject_r, ref this.itemID_r, ref this.itemBehaviour_r, text, gameObject, this.leftHandAnchor, true, 1);
			ItemManager.i.GetHat(out text, out gameObject);
			this.RefreshVariant(ref this.hatObject, ref this.hatID, ref this.hatBehaviour, text, gameObject, this.hatAnchor, true, 0);
			int braceletsCollected = ItemManager.i.BraceletsCollected;
			this.movement.maxStamina = (float)braceletsCollected;
			for (int i = 0; i < this.bracelets.Length; i++)
			{
				this.bracelets[i].SetActive(braceletsCollected > i);
			}
			this.movement.glidingUnlocked = this.gliderItem.IsUnlocked;
		}
		else
		{
			if (this.nonPersistentPrimary != null)
			{
				this.RefreshVariant(ref this.primaryObject, ref this.primaryID, ref this.primaryBehaviour, this.nonPersistentPrimary.id, this.nonPersistentPrimary.prefab, this.leftHandAnchor, true, 0);
			}
			else
			{
				this.RefreshVariant(ref this.primaryObject, ref this.primaryID, ref this.primaryBehaviour, "", null, this.leftHandAnchor, true, 0);
			}
			if (this.nonPersistentSecondary != null)
			{
				this.RefreshVariant(ref this.secondaryObject, ref this.secondaryID, ref this.secondaryBehaviour, this.nonPersistentSecondary.id, this.nonPersistentSecondary.prefab, this.rightHandAnchor, true, 0);
			}
			else
			{
				this.RefreshVariant(ref this.secondaryObject, ref this.secondaryID, ref this.secondaryBehaviour, "", null, this.rightHandAnchor, true, 0);
			}
			if (this.nonPersistentItem != null)
			{
				this.RefreshVariant(ref this.itemObject, ref this.itemID, ref this.itemBehaviour, this.nonPersistentItem.id, this.nonPersistentItem.prefab, this.leftHandAnchor, true, 0);
			}
			else
			{
				this.RefreshVariant(ref this.itemObject, ref this.itemID, ref this.itemBehaviour, "", null, this.leftHandAnchor, true, 0);
			}
			if (this.nonPersistentHat != null)
			{
				this.RefreshVariant(ref this.hatObject, ref this.hatID, ref this.hatBehaviour, this.nonPersistentHat.id, this.nonPersistentHat.prefab, this.hatAnchor, true, 0);
			}
			else
			{
				this.RefreshVariant(ref this.hatObject, ref this.hatID, ref this.hatBehaviour, "", null, this.hatAnchor, true, 0);
			}
			this.movement.maxStamina = (float)this.nonPersistentBraceletCount;
		}
		this.SetEquippedState(this.equippedState, true);
		PlayerItemManager.onItemRefresh.Invoke();
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x00031340 File Offset: 0x0002F540
	private void RefreshVariant(ref GameObject item, ref string itemID, ref IItemBehaviour itemBehaviour, string newID, GameObject newPrefab, Transform anchor, bool active, int index = 0)
	{
		if (newPrefab != null != (item != null) || item == null || itemID != newID)
		{
			if (itemBehaviour != null)
			{
				if (itemBehaviour == this.itemInUse)
				{
					this.itemInUse.Cancel();
					this.itemInUse = null;
				}
				itemBehaviour.OnRemove();
				itemBehaviour = null;
			}
			if (item != null)
			{
				Object.Destroy(item);
			}
			if (newPrefab != null)
			{
				item = Object.Instantiate<GameObject>(newPrefab, anchor);
				item.transform.localPosition = Vector3.zero;
				item.transform.localRotation = Quaternion.identity;
				item.SetActive(active);
				itemID = newID;
				itemBehaviour = item.GetComponent<IItemBehaviour>();
				if (itemBehaviour != null)
				{
					itemBehaviour.SetIndex(index);
				}
			}
			EffectsManager.e.Dust(anchor.position, 5, Vector3.zero, 0f);
		}
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x00031428 File Offset: 0x0002F628
	public void SetEquippedState(PlayerItemManager.EquippedState newEquippedState, bool force = false)
	{
		if (newEquippedState == this.equippedState && !force)
		{
			return;
		}
		this.equippedState = newEquippedState;
		if (this.primaryBehaviour != null)
		{
			this.primaryBehaviour.SetEquipped(newEquippedState == PlayerItemManager.EquippedState.SwordAndShield);
		}
		if (this.secondaryBehaviour != null)
		{
			this.secondaryBehaviour.SetEquipped(newEquippedState == PlayerItemManager.EquippedState.SwordAndShield || this.equippedState == PlayerItemManager.EquippedState.ShieldSled);
		}
		if (this.itemBehaviour != null)
		{
			this.itemBehaviour.SetEquipped(newEquippedState == PlayerItemManager.EquippedState.Item);
		}
		if (this.itemBehaviour_r != null)
		{
			this.itemBehaviour_r.SetEquipped(newEquippedState == PlayerItemManager.EquippedState.ItemR);
		}
		if (this.phone != null)
		{
			this.phone.SetActive(newEquippedState == PlayerItemManager.EquippedState.Phone);
		}
		if (this.hatBehaviour != null)
		{
			this.hatBehaviour.SetEquipped(newEquippedState == PlayerItemManager.EquippedState.Gliding);
		}
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x000314E8 File Offset: 0x0002F6E8
	private void FixedUpdate()
	{
		if (this.framesUntilRefresh > 0)
		{
			this.framesUntilRefresh--;
		}
		if (this.framesUntilRefresh == 0)
		{
			this.Refresh();
		}
		PlayerItemManager.EquippedState equippedState = this.equippedState;
		if (Game.State == GameState.ItemMenu)
		{
			return;
		}
		if (this.equippedState == PlayerItemManager.EquippedState.Phone && Game.State != GameState.Play)
		{
			return;
		}
		if (this.movement.isGliding)
		{
			equippedState = PlayerItemManager.EquippedState.Gliding;
		}
		else if (this.equippedState == PlayerItemManager.EquippedState.Gliding)
		{
			equippedState = PlayerItemManager.EquippedState.None;
		}
		if (this.movement.IsClimbing || this.movement.IsSwimming)
		{
			equippedState = PlayerItemManager.EquippedState.None;
		}
		if (this.movement.isRagdolling && this.equippedState != PlayerItemManager.EquippedState.Item && this.equippedState != PlayerItemManager.EquippedState.ItemR)
		{
			equippedState = PlayerItemManager.EquippedState.None;
		}
		if (this.equippedState == PlayerItemManager.EquippedState.ShieldSled && (!this.movement.isSledding || this.movement.isModified))
		{
			equippedState = PlayerItemManager.EquippedState.None;
		}
		if (this.movement.isSledding)
		{
			equippedState = PlayerItemManager.EquippedState.ShieldSled;
		}
		if (Game.State != GameState.Play)
		{
			equippedState = PlayerItemManager.EquippedState.None;
		}
		if (equippedState != this.equippedState)
		{
			this.SetEquippedState(equippedState, false);
		}
		if (this.equippedState != PlayerItemManager.EquippedState.Item && this.itemInUse == this.itemBehaviour)
		{
			this.itemInUse = null;
		}
		if (this.equippedState != PlayerItemManager.EquippedState.ItemR && this.itemInUse == this.itemBehaviour_r)
		{
			this.itemInUse = null;
		}
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x00031620 File Offset: 0x0002F820
	public void EquipLeft(GameObject itemObject)
	{
		if (this.leftHandHeld != itemObject)
		{
			if (this.leftHandHeld != null)
			{
				this.leftHandHeld.SetActive(false);
			}
			this.leftHandHeld = itemObject;
		}
		if (this.leftHandHeld != null)
		{
			this.leftHandHeld.SetActive(true);
		}
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x00031676 File Offset: 0x0002F876
	private void ClearLeft()
	{
		if (this.leftHandHeld != null)
		{
			this.leftHandHeld.SetActive(false);
		}
		this.leftHandHeld = null;
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0003169C File Offset: 0x0002F89C
	public void CutGrass()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.TransformPoint(this.grassOffset), Vector3.down, out raycastHit, 2f, this.grassLayerMask))
		{
			TerrainDetails component = raycastHit.collider.GetComponent<TerrainDetails>();
			if (component != null)
			{
				component.ClearDetailsBox(base.transform, new Vector2(-1.5f, -0.5f), new Vector2(1.5f, 1.5f), 0.5f);
			}
		}
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0003171D File Offset: 0x0002F91D
	public void EquipPhone()
	{
		this.SetEquippedState(PlayerItemManager.EquippedState.Phone, false);
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x00031728 File Offset: 0x0002F928
	public void OnPrimary(bool isDown, bool isHeld)
	{
		if (this.primaryBehaviour == null || !this.movement.CanUsePrimary)
		{
			return;
		}
		if (isDown && !this.PrimaryInUse)
		{
			if (this.movement.modPrimaryRule == PlayerMovement.ModRule.Cancels)
			{
				this.movement.ClearMods();
			}
			if (this.itemInUse != null)
			{
				this.SetItemInUse(null, true);
			}
			else
			{
				Player.input.cancelAction = true;
			}
		}
		this.primaryBehaviour.Input(isDown, isHeld);
		if (isDown)
		{
			this.primaryButtonTutorial.Press();
		}
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x000317A8 File Offset: 0x0002F9A8
	public void OnSecondary(bool isDown, bool isHeld)
	{
		if (this.secondaryBehaviour == null || !this.movement.CanUseSecondary)
		{
			return;
		}
		if (isDown && !this.SecondaryInUse)
		{
			if (this.movement.modSecondaryRule == PlayerMovement.ModRule.Cancels)
			{
				this.movement.ClearMods();
			}
			if (this.itemInUse != null)
			{
				this.SetItemInUse(null, true);
			}
			else
			{
				Player.input.cancelAction = true;
			}
		}
		this.secondaryBehaviour.Input(isDown, isHeld);
		if (isDown)
		{
			this.secondaryButtonTutorial.Press();
		}
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x00031828 File Offset: 0x0002FA28
	public void OnUseItem(bool isDown, bool isHeld)
	{
		if (this.itemBehaviour == null || !this.movement.CanUseItem)
		{
			return;
		}
		if (isDown && !this.IsItemInUse)
		{
			if (this.movement.modItemRule == PlayerMovement.ModRule.Cancels)
			{
				this.movement.ClearMods();
			}
			if (this.itemInUse != null)
			{
				this.SetItemInUse(null, true);
			}
			else
			{
				Player.input.cancelAction = true;
			}
		}
		this.itemBehaviour.Input(isDown, isHeld);
		if (isDown)
		{
			this.itemButtonTutorial.Press();
		}
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x000318A8 File Offset: 0x0002FAA8
	public void OnUseItem_R(bool isDown, bool isHeld)
	{
		if (this.itemBehaviour_r == null || !this.movement.CanUseItem)
		{
			return;
		}
		if (isDown && !this.IsItemInUse_R)
		{
			if (this.movement.modItemRule == PlayerMovement.ModRule.Cancels)
			{
				this.movement.ClearMods();
			}
			if (this.itemInUse != null)
			{
				this.SetItemInUse(null, true);
			}
			else
			{
				Player.input.cancelAction = true;
			}
		}
		this.itemBehaviour_r.Input(isDown, isHeld);
		if (isDown)
		{
			this.itemButtonTutorial_r.Press();
		}
	}

	// Token: 0x04000D17 RID: 3351
	public static PlayerItemManager p;

	// Token: 0x04000D18 RID: 3352
	public static UnityEvent onItemRefresh = new UnityEvent();

	// Token: 0x04000D19 RID: 3353
	public Animator animator;

	// Token: 0x04000D1A RID: 3354
	private PlayerMovement movement;

	// Token: 0x04000D1B RID: 3355
	public bool handsPreoccupied;

	// Token: 0x04000D1C RID: 3356
	public GameObject aimingCamera;

	// Token: 0x04000D1D RID: 3357
	public GameObject menuCamera;

	// Token: 0x04000D1E RID: 3358
	public bool usePersistentItems = true;

	// Token: 0x04000D1F RID: 3359
	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public ItemObject nonPersistentHat;

	// Token: 0x04000D20 RID: 3360
	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public ItemObject nonPersistentPrimary;

	// Token: 0x04000D21 RID: 3361
	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public ItemObject nonPersistentSecondary;

	// Token: 0x04000D22 RID: 3362
	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public ItemObject nonPersistentItem;

	// Token: 0x04000D23 RID: 3363
	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public int nonPersistentBraceletCount;

	// Token: 0x04000D24 RID: 3364
	public ItemObject nonPersistentItem_r;

	// Token: 0x04000D25 RID: 3365
	[Header("Equipped Items")]
	public GameObject primaryObject;

	// Token: 0x04000D26 RID: 3366
	public IItemBehaviour primaryBehaviour;

	// Token: 0x04000D27 RID: 3367
	private string primaryID;

	// Token: 0x04000D28 RID: 3368
	public GameObject secondaryObject;

	// Token: 0x04000D29 RID: 3369
	public IItemBehaviour secondaryBehaviour;

	// Token: 0x04000D2A RID: 3370
	private string secondaryID;

	// Token: 0x04000D2B RID: 3371
	public GameObject hatObject;

	// Token: 0x04000D2C RID: 3372
	public IItemBehaviour hatBehaviour;

	// Token: 0x04000D2D RID: 3373
	private string hatID;

	// Token: 0x04000D2E RID: 3374
	public GameObject itemObject;

	// Token: 0x04000D2F RID: 3375
	public IItemBehaviour itemBehaviour;

	// Token: 0x04000D30 RID: 3376
	private string itemID;

	// Token: 0x04000D31 RID: 3377
	public GameObject itemObject_r;

	// Token: 0x04000D32 RID: 3378
	public IItemBehaviour itemBehaviour_r;

	// Token: 0x04000D33 RID: 3379
	private string itemID_r;

	// Token: 0x04000D34 RID: 3380
	public IItemBehaviour itemInUse;

	// Token: 0x04000D35 RID: 3381
	private GameObject leftHandHeld;

	// Token: 0x04000D36 RID: 3382
	private GameObject rightHandHeld;

	// Token: 0x04000D37 RID: 3383
	[Header("Anchors")]
	public Transform leftHandAnchor;

	// Token: 0x04000D38 RID: 3384
	[Header("Anchors")]
	public Transform rightHandAnchor;

	// Token: 0x04000D39 RID: 3385
	public Transform shieldArmAnchor;

	// Token: 0x04000D3A RID: 3386
	public Transform shieldSledAnchor;

	// Token: 0x04000D3B RID: 3387
	public Transform shieldUnderArmAnchor;

	// Token: 0x04000D3C RID: 3388
	public Transform shieldSkateAnchor;

	// Token: 0x04000D3D RID: 3389
	public Transform shieldSledBellyAnchor;

	// Token: 0x04000D3E RID: 3390
	public Transform hatAnchor;

	// Token: 0x04000D3F RID: 3391
	public Transform gliderAnchor;

	// Token: 0x04000D40 RID: 3392
	public Transform swordUnequippedAnchor;

	// Token: 0x04000D41 RID: 3393
	public Transform shieldUnequippedAnchor;

	// Token: 0x04000D42 RID: 3394
	public Transform thrownSpawnPoint;

	// Token: 0x04000D43 RID: 3395
	public Transform hipAnchor;

	// Token: 0x04000D44 RID: 3396
	public Transform satchelAnchor;

	// Token: 0x04000D45 RID: 3397
	public Transform holsterAnchor;

	// Token: 0x04000D46 RID: 3398
	public Transform hipAnchor_r;

	// Token: 0x04000D47 RID: 3399
	public Transform satchelAnchor_r;

	// Token: 0x04000D48 RID: 3400
	public Transform holsterAnchor_r;

	// Token: 0x04000D49 RID: 3401
	public Transform chestAnchor;

	// Token: 0x04000D4A RID: 3402
	public Transform hipsAnchor;

	// Token: 0x04000D4B RID: 3403
	public Transform lowerSpineAnchor;

	// Token: 0x04000D4C RID: 3404
	public Transform shoulderAnchor;

	// Token: 0x04000D4D RID: 3405
	[Header("Weapon")]
	public Vector3 grassOffset;

	// Token: 0x04000D4E RID: 3406
	public LayerMask grassLayerMask;

	// Token: 0x04000D4F RID: 3407
	[Header("Accessories")]
	public ItemObject gliderItem;

	// Token: 0x04000D50 RID: 3408
	public GameObject moonShoes;

	// Token: 0x04000D51 RID: 3409
	public float moonShoesHeight;

	// Token: 0x04000D52 RID: 3410
	public GameObject phone;

	// Token: 0x04000D53 RID: 3411
	public GameObject[] bracelets;

	// Token: 0x04000D54 RID: 3412
	public GameObject hipSatchel;

	// Token: 0x04000D55 RID: 3413
	public GameObject hipSatchel_r;

	// Token: 0x04000D56 RID: 3414
	public GameObject bareHead;

	// Token: 0x04000D57 RID: 3415
	[Header("Misc")]
	public Rigidbody armRigidbody;

	// Token: 0x04000D58 RID: 3416
	public Rigidbody chestRigidbody;

	// Token: 0x04000D59 RID: 3417
	public Rigidbody headRigidbody;

	// Token: 0x04000D5A RID: 3418
	public ButtonTutorial primaryButtonTutorial;

	// Token: 0x04000D5B RID: 3419
	public ButtonTutorial secondaryButtonTutorial;

	// Token: 0x04000D5C RID: 3420
	public ButtonTutorial itemButtonTutorial;

	// Token: 0x04000D5D RID: 3421
	public ButtonTutorial itemButtonTutorial_r;

	// Token: 0x04000D5E RID: 3422
	private bool isAiming;

	// Token: 0x04000D5F RID: 3423
	[ReadOnly]
	public PlayerItemManager.EquippedState equippedState;

	// Token: 0x04000D60 RID: 3424
	private int framesUntilRefresh;

	// Token: 0x020003E8 RID: 1000
	public enum EquippedState
	{
		// Token: 0x04001C72 RID: 7282
		None,
		// Token: 0x04001C73 RID: 7283
		SwordAndShield,
		// Token: 0x04001C74 RID: 7284
		ShieldSled,
		// Token: 0x04001C75 RID: 7285
		Item,
		// Token: 0x04001C76 RID: 7286
		Phone,
		// Token: 0x04001C77 RID: 7287
		Gliding,
		// Token: 0x04001C78 RID: 7288
		ItemR
	}
}
