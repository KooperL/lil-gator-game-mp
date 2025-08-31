using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BraceletShopFailsafe : MonoBehaviour
{
	// Token: 0x0600042C RID: 1068 RVA: 0x000182FA File Offset: 0x000164FA
	private void OnValidate()
	{
		if (this.uiItemGet == null)
		{
			this.uiItemGet = Object.FindObjectOfType<UIItemGet>(true);
		}
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00018316 File Offset: 0x00016516
	public void CheckFailsafe()
	{
		if (this.braceletShop.CurrentState == 0 && !Game.IsNewGamePlus)
		{
			this.rootObject.SetActive(true);
			CoroutineUtil.Start(this.RunShop());
			this.braceletShop.MarkSavedSecret();
		}
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x0001834F File Offset: 0x0001654F
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

	// Token: 0x0600042F RID: 1071 RVA: 0x00018360 File Offset: 0x00016560
	private Coroutine LoadDialogue(string dialogue)
	{
		if (this.document != null)
		{
			return base.StartCoroutine(DialogueManager.d.LoadChunk(this.document.FetchChunk(dialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		}
		return base.StartCoroutine(DialogueManager.d.LoadChunk(dialogue, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x000183BC File Offset: 0x000165BC
	private Coroutine DoBraceletGet()
	{
		string text = this.braceletGetDialogue;
		if (this.document != null)
		{
			return base.StartCoroutine(this.uiItemGet.RunSequence(this.braceletSprite, this.braceletItem.DisplayName, this.document.FetchChunk(text), this.actors));
		}
		return base.StartCoroutine(this.uiItemGet.RunSequence(this.braceletSprite, this.braceletItem.DisplayName, text, this.actors));
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x0001843C File Offset: 0x0001663C
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
