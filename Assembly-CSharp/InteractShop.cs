using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200019B RID: 411
public class InteractShop : MonoBehaviour, Interaction
{
	// Token: 0x170000BC RID: 188
	// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00007A83 File Offset: 0x00005C83
	private string SaveID
	{
		get
		{
			return "ShopState" + this.id.ToString();
		}
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x00034BB8 File Offset: 0x00032DB8
	private void Start()
	{
		int num = GameData.g.ReadInt(this.SaveID, 0);
		this.itemsBought = Mathf.FloorToInt((float)num / 10f);
		for (int i = 0; i < this.shopItems.Length; i++)
		{
			this.shopItems[i].visual.SetActive(i == this.itemsBought);
		}
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00007A9A File Offset: 0x00005C9A
	public void Interact()
	{
		base.StartCoroutine(this.RunShop());
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00007AA9 File Offset: 0x00005CA9
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

	// Token: 0x04000A32 RID: 2610
	public DialogueActor actor;

	// Token: 0x04000A33 RID: 2611
	public string introDialogue;

	// Token: 0x04000A34 RID: 2612
	public string returnDialogue;

	// Token: 0x04000A35 RID: 2613
	public string newItemDialogue;

	// Token: 0x04000A36 RID: 2614
	public InteractShop.ShopItem[] shopItems;

	// Token: 0x04000A37 RID: 2615
	public string buyDialogue;

	// Token: 0x04000A38 RID: 2616
	public string notEnoughMoneyDialogue;

	// Token: 0x04000A39 RID: 2617
	public string noBuyDialogue;

	// Token: 0x04000A3A RID: 2618
	public string soldOutDialogue;

	// Token: 0x04000A3B RID: 2619
	private bool introPlayed;

	// Token: 0x04000A3C RID: 2620
	private bool newItem;

	// Token: 0x04000A3D RID: 2621
	private int itemsBought;

	// Token: 0x04000A3E RID: 2622
	public int id;

	// Token: 0x0200019C RID: 412
	[Serializable]
	public class ShopItem
	{
		// Token: 0x04000A3F RID: 2623
		public GameObject visual;

		// Token: 0x04000A40 RID: 2624
		public int price;

		// Token: 0x04000A41 RID: 2625
		public string dialogue;

		// Token: 0x04000A42 RID: 2626
		public string item;
	}
}
