using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BraceletShopFailsafe : MonoBehaviour
{
	// Token: 0x06000512 RID: 1298 RVA: 0x00005B64 File Offset: 0x00003D64
	private void OnValidate()
	{
		if (this.uiItemGet == null)
		{
			this.uiItemGet = global::UnityEngine.Object.FindObjectOfType<UIItemGet>(true);
		}
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00005B80 File Offset: 0x00003D80
	public void CheckFailsafe()
	{
		if (this.braceletShop.CurrentState == 0 && !Game.IsNewGamePlus)
		{
			this.rootObject.SetActive(true);
			CoroutineUtil.Start(this.RunShop());
			this.braceletShop.MarkSavedSecret();
		}
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00005BB9 File Offset: 0x00003DB9
	private IEnumerator RunShop()
	{
		Game.DialogueDepth++;
		yield return this.introSequence.StartSequence();
		Player.movement.Stamina = 0f;
		ItemManager i = ItemManager.i;
		int braceletsCollected = i.BraceletsCollected;
		i.BraceletsCollected = braceletsCollected + 1;
		yield return null;
		Player.itemManager.Refresh();
		yield return this.DoBraceletGet();
		yield return this.LoadDialogue(this.leaveDialogue);
		yield return base.StartCoroutine(this.Poof());
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x0002DAE8 File Offset: 0x0002BCE8
	private Coroutine LoadDialogue(string dialogue)
	{
		if (this.document != null)
		{
			return base.StartCoroutine(DialogueManager.d.LoadChunk(this.document.FetchChunk(dialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		}
		return base.StartCoroutine(DialogueManager.d.LoadChunk(dialogue, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x0002DB44 File Offset: 0x0002BD44
	private Coroutine DoBraceletGet()
	{
		string text = this.braceletGetDialogue;
		if (this.document != null)
		{
			return base.StartCoroutine(this.uiItemGet.RunSequence(this.braceletSprite, this.braceletItem.DisplayName, this.document.FetchChunk(text), this.actors));
		}
		return base.StartCoroutine(this.uiItemGet.RunSequence(this.braceletSprite, this.braceletItem.DisplayName, text, this.actors));
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00005BC8 File Offset: 0x00003DC8
	private IEnumerator Poof()
	{
		base.GetComponent<Collider>().enabled = false;
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

	public GameObject rootObject;

	public DialogueActor[] actors;

	public BraceletShopDialogue braceletShop;

	public MultilingualTextDocument document;

	public DialogueSequencer introSequence;

	[ChunkLookup("document")]
	public string braceletGetDialogue;

	[ChunkLookup("document")]
	public string leaveDialogue = "BraceletShopSoldOut";

	public UIItemGet uiItemGet;

	public ItemObject braceletItem;

	public Sprite braceletSprite;

	public UnityEvent onExit;
}
