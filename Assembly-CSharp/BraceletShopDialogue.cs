using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BraceletShopDialogue : MonoBehaviour, Interaction
{
	// (get) Token: 0x06000419 RID: 1049 RVA: 0x00017EDE File Offset: 0x000160DE
	public int CurrentState
	{
		get
		{
			return GameData.g.ReadInt(this.SaveID, 0);
		}
	}

	// (get) Token: 0x0600041A RID: 1050 RVA: 0x00017EF1 File Offset: 0x000160F1
	private string SaveID
	{
		get
		{
			return "BraceletShop" + this.id.ToString();
		}
	}

	// (get) Token: 0x0600041B RID: 1051 RVA: 0x00017F08 File Offset: 0x00016108
	private bool NGP_IntroPlayed
	{
		get
		{
			return GameData.g.ReadBool("BraceletShopNGPIntro", false);
		}
	}

	// (get) Token: 0x0600041C RID: 1052 RVA: 0x00017F1A File Offset: 0x0001611A
	private bool NGP_AllBraceletsPlayed
	{
		get
		{
			return GameData.g.ReadBool("BraceletShopNGPAllBracelets", false);
		}
	}

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

	public DialogueActor[] actors;

	public NPCPlayerProximity proximity;

	private int state;

	public int braceletsInStock = 1;

	public int price = 15;

	public ItemResource itemResource;

	private bool introPlayed;

	public int id;

	[Header("Dialogue")]
	public MultilingualTextDocument document;

	[ChunkLookup("document")]
	public string introDialogue = "BraceletShopNewLocation";

	[ChunkLookup("document")]
	public string returnDialogue = "BraceletShopReturn";

	[ChunkLookup("document")]
	public string promptDialogue = "BraceletShop";

	[ChunkLookup("document")]
	public string purchaseDialogue = "BraceletShopBuy";

	[ChunkLookup("document")]
	public string notEnoughDialogue = "BraceletShopNotEnough";

	[ChunkLookup("document")]
	public string noPurchaseDialogue = "BraceletShopNoBuy";

	[ChunkLookup("document")]
	public string leaveDialogue = "BraceletShopSoldOut";

	[ChunkLookup("document")]
	public string allPurchased;

	[ChunkLookup("document")]
	public string afterAllPurchased;

	public QuestRewardNPCs rewardNPC;

	[ChunkLookup("document")]
	public string[] braceletGetDialogue;

	public UIItemGet uiItemGet;

	public ItemObject braceletItem;

	public Sprite[] braceletSprite;

	public UnityEvent onExit;

	[Header("NG+")]
	public int ngp_price = 1000;

	public bool ngp_skipNormalIntro;

	public MultilingualTextDocument ngpDocument;

	[ChunkLookup("ngpDocument")]
	public string ngp_intro;

	[ChunkLookup("ngpDocument")]
	public string ngp_allBraceletsIntro;

	[ChunkLookup("ngpDocument")]
	public string ngp_prompt;

	[ChunkLookup("ngpDocument")]
	public string[] ngp_info;

	[TextLookup("ngpDocument")]
	public string npg_placeholderName;

	public GameObject meshObject;

	public ParticleSystem ngp_sparkles;

	public ParticleSystem ngp_sparkleSplash;

	private const string ngpIntroPlayedKey = "BraceletShopNGPIntro";

	private const string ngpAllBraceletsPlayedKey = "BraceletShopNGPAllBracelets";

	private const string ngpInfoGottenKey = "BraceletShopNGPInfo";
}
