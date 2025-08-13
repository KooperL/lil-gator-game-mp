using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000FE RID: 254
public class BraceletShopDialogue : MonoBehaviour, Interaction
{
	// Token: 0x17000074 RID: 116
	// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000580A File Offset: 0x00003A0A
	public int CurrentState
	{
		get
		{
			return GameData.g.ReadInt(this.SaveID, 0);
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000581D File Offset: 0x00003A1D
	private string SaveID
	{
		get
		{
			return "BraceletShop" + this.id.ToString();
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x00005834 File Offset: 0x00003A34
	private void OnValidate()
	{
		if (this.uiItemGet == null)
		{
			this.uiItemGet = Object.FindObjectOfType<UIItemGet>(true);
		}
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x00005850 File Offset: 0x00003A50
	private void OnEnable()
	{
		this.state = this.CurrentState;
		if (this.state >= this.braceletsInStock)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x00005878 File Offset: 0x00003A78
	public void Interact()
	{
		CoroutineUtil.Start(this.RunShop());
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00005886 File Offset: 0x00003A86
	private IEnumerator RunShop()
	{
		Game.DialogueDepth++;
		this.state = GameData.g.ReadInt(this.SaveID, 0);
		if (!this.introPlayed)
		{
			this.introPlayed = true;
			if (this.introDialogue != "")
			{
				yield return this.LoadDialogue(this.introDialogue);
			}
		}
		else if (this.returnDialogue != "")
		{
			yield return this.LoadDialogue(this.returnDialogue);
		}
		this.itemResource.ForceShow = true;
		MultilingualTextDocument.SetPlaceholder(0, this.price.ToString("0"));
		yield return this.LoadDialogue(this.promptDialogue);
		this.itemResource.ForceShow = false;
		if (DialogueManager.optionChosen == 1)
		{
			if (this.itemResource.Amount >= this.price)
			{
				this.itemResource.Amount -= this.price;
				yield return this.LoadDialogue(this.purchaseDialogue);
				this.itemResource.ForceShow = false;
				Player.movement.Stamina = 0f;
				ItemManager i = ItemManager.i;
				int braceletsCollected = i.BraceletsCollected;
				i.BraceletsCollected = braceletsCollected + 1;
				yield return null;
				Player.itemManager.Refresh();
				yield return this.DoBraceletGet();
				this.state++;
				GameData.g.Write(this.SaveID, this.state);
				if (this.state >= this.braceletsInStock)
				{
					this.actors[0].showNpcMarker = false;
					if (ItemManager.i.BraceletsCollected >= 4)
					{
						Game.DialogueDepth++;
						yield return this.LoadDialogue(this.allPurchased);
						yield return base.StartCoroutine(this.Poof());
						yield return new WaitForSeconds(1.5f);
						yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.afterAllPurchased), null, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
						this.rewardNPC.GiveReward();
						Game.DialogueDepth--;
					}
					else
					{
						yield return this.LoadDialogue(this.leaveDialogue);
						yield return base.StartCoroutine(this.Poof());
					}
				}
			}
			else
			{
				yield return this.LoadDialogue(this.notEnoughDialogue);
			}
		}
		else
		{
			yield return this.LoadDialogue(this.noPurchaseDialogue);
		}
		this.itemResource.ForceShow = false;
		GameData.g.Write(this.SaveID, this.state);
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0002BF94 File Offset: 0x0002A194
	private Coroutine LoadDialogue(string dialogue)
	{
		if (this.document != null)
		{
			return base.StartCoroutine(DialogueManager.d.LoadChunk(this.document.FetchChunk(dialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		}
		return base.StartCoroutine(DialogueManager.d.LoadChunk(dialogue, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x0002BFF0 File Offset: 0x0002A1F0
	private Coroutine DoBraceletGet()
	{
		int num = ItemManager.i.BraceletsCollected - 1;
		string text = this.braceletGetDialogue[num];
		if (this.document != null)
		{
			return base.StartCoroutine(this.uiItemGet.RunSequence(this.braceletSprite[num], this.braceletItem.DisplayName, this.document.FetchChunk(text), this.actors));
		}
		return base.StartCoroutine(this.uiItemGet.RunSequence(this.braceletSprite[num], this.braceletItem.DisplayName, text, this.actors));
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00005895 File Offset: 0x00003A95
	private IEnumerator Poof()
	{
		base.GetComponent<Collider>().enabled = false;
		if (this.proximity != null)
		{
			this.proximity.gameObject.SetActive(false);
		}
		this.actors[0].LookAt = false;
		this.actors[0].LookAtDialogue = false;
		if (this.actors[0].npcMarker != null)
		{
			this.actors[0].npcMarker.SetActive(false);
		}
		this.actors[0].SetEmote(Animator.StringToHash("LoopAction"), false);
		this.actors[0].HoldEmote();
		yield return new WaitForSeconds(0.5f);
		if (this.onExit != null)
		{
			this.onExit.Invoke();
		}
		yield break;
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x0002C084 File Offset: 0x0002A284
	[ContextMenu("Assign Unique ID")]
	public void AssignUniqueID()
	{
		List<int> list = new List<int>();
		foreach (BraceletShopDialogue braceletShopDialogue in Object.FindObjectsOfType<BraceletShopDialogue>())
		{
			if (braceletShopDialogue.id != -1 && braceletShopDialogue != this && !list.Contains(braceletShopDialogue.id))
			{
				list.Add(braceletShopDialogue.id);
			}
		}
		if (this.id == -1 || list.Contains(this.id))
		{
			this.id = 0;
			while (list.Contains(this.id))
			{
				this.id++;
			}
		}
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x000058A4 File Offset: 0x00003AA4
	public void MarkSavedSecret()
	{
		GameData.g.Write(this.SaveID, this.braceletsInStock);
		base.gameObject.SetActive(false);
	}

	// Token: 0x040006CC RID: 1740
	public DialogueActor[] actors;

	// Token: 0x040006CD RID: 1741
	public NPCPlayerProximity proximity;

	// Token: 0x040006CE RID: 1742
	private int state;

	// Token: 0x040006CF RID: 1743
	public int braceletsInStock = 1;

	// Token: 0x040006D0 RID: 1744
	public int price = 15;

	// Token: 0x040006D1 RID: 1745
	public ItemResource itemResource;

	// Token: 0x040006D2 RID: 1746
	private bool introPlayed;

	// Token: 0x040006D3 RID: 1747
	public int id;

	// Token: 0x040006D4 RID: 1748
	[Header("Dialogue")]
	public MultilingualTextDocument document;

	// Token: 0x040006D5 RID: 1749
	[ChunkLookup("document")]
	public string introDialogue = "BraceletShopNewLocation";

	// Token: 0x040006D6 RID: 1750
	[ChunkLookup("document")]
	public string returnDialogue = "BraceletShopReturn";

	// Token: 0x040006D7 RID: 1751
	[ChunkLookup("document")]
	public string promptDialogue = "BraceletShop";

	// Token: 0x040006D8 RID: 1752
	[ChunkLookup("document")]
	public string purchaseDialogue = "BraceletShopBuy";

	// Token: 0x040006D9 RID: 1753
	[ChunkLookup("document")]
	public string notEnoughDialogue = "BraceletShopNotEnough";

	// Token: 0x040006DA RID: 1754
	[ChunkLookup("document")]
	public string noPurchaseDialogue = "BraceletShopNoBuy";

	// Token: 0x040006DB RID: 1755
	[ChunkLookup("document")]
	public string leaveDialogue = "BraceletShopSoldOut";

	// Token: 0x040006DC RID: 1756
	[ChunkLookup("document")]
	public string allPurchased;

	// Token: 0x040006DD RID: 1757
	[ChunkLookup("document")]
	public string afterAllPurchased;

	// Token: 0x040006DE RID: 1758
	public QuestRewardNPCs rewardNPC;

	// Token: 0x040006DF RID: 1759
	[ChunkLookup("document")]
	public string[] braceletGetDialogue;

	// Token: 0x040006E0 RID: 1760
	public UIItemGet uiItemGet;

	// Token: 0x040006E1 RID: 1761
	public ItemObject braceletItem;

	// Token: 0x040006E2 RID: 1762
	public Sprite[] braceletSprite;

	// Token: 0x040006E3 RID: 1763
	public UnityEvent onExit;
}
