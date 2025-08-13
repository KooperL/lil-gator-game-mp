using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000275 RID: 629
public class PlayerItemManager : MonoBehaviour
{
	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0000B5B1 File Offset: 0x000097B1
	public bool PrimaryInUse
	{
		get
		{
			return this.primaryBehaviour != null && this.itemInUse == this.primaryBehaviour;
		}
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0000B5CB File Offset: 0x000097CB
	public bool SecondaryInUse
	{
		get
		{
			return this.secondaryBehaviour != null && this.itemInUse == this.secondaryBehaviour;
		}
	}

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0000B5E5 File Offset: 0x000097E5
	public bool IsAnyItemInUse
	{
		get
		{
			return this.itemBehaviour != null && this.itemInUse == this.itemBehaviour;
		}
	}

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0000B5E5 File Offset: 0x000097E5
	public bool IsItemInUse
	{
		get
		{
			return this.itemBehaviour != null && this.itemInUse == this.itemBehaviour;
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0000B5FF File Offset: 0x000097FF
	public bool IsItemInUse_R
	{
		get
		{
			return this.itemBehaviour_r != null && this.itemInUse == this.itemBehaviour_r;
		}
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x0000B619 File Offset: 0x00009819
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

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0000B652 File Offset: 0x00009852
	public bool RightHandBusy
	{
		get
		{
			return this.equippedState == PlayerItemManager.EquippedState.ShieldSled || this.equippedState == PlayerItemManager.EquippedState.SwordAndShield || Player.actorStates.isFidgeting;
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0000B652 File Offset: 0x00009852
	public bool LeftHandBusy
	{
		get
		{
			return this.equippedState == PlayerItemManager.EquippedState.ShieldSled || this.equippedState == PlayerItemManager.EquippedState.SwordAndShield || Player.actorStates.isFidgeting;
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0000B672 File Offset: 0x00009872
	// (set) Token: 0x06000C1B RID: 3099 RVA: 0x0000B67A File Offset: 0x0000987A
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

	// Token: 0x06000C1C RID: 3100 RVA: 0x0000B68E File Offset: 0x0000988E
	private void Awake()
	{
		this.movement = base.GetComponent<PlayerMovement>();
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x0000B69C File Offset: 0x0000989C
	private void OnEnable()
	{
		PlayerItemManager.p = this;
		this.Refresh();
		this.framesUntilRefresh = 2;
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x0000B6B1 File Offset: 0x000098B1
	private void Start()
	{
		this.bareHead.SetActive(false);
		this.framesUntilRefresh = 2;
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00042594 File Offset: 0x00040794
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

	// Token: 0x06000C20 RID: 3104 RVA: 0x000428E4 File Offset: 0x00040AE4
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

	// Token: 0x06000C21 RID: 3105 RVA: 0x000429CC File Offset: 0x00040BCC
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

	// Token: 0x06000C22 RID: 3106 RVA: 0x00042A8C File Offset: 0x00040C8C
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

	// Token: 0x06000C23 RID: 3107 RVA: 0x00042BC4 File Offset: 0x00040DC4
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

	// Token: 0x06000C24 RID: 3108 RVA: 0x0000B6C6 File Offset: 0x000098C6
	private void ClearLeft()
	{
		if (this.leftHandHeld != null)
		{
			this.leftHandHeld.SetActive(false);
		}
		this.leftHandHeld = null;
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x00042C1C File Offset: 0x00040E1C
	public void CutGrass()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.TransformPoint(this.grassOffset), Vector3.down, ref raycastHit, 2f, this.grassLayerMask))
		{
			TerrainDetails component = raycastHit.collider.GetComponent<TerrainDetails>();
			if (component != null)
			{
				component.ClearDetailsBox(base.transform, new Vector2(-1.5f, -0.5f), new Vector2(1.5f, 1.5f), 0.5f);
			}
		}
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x0000B6E9 File Offset: 0x000098E9
	public void EquipPhone()
	{
		this.SetEquippedState(PlayerItemManager.EquippedState.Phone, false);
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x00042CA0 File Offset: 0x00040EA0
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

	// Token: 0x06000C28 RID: 3112 RVA: 0x00042D20 File Offset: 0x00040F20
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

	// Token: 0x06000C29 RID: 3113 RVA: 0x00042DA0 File Offset: 0x00040FA0
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

	// Token: 0x06000C2A RID: 3114 RVA: 0x00042E20 File Offset: 0x00041020
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

	// Token: 0x04000F5C RID: 3932
	public static PlayerItemManager p;

	// Token: 0x04000F5D RID: 3933
	public static UnityEvent onItemRefresh = new UnityEvent();

	// Token: 0x04000F5E RID: 3934
	public Animator animator;

	// Token: 0x04000F5F RID: 3935
	private PlayerMovement movement;

	// Token: 0x04000F60 RID: 3936
	public bool handsPreoccupied;

	// Token: 0x04000F61 RID: 3937
	public GameObject aimingCamera;

	// Token: 0x04000F62 RID: 3938
	public GameObject menuCamera;

	// Token: 0x04000F63 RID: 3939
	public bool usePersistentItems = true;

	// Token: 0x04000F64 RID: 3940
	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public ItemObject nonPersistentHat;

	// Token: 0x04000F65 RID: 3941
	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public ItemObject nonPersistentPrimary;

	// Token: 0x04000F66 RID: 3942
	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public ItemObject nonPersistentSecondary;

	// Token: 0x04000F67 RID: 3943
	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public ItemObject nonPersistentItem;

	// Token: 0x04000F68 RID: 3944
	[ConditionalHide("usePersistentItems", true, Inverse = true)]
	public int nonPersistentBraceletCount;

	// Token: 0x04000F69 RID: 3945
	public ItemObject nonPersistentItem_r;

	// Token: 0x04000F6A RID: 3946
	[Header("Equipped Items")]
	public GameObject primaryObject;

	// Token: 0x04000F6B RID: 3947
	public IItemBehaviour primaryBehaviour;

	// Token: 0x04000F6C RID: 3948
	private string primaryID;

	// Token: 0x04000F6D RID: 3949
	public GameObject secondaryObject;

	// Token: 0x04000F6E RID: 3950
	public IItemBehaviour secondaryBehaviour;

	// Token: 0x04000F6F RID: 3951
	private string secondaryID;

	// Token: 0x04000F70 RID: 3952
	public GameObject hatObject;

	// Token: 0x04000F71 RID: 3953
	public IItemBehaviour hatBehaviour;

	// Token: 0x04000F72 RID: 3954
	private string hatID;

	// Token: 0x04000F73 RID: 3955
	public GameObject itemObject;

	// Token: 0x04000F74 RID: 3956
	public IItemBehaviour itemBehaviour;

	// Token: 0x04000F75 RID: 3957
	private string itemID;

	// Token: 0x04000F76 RID: 3958
	public GameObject itemObject_r;

	// Token: 0x04000F77 RID: 3959
	public IItemBehaviour itemBehaviour_r;

	// Token: 0x04000F78 RID: 3960
	private string itemID_r;

	// Token: 0x04000F79 RID: 3961
	public IItemBehaviour itemInUse;

	// Token: 0x04000F7A RID: 3962
	private GameObject leftHandHeld;

	// Token: 0x04000F7B RID: 3963
	private GameObject rightHandHeld;

	// Token: 0x04000F7C RID: 3964
	[Header("Anchors")]
	public Transform leftHandAnchor;

	// Token: 0x04000F7D RID: 3965
	[Header("Anchors")]
	public Transform rightHandAnchor;

	// Token: 0x04000F7E RID: 3966
	public Transform shieldArmAnchor;

	// Token: 0x04000F7F RID: 3967
	public Transform shieldSledAnchor;

	// Token: 0x04000F80 RID: 3968
	public Transform shieldUnderArmAnchor;

	// Token: 0x04000F81 RID: 3969
	public Transform shieldSkateAnchor;

	// Token: 0x04000F82 RID: 3970
	public Transform shieldSledBellyAnchor;

	// Token: 0x04000F83 RID: 3971
	public Transform hatAnchor;

	// Token: 0x04000F84 RID: 3972
	public Transform gliderAnchor;

	// Token: 0x04000F85 RID: 3973
	public Transform swordUnequippedAnchor;

	// Token: 0x04000F86 RID: 3974
	public Transform shieldUnequippedAnchor;

	// Token: 0x04000F87 RID: 3975
	public Transform thrownSpawnPoint;

	// Token: 0x04000F88 RID: 3976
	public Transform hipAnchor;

	// Token: 0x04000F89 RID: 3977
	public Transform satchelAnchor;

	// Token: 0x04000F8A RID: 3978
	public Transform holsterAnchor;

	// Token: 0x04000F8B RID: 3979
	public Transform hipAnchor_r;

	// Token: 0x04000F8C RID: 3980
	public Transform satchelAnchor_r;

	// Token: 0x04000F8D RID: 3981
	public Transform holsterAnchor_r;

	// Token: 0x04000F8E RID: 3982
	public Transform chestAnchor;

	// Token: 0x04000F8F RID: 3983
	public Transform hipsAnchor;

	// Token: 0x04000F90 RID: 3984
	public Transform lowerSpineAnchor;

	// Token: 0x04000F91 RID: 3985
	public Transform shoulderAnchor;

	// Token: 0x04000F92 RID: 3986
	[Header("Weapon")]
	public Vector3 grassOffset;

	// Token: 0x04000F93 RID: 3987
	public LayerMask grassLayerMask;

	// Token: 0x04000F94 RID: 3988
	[Header("Accessories")]
	public ItemObject gliderItem;

	// Token: 0x04000F95 RID: 3989
	public GameObject moonShoes;

	// Token: 0x04000F96 RID: 3990
	public float moonShoesHeight;

	// Token: 0x04000F97 RID: 3991
	public GameObject phone;

	// Token: 0x04000F98 RID: 3992
	public GameObject[] bracelets;

	// Token: 0x04000F99 RID: 3993
	public GameObject hipSatchel;

	// Token: 0x04000F9A RID: 3994
	public GameObject hipSatchel_r;

	// Token: 0x04000F9B RID: 3995
	public GameObject bareHead;

	// Token: 0x04000F9C RID: 3996
	[Header("Misc")]
	public Rigidbody armRigidbody;

	// Token: 0x04000F9D RID: 3997
	public Rigidbody chestRigidbody;

	// Token: 0x04000F9E RID: 3998
	public Rigidbody headRigidbody;

	// Token: 0x04000F9F RID: 3999
	public ButtonTutorial primaryButtonTutorial;

	// Token: 0x04000FA0 RID: 4000
	public ButtonTutorial secondaryButtonTutorial;

	// Token: 0x04000FA1 RID: 4001
	public ButtonTutorial itemButtonTutorial;

	// Token: 0x04000FA2 RID: 4002
	public ButtonTutorial itemButtonTutorial_r;

	// Token: 0x04000FA3 RID: 4003
	private bool isAiming;

	// Token: 0x04000FA4 RID: 4004
	[ReadOnly]
	public PlayerItemManager.EquippedState equippedState;

	// Token: 0x04000FA5 RID: 4005
	private int framesUntilRefresh;

	// Token: 0x02000276 RID: 630
	public enum EquippedState
	{
		// Token: 0x04000FA7 RID: 4007
		None,
		// Token: 0x04000FA8 RID: 4008
		SwordAndShield,
		// Token: 0x04000FA9 RID: 4009
		ShieldSled,
		// Token: 0x04000FAA RID: 4010
		Item,
		// Token: 0x04000FAB RID: 4011
		Phone,
		// Token: 0x04000FAC RID: 4012
		Gliding,
		// Token: 0x04000FAD RID: 4013
		ItemR
	}
}
