using System;
using System.Collections;
using Rewired;
using UnityEngine;

// Token: 0x0200023A RID: 570
public class Shop : MonoBehaviour
{
	// Token: 0x170000D2 RID: 210
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

	// Token: 0x0400105E RID: 4190
	private MultilingualTextDocument document;

	// Token: 0x0400105F RID: 4191
	public Shop.ShopItem[] shopItems;

	// Token: 0x04001060 RID: 4192
	public Transform[] itemSlots;

	// Token: 0x04001061 RID: 4193
	private int[] displayedItems;

	// Token: 0x04001062 RID: 4194
	public GameObject camera;

	// Token: 0x04001063 RID: 4195
	public GameObject playerMark;

	// Token: 0x04001064 RID: 4196
	private int displayedItemCount;

	// Token: 0x04001065 RID: 4197
	private int selectionIndex;

	// Token: 0x04001066 RID: 4198
	private Vector3 cameraDelta;

	// Token: 0x04001067 RID: 4199
	public ItemResource itemResource;

	// Token: 0x04001068 RID: 4200
	public string notEnoughChunk;

	// Token: 0x04001069 RID: 4201
	public DialogueActor actor;

	// Token: 0x0400106A RID: 4202
	private Vector3 cameraVelocity;

	// Token: 0x0400106B RID: 4203
	private global::Rewired.Player rePlayer;

	// Token: 0x0200041F RID: 1055
	[Serializable]
	public struct ShopItem
	{
		// Token: 0x04001D3B RID: 7483
		[TextLookup("document")]
		public string choiceDisplay;

		// Token: 0x04001D3C RID: 7484
		public ItemObject item;

		// Token: 0x04001D3D RID: 7485
		public int cost;

		// Token: 0x04001D3E RID: 7486
		public int priority;

		// Token: 0x04001D3F RID: 7487
		public GameObject gameObject;

		// Token: 0x04001D40 RID: 7488
		[ChunkLookup("document")]
		public string unlockChunk;

		// Token: 0x04001D41 RID: 7489
		public bool isHidden;
	}
}
