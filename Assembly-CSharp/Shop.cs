using System;
using System.Collections;
using Rewired;
using UnityEngine;

public class Shop : MonoBehaviour
{
	// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0000D606 File Offset: 0x0000B806
	private Shop.ShopItem currentItem
	{
		get
		{
			return this.shopItems[this.displayedItems[this.selectionIndex]];
		}
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x0000D620 File Offset: 0x0000B820
	private void Awake()
	{
		this.displayedItems = new int[this.itemSlots.Length];
		this.cameraDelta = this.camera.transform.position - this.itemSlots[0].position;
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x0000D65D File Offset: 0x0000B85D
	private void Start()
	{
		this.UpdateInventory();
		base.enabled = false;
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x00051868 File Offset: 0x0004FA68
	public void Activate()
	{
		base.enabled = true;
		Game.DialogueDepth++;
		this.UpdateInventory();
		if (this.displayedItems[0] == -1)
		{
			return;
		}
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveHorizontal), 0, 3, ReInput.mapping.GetActionId("UIHorizontal"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveHorizontal), 0, 19, ReInput.mapping.GetActionId("UIHorizontal"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnSubmit), 0, 3, ReInput.mapping.GetActionId("UISubmit"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnCancel), 0, 3, ReInput.mapping.GetActionId("UICancel"));
		this.selectionIndex = 0;
		this.UpdateSelection();
		this.camera.transform.position = this.itemSlots[this.selectionIndex].position + this.cameraDelta;
		this.camera.SetActive(true);
		this.playerMark.SetActive(true);
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x000519A8 File Offset: 0x0004FBA8
	public void Deactivate()
	{
		this.camera.SetActive(false);
		Game.DialogueDepth--;
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnMoveHorizontal));
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnSubmit));
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnCancel));
		UIMenus.shop.Deactivate();
		base.enabled = false;
		this.playerMark.SetActive(false);
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x00051A30 File Offset: 0x0004FC30
	private void OnMoveHorizontal(InputActionEventData obj)
	{
		float axisRaw = obj.GetAxisRaw();
		this.MoveSelection(axisRaw > 0f);
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x0000D66C File Offset: 0x0000B86C
	private void OnCancel(InputActionEventData obj)
	{
		this.Deactivate();
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x0000D674 File Offset: 0x0000B874
	private void OnSubmit(InputActionEventData obj)
	{
		this.Buy();
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x00051A54 File Offset: 0x0004FC54
	private void Update()
	{
		this.camera.transform.position = Vector3.SmoothDamp(this.camera.transform.position, this.itemSlots[this.selectionIndex].position + this.cameraDelta, ref this.cameraVelocity, 0.25f);
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x00051AB0 File Offset: 0x0004FCB0
	public void MoveSelection(bool isRight)
	{
		this.selectionIndex += (isRight ? 1 : (-1));
		if (this.selectionIndex >= this.displayedItemCount)
		{
			this.selectionIndex = 0;
		}
		if (this.selectionIndex < 0)
		{
			this.selectionIndex = this.displayedItemCount - 1;
		}
		this.UpdateSelection();
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x0000D67C File Offset: 0x0000B87C
	private void UpdateSelection()
	{
		UIMenus.shop.Load(this.currentItem, this);
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x00051B04 File Offset: 0x0004FD04
	private void UpdateInventory()
	{
		for (int i = 0; i < this.displayedItemCount; i++)
		{
			this.shopItems[this.displayedItems[i]].gameObject.SetActive(false);
			this.displayedItems[i] = -1;
		}
		this.displayedItemCount = this.itemSlots.Length;
		for (int j = 0; j < this.itemSlots.Length; j++)
		{
			int num = 9999;
			for (int k = 0; k < this.shopItems.Length; k++)
			{
				if (!this.shopItems[k].isHidden && !this.shopItems[k].item.IsUnlocked && this.shopItems[k].priority <= num)
				{
					bool flag = false;
					for (int l = 0; l < j; l++)
					{
						if (this.displayedItems[l] == k)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this.displayedItems[j] = k;
						num = this.shopItems[k].priority;
					}
				}
			}
			if (this.displayedItems[j] == -1)
			{
				this.displayedItemCount = j;
				break;
			}
			this.shopItems[this.displayedItems[j]].gameObject.transform.position = this.itemSlots[j].transform.position;
			this.shopItems[this.displayedItems[j]].gameObject.SetActive(true);
		}
		if (this.displayedItems[0] == -1)
		{
			this.Deactivate();
			return;
		}
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x00051C8C File Offset: 0x0004FE8C
	public void Buy()
	{
		Shop.ShopItem shopItem = this.shopItems[this.displayedItems[this.selectionIndex]];
		if (this.itemResource.Amount >= shopItem.cost)
		{
			this.itemResource.Amount -= shopItem.cost;
			base.StartCoroutine(this.RunBuyItem(shopItem.item));
			return;
		}
		UIMenus.shop.SetDescription(DialogueManager.d.chunkDic[this.notEnoughChunk].lines[0].GetText(Language.Auto));
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x0000D68F File Offset: 0x0000B88F
	public IEnumerator RunBuyItem(ItemObject item)
	{
		this.Deactivate();
		Coroutine coroutine = ItemManager.i.UnlockItem(item.id);
		this.UpdateInventory();
		yield return coroutine;
		if (this.displayedItems[0] != -1)
		{
			this.Activate();
		}
		yield break;
	}

	private MultilingualTextDocument document;

	public Shop.ShopItem[] shopItems;

	public Transform[] itemSlots;

	private int[] displayedItems;

	public GameObject camera;

	public GameObject playerMark;

	private int displayedItemCount;

	private int selectionIndex;

	private Vector3 cameraDelta;

	public ItemResource itemResource;

	public string notEnoughChunk;

	public DialogueActor actor;

	private Vector3 cameraVelocity;

	private Player rePlayer;

	[Serializable]
	public struct ShopItem
	{
		[TextLookup("document")]
		public string choiceDisplay;

		public ItemObject item;

		public int cost;

		public int priority;

		public GameObject gameObject;

		[ChunkLookup("document")]
		public string unlockChunk;

		public bool isHidden;
	}
}
