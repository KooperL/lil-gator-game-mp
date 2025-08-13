using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000BF RID: 191
public class BraceletShopDialogue : MonoBehaviour, Interaction
{
	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000419 RID: 1049 RVA: 0x00017EDE File Offset: 0x000160DE
	public int CurrentState
	{
		get
		{
			return GameData.g.ReadInt(this.SaveID, 0);
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x0600041A RID: 1050 RVA: 0x00017EF1 File Offset: 0x000160F1
	private string SaveID
	{
		get
		{
			return "BraceletShop" + this.id.ToString();
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x0600041B RID: 1051 RVA: 0x00017F08 File Offset: 0x00016108
	private bool NGP_IntroPlayed
	{
		get
		{
			return GameData.g.ReadBool("BraceletShopNGPIntro", false);
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x0600041C RID: 1052 RVA: 0x00017F1A File Offset: 0x0001611A
	private bool NGP_AllBraceletsPlayed
	{
		get
		{
			return GameData.g.ReadBool("BraceletShopNGPAllBracelets", false);
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x0600041D RID: 1053 RVA: 0x00017F2C File Offset: 0x0001612C
	// (set) Token: 0x0600041E RID: 1054 RVA: 0x00017F3E File Offset: 0x0001613E
	private int NGP_InfoGotten
	{
		get
		{
			return GameData.g.ReadInt("BraceletShopNGPInfo", 0);
		}
		set
		{
			GameData.g.Write("BraceletShopNGPInfo", value);
		}
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00017F50 File Offset: 0x00016150
	private void OnValidate()
	{
		if (this.uiItemGet == null)
		{
			this.uiItemGet = Object.FindObjectOfType<UIItemGet>(true);
		}
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00017F6C File Offset: 0x0001616C
	private void OnEnable()
	{
		this.state = this.CurrentState;
		if (this.state >= this.braceletsInStock)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x00017F94 File Offset: 0x00016194
	public void Interact()
	{
		CoroutineUtil.Start(this.RunShop());
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x00017FA2 File Offset: 0x000161A2
	private IEnumerator RunShop()
	{
		Game.DialogueDepth++;
		this.state = GameData.g.ReadInt(this.SaveID, 0);
		if (Game.IsNewGamePlus && !this.NGP_IntroPlayed)
		{
			yield return this.LoadDialogueNGP(this.ngp_intro);
			GameData.g.Write("BraceletShopNGPIntro", true);
		}
		else if (!this.introPlayed && (!Game.IsNewGamePlus || !this.ngp_skipNormalIntro))
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
		if (Game.IsNewGamePlus && ItemManager.i.BraceletsCollected == 4 && !this.NGP_AllBraceletsPlayed)
		{
			yield return this.LoadDialogueNGP(this.ngp_allBraceletsIntro);
			GameData.g.Write("BraceletShopNGPAllBracelets", true);
		}
		this.itemResource.ForceShow = true;
		int price = this.price;
		if (Game.IsNewGamePlus && ItemManager.i.BraceletsCollected == 4)
		{
			price = this.ngp_price;
			MultilingualTextDocument.SetPlaceholder(0, this.ngp_price.ToString("0"));
			yield return this.LoadDialogueNGP(this.ngp_prompt);
		}
		else
		{
			MultilingualTextDocument.SetPlaceholder(0, price.ToString("0"));
			yield return this.LoadDialogue(this.promptDialogue);
		}
		this.itemResource.ForceShow = false;
		if (DialogueManager.optionChosen == 1)
		{
			if (this.itemResource.Amount >= price)
			{
				this.itemResource.Amount -= price;
				if (!Game.IsNewGamePlus || ItemManager.i.BraceletsCollected < 4)
				{
					yield return this.LoadDialogue(this.purchaseDialogue);
					this.itemResource.ForceShow = false;
					Player.movement.Stamina = 0f;
					ItemManager i = ItemManager.i;
					int num = i.BraceletsCollected;
					i.BraceletsCollected = num + 1;
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
							if (!Game.IsNewGamePlus)
							{
								this.rewardNPC.GiveReward();
							}
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
					this.itemResource.ForceShow = false;
					this.state++;
					GameData.g.Write(this.SaveID, this.state);
					this.actors[0].showNpcMarker = false;
					Coroutine dialogue;
					switch (this.NGP_InfoGotten)
					{
					case 0:
						if (GameData.g.ReadInt("Playground_Monkey_Names", 0) == 0)
						{
							MultilingualTextDocument.SetPlaceholder(0, this.ngpDocument.FetchString(this.npg_placeholderName, Language.Auto));
						}
						else
						{
							MultilingualTextDocument.SetPlaceholder(0, this.actors[0].profile.Name);
						}
						yield return this.LoadDialogueNGP(this.ngp_info[this.NGP_InfoGotten]);
						yield return base.StartCoroutine(this.Poof());
						break;
					case 1:
					case 2:
						yield return this.LoadDialogueNGP(this.ngp_info[this.NGP_InfoGotten]);
						yield return base.StartCoroutine(this.Poof());
						break;
					case 3:
						dialogue = this.LoadDialogueNGP(this.ngp_info[this.NGP_InfoGotten]);
						yield return DialogueManager.d.waitUntilCue;
						DialogueManager.d.cue = false;
						this.ngp_sparkles.gameObject.SetActive(true);
						yield return DialogueManager.d.waitUntilCue;
						DialogueManager.d.cue = false;
						this.DisableActor();
						this.ngp_sparkleSplash.gameObject.SetActive(true);
						this.ngp_sparkles.emission.enabled = false;
						this.meshObject.SetActive(false);
						yield return dialogue;
						this.rewardNPC.GiveReward();
						GameData.g.Write(this.SaveID, this.state);
						Game.DialogueDepth--;
						base.gameObject.SetActive(false);
						yield break;
					}
					dialogue = null;
					int num = this.NGP_InfoGotten;
					this.NGP_InfoGotten = num + 1;
					if (this.CheckIfAllBraceletShops())
					{
						this.rewardNPC.GiveReward();
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

	// Token: 0x06000423 RID: 1059 RVA: 0x00017FB4 File Offset: 0x000161B4
	private Coroutine LoadDialogue(string dialogue)
	{
		if (this.document != null)
		{
			return base.StartCoroutine(DialogueManager.d.LoadChunk(this.document.FetchChunk(dialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		}
		return base.StartCoroutine(DialogueManager.d.LoadChunk(dialogue, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x00018010 File Offset: 0x00016210
	private Coroutine LoadDialogueNGP(string dialogue)
	{
		if (this.ngpDocument != null)
		{
			return base.StartCoroutine(DialogueManager.d.LoadChunk(this.ngpDocument.FetchChunk(dialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		}
		return base.StartCoroutine(DialogueManager.d.LoadChunk(dialogue, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0001806C File Offset: 0x0001626C
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

	// Token: 0x06000426 RID: 1062 RVA: 0x00018100 File Offset: 0x00016300
	private void DisableActor()
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
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x0001817C File Offset: 0x0001637C
	private IEnumerator Poof()
	{
		this.DisableActor();
		this.actors[0].SetEmote(Animator.StringToHash("LoopAction"), false);
		this.actors[0].HoldEmote();
		yield return new WaitForSeconds(0.5f);
		if (this.onExit != null)
		{
			this.onExit.Invoke();
		}
		yield break;
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0001818C File Offset: 0x0001638C
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

	// Token: 0x06000429 RID: 1065 RVA: 0x0001821F File Offset: 0x0001641F
	public void MarkSavedSecret()
	{
		GameData.g.Write(this.SaveID, this.braceletsInStock);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x00018244 File Offset: 0x00016444
	private bool CheckIfAllBraceletShops()
	{
		for (int i = 0; i < 4; i++)
		{
			if (GameData.g.ReadInt("BraceletShop" + i.ToString(), 0) == 0)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x040005BA RID: 1466
	public DialogueActor[] actors;

	// Token: 0x040005BB RID: 1467
	public NPCPlayerProximity proximity;

	// Token: 0x040005BC RID: 1468
	private int state;

	// Token: 0x040005BD RID: 1469
	public int braceletsInStock = 1;

	// Token: 0x040005BE RID: 1470
	public int price = 15;

	// Token: 0x040005BF RID: 1471
	public ItemResource itemResource;

	// Token: 0x040005C0 RID: 1472
	private bool introPlayed;

	// Token: 0x040005C1 RID: 1473
	public int id;

	// Token: 0x040005C2 RID: 1474
	[Header("Dialogue")]
	public MultilingualTextDocument document;

	// Token: 0x040005C3 RID: 1475
	[ChunkLookup("document")]
	public string introDialogue = "BraceletShopNewLocation";

	// Token: 0x040005C4 RID: 1476
	[ChunkLookup("document")]
	public string returnDialogue = "BraceletShopReturn";

	// Token: 0x040005C5 RID: 1477
	[ChunkLookup("document")]
	public string promptDialogue = "BraceletShop";

	// Token: 0x040005C6 RID: 1478
	[ChunkLookup("document")]
	public string purchaseDialogue = "BraceletShopBuy";

	// Token: 0x040005C7 RID: 1479
	[ChunkLookup("document")]
	public string notEnoughDialogue = "BraceletShopNotEnough";

	// Token: 0x040005C8 RID: 1480
	[ChunkLookup("document")]
	public string noPurchaseDialogue = "BraceletShopNoBuy";

	// Token: 0x040005C9 RID: 1481
	[ChunkLookup("document")]
	public string leaveDialogue = "BraceletShopSoldOut";

	// Token: 0x040005CA RID: 1482
	[ChunkLookup("document")]
	public string allPurchased;

	// Token: 0x040005CB RID: 1483
	[ChunkLookup("document")]
	public string afterAllPurchased;

	// Token: 0x040005CC RID: 1484
	public QuestRewardNPCs rewardNPC;

	// Token: 0x040005CD RID: 1485
	[ChunkLookup("document")]
	public string[] braceletGetDialogue;

	// Token: 0x040005CE RID: 1486
	public UIItemGet uiItemGet;

	// Token: 0x040005CF RID: 1487
	public ItemObject braceletItem;

	// Token: 0x040005D0 RID: 1488
	public Sprite[] braceletSprite;

	// Token: 0x040005D1 RID: 1489
	public UnityEvent onExit;

	// Token: 0x040005D2 RID: 1490
	[Header("NG+")]
	public int ngp_price = 1000;

	// Token: 0x040005D3 RID: 1491
	public bool ngp_skipNormalIntro;

	// Token: 0x040005D4 RID: 1492
	public MultilingualTextDocument ngpDocument;

	// Token: 0x040005D5 RID: 1493
	[ChunkLookup("ngpDocument")]
	public string ngp_intro;

	// Token: 0x040005D6 RID: 1494
	[ChunkLookup("ngpDocument")]
	public string ngp_allBraceletsIntro;

	// Token: 0x040005D7 RID: 1495
	[ChunkLookup("ngpDocument")]
	public string ngp_prompt;

	// Token: 0x040005D8 RID: 1496
	[ChunkLookup("ngpDocument")]
	public string[] ngp_info;

	// Token: 0x040005D9 RID: 1497
	[TextLookup("ngpDocument")]
	public string npg_placeholderName;

	// Token: 0x040005DA RID: 1498
	public GameObject meshObject;

	// Token: 0x040005DB RID: 1499
	public ParticleSystem ngp_sparkles;

	// Token: 0x040005DC RID: 1500
	public ParticleSystem ngp_sparkleSplash;

	// Token: 0x040005DD RID: 1501
	private const string ngpIntroPlayedKey = "BraceletShopNGPIntro";

	// Token: 0x040005DE RID: 1502
	private const string ngpAllBraceletsPlayedKey = "BraceletShopNGPAllBracelets";

	// Token: 0x040005DF RID: 1503
	private const string ngpInfoGottenKey = "BraceletShopNGPInfo";
}
