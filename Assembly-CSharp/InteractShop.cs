using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200013D RID: 317
public class InteractShop : MonoBehaviour, Interaction
{
	// Token: 0x1700005A RID: 90
	// (get) Token: 0x0600067D RID: 1661 RVA: 0x00021543 File Offset: 0x0001F743
	private string SaveID
	{
		get
		{
			return "ShopState" + this.id.ToString();
		}
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0002155C File Offset: 0x0001F75C
	private void Start()
	{
		int num = GameData.g.ReadInt(this.SaveID, 0);
		this.itemsBought = Mathf.FloorToInt((float)num / 10f);
		for (int i = 0; i < this.shopItems.Length; i++)
		{
			this.shopItems[i].visual.SetActive(i == this.itemsBought);
		}
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x000215BC File Offset: 0x0001F7BC
	public void Interact()
	{
		base.StartCoroutine(this.RunShop());
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x000215CB File Offset: 0x0001F7CB
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

	// Token: 0x040008BF RID: 2239
	public DialogueActor actor;

	// Token: 0x040008C0 RID: 2240
	public string introDialogue;

	// Token: 0x040008C1 RID: 2241
	public string returnDialogue;

	// Token: 0x040008C2 RID: 2242
	public string newItemDialogue;

	// Token: 0x040008C3 RID: 2243
	public InteractShop.ShopItem[] shopItems;

	// Token: 0x040008C4 RID: 2244
	public string buyDialogue;

	// Token: 0x040008C5 RID: 2245
	public string notEnoughMoneyDialogue;

	// Token: 0x040008C6 RID: 2246
	public string noBuyDialogue;

	// Token: 0x040008C7 RID: 2247
	public string soldOutDialogue;

	// Token: 0x040008C8 RID: 2248
	private bool introPlayed;

	// Token: 0x040008C9 RID: 2249
	private bool newItem;

	// Token: 0x040008CA RID: 2250
	private int itemsBought;

	// Token: 0x040008CB RID: 2251
	public int id;

	// Token: 0x020003B5 RID: 949
	[Serializable]
	public class ShopItem
	{
		// Token: 0x04001B8E RID: 7054
		public GameObject visual;

		// Token: 0x04001B8F RID: 7055
		public int price;

		// Token: 0x04001B90 RID: 7056
		public string dialogue;

		// Token: 0x04001B91 RID: 7057
		public string item;
	}
}
