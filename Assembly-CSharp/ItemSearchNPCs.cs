using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ItemSearchNPCs : ItemSearch<DialogueActor>
{
	// Token: 0x06000B8F RID: 2959 RVA: 0x0000AD15 File Offset: 0x00008F15
	protected override DialogueActor[] GetList()
	{
		return CompletionStats.c.completionActors;
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00040E28 File Offset: 0x0003F028
	protected override bool IsValid(DialogueActor item)
	{
		return !(item == null) && !item.profile.IsUnlocked && item.gameObject.activeSelf && (!(item.profile == this.stickDuckProfile) || !(item.gameObject.transform.parent != null) || item.gameObject.transform.parent.gameObject.activeSelf);
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x0000AD21 File Offset: 0x00008F21
	protected override void OnUse()
	{
		if (this.isSearching || this.isRunning)
		{
			return;
		}
		CoroutineUtil.c.StartCo(this.Run());
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x0000AD45 File Offset: 0x00008F45
	private IEnumerator Run()
	{
		this.isRunning = true;
		if (CompletionStats.PercentCharacters == 100)
		{
			DialogueManager.d.Bubble(this.document.FetchChunk(this.foundAllBubble), null, 0f, false, true, true);
			yield return this.waitForCoolDown;
			this.isRunning = false;
			yield break;
		}
		Player.itemManager.SetItemInUse(this, true);
		Player.itemManager.SetEquippedState(this.isOnRight ? PlayerItemManager.EquippedState.ItemR : PlayerItemManager.EquippedState.Item, false);
		Player.actor.SetEmote(Animator.StringToHash("E_MegaphoneShout"), false);
		this.onShout.Invoke();
		DialogueManager.d.Bubble(this.document.FetchChunk(this.callouts.RandomValue<string>()), null, 0f, true, true, true);
		yield return CoroutineUtil.c.StartCo(base.RunSearch());
		yield return this.waitForCoolDown;
		Player.itemManager.SetItemInUse(this, false);
		if (this.result != null)
		{
			DialogueManager.d.Bubble(this.document.FetchChunk(this.replies.RandomValue<string>()), new DialogueActor[] { this.result }, 0f, true, true, true);
			yield return this.waitForCoolDown;
		}
		this.result = null;
		this.isRunning = false;
		yield break;
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x0000AD54 File Offset: 0x00008F54
	protected override void SearchResult(DialogueActor result)
	{
		this.result = result;
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x00040EA8 File Offset: 0x0003F0A8
	public override void SetEquipped(bool isEquipped)
	{
		Transform transform;
		if (isEquipped)
		{
			transform = Player.itemManager.leftHandAnchor;
		}
		else
		{
			transform = (this.isOnRight ? Player.itemManager.holsterAnchor_r : Player.itemManager.holsterAnchor);
		}
		if (transform != base.transform.parent)
		{
			base.transform.ApplyParent(transform);
		}
	}

	public MultilingualTextDocument document;

	[ChunkLookup("document")]
	public string[] callouts;

	[ChunkLookup("document")]
	public string[] replies;

	[ChunkLookup("document")]
	public string foundAllBubble;

	private bool isRunning;

	private DialogueActor result;

	public UnityEvent onShout;

	private WaitForSeconds waitForCoolDown = new WaitForSeconds(0.5f);

	public CharacterProfile stickDuckProfile;
}
