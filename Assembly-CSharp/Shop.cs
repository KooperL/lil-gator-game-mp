using System;
using System.Collections;
using Rewired;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class Shop : MonoBehaviour
{
	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0000D25E File Offset: 0x0000B45E
	private Shop.ShopItem currentItem
	{
		get
		{
			return this.shopItems[this.displayedItems[this.selectionIndex]];
		}
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x0000D278 File Offset: 0x0000B478
	private void Awake()
	{
		this.displayedItems = new int[this.itemSlots.Length];
		this.cameraDelta = this.camera.transform.position - this.itemSlots[0].position;
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x0000D2B5 File Offset: 0x0000B4B5
	private void Start()
	{
		this.UpdateInventory();
		base.enabled = false;
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x0004F944 File Offset: 0x0004DB44
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

	// Token: 0x06000F1A RID: 3866 RVA: 0x0004FA84 File Offset: 0x0004DC84
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

	// Token: 0x06000F1B RID: 3867 RVA: 0x0004FB0C File Offset: 0x0004DD0C
	private void OnMoveHorizontal(InputActionEventData obj)
	{
		float axisRaw = obj.GetAxisRaw();
		this.MoveSelection(axisRaw > 0f);
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x0000D2C4 File Offset: 0x0000B4C4
	private void OnCancel(InputActionEventData obj)
	{
		this.Deactivate();
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x0000D2CC File Offset: 0x0000B4CC
	private void OnSubmit(InputActionEventData obj)
	{
		this.Buy();
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x0004FB30 File Offset: 0x0004DD30
	private void Update()
	{
		this.camera.transform.position = Vector3.SmoothDamp(this.camera.transform.position, this.itemSlots[this.selectionIndex].position + this.cameraDelta, ref this.cameraVelocity, 0.25f);
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x0004FB8C File Offset: 0x0004DD8C
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

	// Token: 0x06000F20 RID: 3872 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
	private void UpdateSelection()
	{
		UIMenus.shop.Load(this.currentItem, this);
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x0004FBE0 File Offset: 0x0004DDE0
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

	// Token: 0x06000F22 RID: 3874 RVA: 0x0004FD68 File Offset: 0x0004DF68
	public void Buy()
	{
		Shop.ShopItem shopItem = this.shopItems[this.displayedItems[this.selectionIndex]];
		if (this.itemResource.Amount >= shopItem.cost)
		{
			this.itemResource.Amount -= shopItem.cost;
			base.StartCoroutine(this.RunBuyItem(shopItem.item));
			return;
		}
		UIMenus.shop.SetDescription(DialogueManager.d.chunkDic[this.notEnoughChunk].lines[0].GetText(Language.English));
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x0000D2E7 File Offset: 0x0000B4E7
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

	// Token: 0x04001366 RID: 4966
	private MultilingualTextDocument document;

	// Token: 0x04001367 RID: 4967
	public Shop.ShopItem[] shopItems;

	// Token: 0x04001368 RID: 4968
	public Transform[] itemSlots;

	// Token: 0x04001369 RID: 4969
	private int[] displayedItems;

	// Token: 0x0400136A RID: 4970
	public GameObject camera;

	// Token: 0x0400136B RID: 4971
	public GameObject playerMark;

	// Token: 0x0400136C RID: 4972
	private int displayedItemCount;

	// Token: 0x0400136D RID: 4973
	private int selectionIndex;

	// Token: 0x0400136E RID: 4974
	private Vector3 cameraDelta;

	// Token: 0x0400136F RID: 4975
	public ItemResource itemResource;

	// Token: 0x04001370 RID: 4976
	public string notEnoughChunk;

	// Token: 0x04001371 RID: 4977
	public DialogueActor actor;

	// Token: 0x04001372 RID: 4978
	private Vector3 cameraVelocity;

	// Token: 0x04001373 RID: 4979
	private Player rePlayer;

	// Token: 0x020002FC RID: 764
	[Serializable]
	public struct ShopItem
	{
		// Token: 0x04001374 RID: 4980
		[TextLookup("document")]
		public string choiceDisplay;

		// Token: 0x04001375 RID: 4981
		public ItemObject item;

		// Token: 0x04001376 RID: 4982
		public int cost;

		// Token: 0x04001377 RID: 4983
		public int priority;

		// Token: 0x04001378 RID: 4984
		public GameObject gameObject;

		// Token: 0x04001379 RID: 4985
		[ChunkLookup("document")]
		public string unlockChunk;

		// Token: 0x0400137A RID: 4986
		public bool isHidden;
	}
}
