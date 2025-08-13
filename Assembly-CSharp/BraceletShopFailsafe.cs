using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000101 RID: 257
public class BraceletShopFailsafe : MonoBehaviour
{
	// Token: 0x060004E5 RID: 1253 RVA: 0x000058F6 File Offset: 0x00003AF6
	private void OnValidate()
	{
		if (this.uiItemGet == null)
		{
			this.uiItemGet = Object.FindObjectOfType<UIItemGet>(true);
		}
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00005912 File Offset: 0x00003B12
	public void CheckFailsafe()
	{
		if (this.braceletShop.CurrentState == 0)
		{
			this.rootObject.SetActive(true);
			CoroutineUtil.Start(this.RunShop());
			this.braceletShop.MarkSavedSecret();
		}
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00005944 File Offset: 0x00003B44
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

	// Token: 0x060004E8 RID: 1256 RVA: 0x0002C678 File Offset: 0x0002A878
	private Coroutine LoadDialogue(string dialogue)
	{
		if (this.document != null)
		{
			return base.StartCoroutine(DialogueManager.d.LoadChunk(this.document.FetchChunk(dialogue), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		}
		return base.StartCoroutine(DialogueManager.d.LoadChunk(dialogue, this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x0002C6D4 File Offset: 0x0002A8D4
	private Coroutine DoBraceletGet()
	{
		string text = this.braceletGetDialogue;
		if (this.document != null)
		{
			return base.StartCoroutine(this.uiItemGet.RunSequence(this.braceletSprite, this.braceletItem.DisplayName, this.document.FetchChunk(text), this.actors));
		}
		return base.StartCoroutine(this.uiItemGet.RunSequence(this.braceletSprite, this.braceletItem.DisplayName, text, this.actors));
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00005953 File Offset: 0x00003B53
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

	// Token: 0x040006EA RID: 1770
	public GameObject rootObject;

	// Token: 0x040006EB RID: 1771
	public DialogueActor[] actors;

	// Token: 0x040006EC RID: 1772
	public BraceletShopDialogue braceletShop;

	// Token: 0x040006ED RID: 1773
	public MultilingualTextDocument document;

	// Token: 0x040006EE RID: 1774
	public DialogueSequencer introSequence;

	// Token: 0x040006EF RID: 1775
	[ChunkLookup("document")]
	public string braceletGetDialogue;

	// Token: 0x040006F0 RID: 1776
	[ChunkLookup("document")]
	public string leaveDialogue = "BraceletShopSoldOut";

	// Token: 0x040006F1 RID: 1777
	public UIItemGet uiItemGet;

	// Token: 0x040006F2 RID: 1778
	public ItemObject braceletItem;

	// Token: 0x040006F3 RID: 1779
	public Sprite braceletSprite;

	// Token: 0x040006F4 RID: 1780
	public UnityEvent onExit;
}
