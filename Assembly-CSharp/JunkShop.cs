using System;
using System.Collections;
using UnityEngine;

public class JunkShop : MonoBehaviour
{
	// Token: 0x060000E4 RID: 228 RVA: 0x0001AA64 File Offset: 0x00018C64
	private void OnValidate()
	{
		if (this.itemGet == null)
		{
			this.itemGet = global::UnityEngine.Object.FindObjectOfType<UIItemGet>();
		}
		if (this.uiItemResource == null || (this.itemResource != null && this.uiItemResource.itemResource != this.itemResource))
		{
			foreach (UIItemResource uiitemResource in global::UnityEngine.Object.FindObjectsOfType<UIItemResource>())
			{
				if (uiitemResource.itemResource == this.itemResource)
				{
					this.uiItemResource = uiitemResource;
					return;
				}
			}
		}
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00002BF6 File Offset: 0x00000DF6
	private void Start()
	{
		this.displayedItems = new int[] { -1, -1, -1 };
		this.UpdateInventory();
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00002C15 File Offset: 0x00000E15
	public void RunShopDialogue()
	{
		CoroutineUtil.Start(this.RunShopDialogueSequence());
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00002C23 File Offset: 0x00000E23
	private IEnumerator RunShopDialogueSequence()
	{
		Game.DialogueDepth++;
		this.itemResource.ForceShow = true;
		GameObject[] array = this.shopStateObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(true);
		}
		yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.shopIntro), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		DialogueManager.optionChosen = -1;
		CoroutineUtil.Start(DialogueManager.d.RunDialogueOptions(this.GetChoiceList()));
		int selectedOption = 0;
		while (DialogueManager.optionChosen == -1)
		{
			int currentlySelectedIndex = DialogueOptions.CurrentlySelectedIndex;
			for (int j = 0; j < this.cameras.Length; j++)
			{
				this.cameras[j].SetActive(j == currentlySelectedIndex - 1);
			}
			if (currentlySelectedIndex != selectedOption)
			{
				if (currentlySelectedIndex == 0)
				{
					this.uiItemResource.ClearPrice();
				}
				else
				{
					this.uiItemResource.SetPrice(this.shopItems[this.displayedItems[currentlySelectedIndex - 1]].cost);
				}
			}
			yield return null;
		}
		array = this.cameras;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		if (DialogueManager.optionChosen == 0)
		{
			this.uiItemResource.ClearPrice();
			this.itemResource.ForceShow = false;
			yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.cancelDialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		}
		else
		{
			int num = DialogueManager.optionChosen - 1;
			JunkShop.ShopItem shopItem = this.shopItems[this.displayedItems[num]];
			if (this.itemResource.Amount >= shopItem.cost)
			{
				this.uiItemResource.ClearPrice();
				this.itemResource.ForceShow = false;
				this.itemResource.Amount -= shopItem.cost;
				shopItem.item.IsUnlocked = true;
				ItemManager.i.EquipItem(shopItem.item, true);
				this.UpdateInventory();
				yield return CoroutineUtil.Start(this.itemGet.RunSequence(shopItem.item.sprite, shopItem.item.DisplayName, this.document.FetchChunk(shopItem.unlockChunk), this.actors));
			}
			else
			{
				yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.notEnoughDialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
				this.uiItemResource.ClearPrice();
				this.itemResource.ForceShow = false;
			}
		}
		array = this.shopStateObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(false);
		}
		if (this.displayedItemCount == 0)
		{
			yield return DialogueManager.d.LoadChunk(this.document.FetchChunk(this.soldOutDialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false);
			yield return this.stateMachine.ProgressState(-1);
		}
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x0001AAF4 File Offset: 0x00018CF4
	private string[] GetChoiceList()
	{
		string[] array = new string[this.displayedItemCount + 1];
		array[0] = this.document.FetchString(this.cancelChoice, Language.Auto);
		for (int i = 0; i < this.displayedItemCount; i++)
		{
			array[i + 1] = this.document.FetchString(this.shopItems[this.displayedItems[i]].choiceDisplay, Language.Auto);
		}
		return array;
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x0001AB64 File Offset: 0x00018D64
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
				return;
			}
			this.shopItems[this.displayedItems[j]].gameObject.transform.position = this.itemSlots[j].transform.position;
			this.shopItems[this.displayedItems[j]].gameObject.SetActive(true);
		}
	}

	public MultilingualTextDocument document;

	public JunkShop.ShopItem[] shopItems;

	public Transform[] itemSlots;

	private int[] displayedItems;

	private int displayedItemCount;

	[ChunkLookup("document")]
	public string shopIntro;

	[TextLookup("document")]
	public string cancelChoice;

	[ChunkLookup("document")]
	public string cancelDialogue;

	[ChunkLookup("document")]
	public string notEnoughDialogue;

	[ChunkLookup("document")]
	public string soldOutDialogue;

	public QuestStates stateMachine;

	public DialogueActor[] actors;

	public ItemResource itemResource;

	public UIItemResource uiItemResource;

	public UIItemGet itemGet;

	public GameObject[] shopStateObjects;

	public GameObject[] cameras;

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
