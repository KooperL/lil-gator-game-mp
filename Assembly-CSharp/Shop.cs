using System;
using System.Collections;
using Rewired;
using UnityEngine;

public class Shop : MonoBehaviour
{
	// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0003C805 File Offset: 0x0003AA05
	private Shop.ShopItem currentItem
	{
		get
		{
			return this.shopItems[this.displayedItems[this.selectionIndex]];
		}
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x0003C81F File Offset: 0x0003AA1F
	private void Awake()
	{
		this.displayedItems = new int[this.itemSlots.Length];
		this.cameraDelta = this.camera.transform.position - this.itemSlots[0].position;
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x0003C85C File Offset: 0x0003AA5C
	private void Start()
	{
		this.UpdateInventory();
		base.enabled = false;
	}

	// Token: 0x06000C73 RID: 3187 RVA: 0x0003C86C File Offset: 0x0003AA6C
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
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveHorizontal), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UIHorizontal"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveHorizontal), UpdateLoopType.Update, InputActionEventType.NegativeButtonJustPressed, ReInput.mapping.GetActionId("UIHorizontal"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnSubmit), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UISubmit"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnCancel), UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, ReInput.mapping.GetActionId("UICancel"));
		this.selectionIndex = 0;
		this.UpdateSelection();
		this.camera.transform.position = this.itemSlots[this.selectionIndex].position + this.cameraDelta;
		this.camera.SetActive(true);
		this.playerMark.SetActive(true);
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x0003C9AC File Offset: 0x0003ABAC
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

	// Token: 0x06000C75 RID: 3189 RVA: 0x0003CA34 File Offset: 0x0003AC34
	private void OnMoveHorizontal(InputActionEventData obj)
	{
		float axisRaw = obj.GetAxisRaw();
		this.MoveSelection(axisRaw > 0f);
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x0003CA57 File Offset: 0x0003AC57
	private void OnCancel(InputActionEventData obj)
	{
		this.Deactivate();
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x0003CA5F File Offset: 0x0003AC5F
	private void OnSubmit(InputActionEventData obj)
	{
		this.Buy();
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x0003CA68 File Offset: 0x0003AC68
	private void Update()
	{
		this.camera.transform.position = Vector3.SmoothDamp(this.camera.transform.position, this.itemSlots[this.selectionIndex].position + this.cameraDelta, ref this.cameraVelocity, 0.25f);
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x0003CAC4 File Offset: 0x0003ACC4
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

	// Token: 0x06000C7A RID: 3194 RVA: 0x0003CB17 File Offset: 0x0003AD17
	private void UpdateSelection()
	{
		UIMenus.shop.Load(this.currentItem, this);
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x0003CB2C File Offset: 0x0003AD2C
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

	// Token: 0x06000C7C RID: 3196 RVA: 0x0003CCB4 File Offset: 0x0003AEB4
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

	// Token: 0x06000C7D RID: 3197 RVA: 0x0003CD4A File Offset: 0x0003AF4A
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

	private global::Rewired.Player rePlayer;

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
