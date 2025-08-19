using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerItemManager : MonoBehaviour
{
	// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0000B8C3 File Offset: 0x00009AC3
	public bool PrimaryInUse
	{
		get
		{
			return this.primaryBehaviour != null && this.itemInUse == this.primaryBehaviour;
		}
	}

	// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0000B8DD File Offset: 0x00009ADD
	public bool SecondaryInUse
	{
		get
		{
			return this.secondaryBehaviour != null && this.itemInUse == this.secondaryBehaviour;
		}
	}

	// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0000B8F7 File Offset: 0x00009AF7
	public bool IsAnyItemInUse
	{
		get
		{
			return this.itemBehaviour != null && this.itemInUse == this.itemBehaviour;
		}
	}

	// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0000B8F7 File Offset: 0x00009AF7
	public bool IsItemInUse
	{
		get
		{
			return this.itemBehaviour != null && this.itemInUse == this.itemBehaviour;
		}
	}

	// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0000B911 File Offset: 0x00009B11
	public bool IsItemInUse_R
	{
		get
		{
			return this.itemBehaviour_r != null && this.itemInUse == this.itemBehaviour_r;
		}
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x0000B92B File Offset: 0x00009B2B
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

	// (get) Token: 0x06000C64 RID: 3172 RVA: 0x0000B964 File Offset: 0x00009B64
	public bool RightHandBusy
	{
		get
		{
			return this.equippedState == PlayerItemManager.EquippedState.ShieldSled || this.equippedState == PlayerItemManager.EquippedState.SwordAndShield || Player.actorStates.isFidgeting;
		}
	}

	// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0000B964 File Offset: 0x00009B64
	public bool LeftHandBusy
	{
		get
		{
			return this.equippedState == PlayerItemManager.EquippedState.ShieldSled || this.equippedState == PlayerItemManager.EquippedState.SwordAndShield || Player.actorStates.isFidgeting;
		}
	}

	// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0000B984 File Offset: 0x00009B84
	// (set) Token: 0x06000C67 RID: 3175 RVA: 0x0000B98C File Offset: 0x00009B8C
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

	// Token: 0x06000C68 RID: 3176 RVA: 0x0000B9A0 File Offset: 0x00009BA0
	private void Awake()
	{
		this.movement = base.GetComponent<PlayerMovement>();
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x0000B9AE File Offset: 0x00009BAE
	private void OnEnable()
	{
		PlayerItemManager.p = this;
		this.Refresh();
		this.framesUntilRefresh = 2;
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x0000B9C3 File Offset: 0x00009BC3
	private void Start()
	{
		this.bareHead.SetActive(false);
		this.framesUntilRefresh = 2;
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x000440F8 File Offset: 0x000422F8
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

	// Token: 0x06000C6C RID: 3180 RVA: 0x00044448 File Offset: 0x00042648
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
				global::UnityEngine.Object.Destroy(item);
			}
			if (newPrefab != null)
			{
				item = global::UnityEngine.Object.Instantiate<GameObject>(newPrefab, anchor);
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

	// Token: 0x06000C6D RID: 3181 RVA: 0x00044530 File Offset: 0x00042730
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

	// Token: 0x06000C6E RID: 3182 RVA: 0x000445F0 File Offset: 0x000427F0
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

	// Token: 0x06000C6F RID: 3183 RVA: 0x00044728 File Offset: 0x00042928
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

	// Token: 0x06000C70 RID: 3184 RVA: 0x0000B9D8 File Offset: 0x00009BD8
	private void ClearLeft()
	{
		if (this.leftHandHeld != null)
		{
			this.leftHandHeld.SetActive(false);
		}
		this.leftHandHeld = null;
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x00044780 File Offset: 0x00042980
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

	// Token: 0x06000C72 RID: 3186 RVA: 0x0000B9FB File Offset: 0x00009BFB
	public void EquipPhone()
	{
		this.SetEquippedState(PlayerItemManager.EquippedState.Phone, false);
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x00044804 File Offset: 0x00042A04
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

	// Token: 0x06000C74 RID: 3188 RVA: 0x00044884 File Offset: 0x00042A84
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

	// Token: 0x06000C75 RID: 3189 RVA: 0x00044904 File Offset: 0x00042B04
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

	// Token: 0x06000C76 RID: 3190 RVA: 0x00044984 File Offset: 0x00042B84
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

	public static PlayerItemManager p;

	public static UnityEvent onItemRefresh = new UnityEvent();

	public Animator animator;

	private PlayerMovement movement;

	public bool handsPreoccupied;

	public GameObject aimingCamera;

	public GameObject menuCamera;

	public bool usePersistentItems = true;

	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public ItemObject nonPersistentHat;

	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public ItemObject nonPersistentPrimary;

	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public ItemObject nonPersistentSecondary;

	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public ItemObject nonPersistentItem;

	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public int nonPersistentBraceletCount;

	public ItemObject nonPersistentItem_r;

	[Header("Equipped Items")]
	public GameObject primaryObject;

	public IItemBehaviour primaryBehaviour;

	private string primaryID;

	public GameObject secondaryObject;

	public IItemBehaviour secondaryBehaviour;

	private string secondaryID;

	public GameObject hatObject;

	public IItemBehaviour hatBehaviour;

	private string hatID;

	public GameObject itemObject;

	public IItemBehaviour itemBehaviour;

	private string itemID;

	public GameObject itemObject_r;

	public IItemBehaviour itemBehaviour_r;

	private string itemID_r;

	public IItemBehaviour itemInUse;

	private GameObject leftHandHeld;

	private GameObject rightHandHeld;

	[Header("Anchors")]
	public Transform leftHandAnchor;

	[Header("Anchors")]
	public Transform rightHandAnchor;

	public Transform shieldArmAnchor;

	public Transform shieldSledAnchor;

	public Transform shieldUnderArmAnchor;

	public Transform shieldSkateAnchor;

	public Transform shieldSledBellyAnchor;

	public Transform hatAnchor;

	public Transform gliderAnchor;

	public Transform swordUnequippedAnchor;

	public Transform shieldUnequippedAnchor;

	public Transform thrownSpawnPoint;

	public Transform hipAnchor;

	public Transform satchelAnchor;

	public Transform holsterAnchor;

	public Transform hipAnchor_r;

	public Transform satchelAnchor_r;

	public Transform holsterAnchor_r;

	public Transform chestAnchor;

	public Transform hipsAnchor;

	public Transform lowerSpineAnchor;

	public Transform shoulderAnchor;

	[Header("Weapon")]
	public Vector3 grassOffset;

	public LayerMask grassLayerMask;

	[Header("Accessories")]
	public ItemObject gliderItem;

	public GameObject moonShoes;

	public float moonShoesHeight;

	public GameObject phone;

	public GameObject[] bracelets;

	public GameObject hipSatchel;

	public GameObject hipSatchel_r;

	public GameObject bareHead;

	[Header("Misc")]
	public Rigidbody armRigidbody;

	public Rigidbody chestRigidbody;

	public Rigidbody headRigidbody;

	public ButtonTutorial primaryButtonTutorial;

	public ButtonTutorial secondaryButtonTutorial;

	public ButtonTutorial itemButtonTutorial;

	public ButtonTutorial itemButtonTutorial_r;

	private bool isAiming;

	[ReadOnly]
	public PlayerItemManager.EquippedState equippedState;

	private int framesUntilRefresh;

	public enum EquippedState
	{
		None,
		SwordAndShield,
		ShieldSled,
		Item,
		Phone,
		Gliding,
		ItemR
	}
}
