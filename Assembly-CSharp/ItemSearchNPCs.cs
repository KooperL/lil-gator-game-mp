using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001D0 RID: 464
public class ItemSearchNPCs : ItemSearch<DialogueActor>
{
	// Token: 0x060009A5 RID: 2469 RVA: 0x0002D1C4 File Offset: 0x0002B3C4
	protected override DialogueActor[] GetList()
	{
		return CompletionStats.c.completionActors;
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x0002D1D0 File Offset: 0x0002B3D0
	protected override bool IsValid(DialogueActor item)
	{
		return !(item == null) && !item.profile.IsUnlocked && item.gameObject.activeSelf && (!(item.profile == this.stickDuckProfile) || !(item.gameObject.transform.parent != null) || item.gameObject.transform.parent.gameObject.activeSelf);
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0002D250 File Offset: 0x0002B450
	protected override void OnUse()
	{
		if (this.isSearching || this.isRunning)
		{
			return;
		}
		CoroutineUtil.c.StartCo(this.Run());
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0002D274 File Offset: 0x0002B474
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

	// Token: 0x060009A9 RID: 2473 RVA: 0x0002D283 File Offset: 0x0002B483
	protected override void SearchResult(DialogueActor result)
	{
		this.result = result;
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0002D28C File Offset: 0x0002B48C
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

	// Token: 0x04000BF9 RID: 3065
	public MultilingualTextDocument document;

	// Token: 0x04000BFA RID: 3066
	[ChunkLookup("document")]
	public string[] callouts;

	// Token: 0x04000BFB RID: 3067
	[ChunkLookup("document")]
	public string[] replies;

	// Token: 0x04000BFC RID: 3068
	[ChunkLookup("document")]
	public string foundAllBubble;

	// Token: 0x04000BFD RID: 3069
	private bool isRunning;

	// Token: 0x04000BFE RID: 3070
	private DialogueActor result;

	// Token: 0x04000BFF RID: 3071
	public UnityEvent onShout;

	// Token: 0x04000C00 RID: 3072
	private WaitForSeconds waitForCoolDown = new WaitForSeconds(0.5f);

	// Token: 0x04000C01 RID: 3073
	public CharacterProfile stickDuckProfile;
}
