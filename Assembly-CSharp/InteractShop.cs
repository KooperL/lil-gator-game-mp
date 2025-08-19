using System;
using System.Collections;
using UnityEngine;

public class InteractShop : MonoBehaviour, Interaction
{
	// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00007D92 File Offset: 0x00005F92
	private string SaveID
	{
		get
		{
			return "ShopState" + this.id.ToString();
		}
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x000364FC File Offset: 0x000346FC
	private void Start()
	{
		int num = GameData.g.ReadInt(this.SaveID, 0);
		this.itemsBought = Mathf.FloorToInt((float)num / 10f);
		for (int i = 0; i < this.shopItems.Length; i++)
		{
			this.shopItems[i].visual.SetActive(i == this.itemsBought);
		}
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x00007DA9 File Offset: 0x00005FA9
	public void Interact()
	{
		base.StartCoroutine(this.RunShop());
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x00007DB8 File Offset: 0x00005FB8
	private IEnumerator RunShop()
	{
		int state = GameData.g.ReadInt(this.SaveID, 0);
		this.itemsBought = Mathf.FloorToInt((float)state / 10f);
		if (this.itemsBought < this.shopItems.Length)
		{
			if (state == 0)
			{
				state = 1;
				if (this.introDialogue != "")
				{
					yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.introDialogue, this.actor, DialogueManager.DialogueBoxBackground.Standard, true));
				}
			}
			else if (this.returnDialogue != "")
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.returnDialogue, this.actor, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			if (this.itemsBought * 10 == state && this.newItemDialogue != "")
			{
				int num = state;
				state = num + 1;
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.newItemDialogue, this.actor, DialogueManager.DialogueBoxBackground.Standard, true));
			}
			InteractShop.ShopItem shopItem = this.shopItems[this.itemsBought];
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk(shopItem.dialogue, this.actor, DialogueManager.DialogueBoxBackground.Standard, true));
			if (DialogueManager.optionChosen != 1)
			{
				yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.noBuyDialogue, this.actor, DialogueManager.DialogueBoxBackground.Standard, true));
			}
		}
		else
		{
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk(this.soldOutDialogue, this.actor, DialogueManager.DialogueBoxBackground.Standard, true));
		}
		GameData.g.Write(this.SaveID, state);
		yield break;
	}

	public DialogueActor actor;

	public string introDialogue;

	public string returnDialogue;

	public string newItemDialogue;

	public InteractShop.ShopItem[] shopItems;

	public string buyDialogue;

	public string notEnoughMoneyDialogue;

	public string noBuyDialogue;

	public string soldOutDialogue;

	private bool introPlayed;

	private bool newItem;

	private int itemsBought;

	public int id;

	[Serializable]
	public class ShopItem
	{
		public GameObject visual;

		public int price;

		public string dialogue;

		public string item;
	}
}
