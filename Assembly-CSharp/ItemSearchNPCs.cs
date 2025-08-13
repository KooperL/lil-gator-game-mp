using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000254 RID: 596
public class ItemSearchNPCs : ItemSearch<DialogueActor>
{
	// Token: 0x06000B42 RID: 2882 RVA: 0x0000A9D7 File Offset: 0x00008BD7
	protected override DialogueActor[] GetList()
	{
		return CompletionStats.c.completionActors;
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x0000A9E3 File Offset: 0x00008BE3
	protected override bool IsValid(DialogueActor item)
	{
		return !(item == null) && !item.profile.IsUnlocked && item.gameObject.activeSelf;
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x0000AA0F File Offset: 0x00008C0F
	protected override void OnUse()
	{
		if (this.isSearching || this.isRunning)
		{
			return;
		}
		CoroutineUtil.c.StartCo(this.Run());
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x0000AA33 File Offset: 0x00008C33
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

	// Token: 0x06000B46 RID: 2886 RVA: 0x0000AA42 File Offset: 0x00008C42
	protected override void SearchResult(DialogueActor result)
	{
		this.result = result;
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x0003F058 File Offset: 0x0003D258
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

	// Token: 0x04000E27 RID: 3623
	public MultilingualTextDocument document;

	// Token: 0x04000E28 RID: 3624
	[ChunkLookup("document")]
	public string[] callouts;

	// Token: 0x04000E29 RID: 3625
	[ChunkLookup("document")]
	public string[] replies;

	// Token: 0x04000E2A RID: 3626
	[ChunkLookup("document")]
	public string foundAllBubble;

	// Token: 0x04000E2B RID: 3627
	private bool isRunning;

	// Token: 0x04000E2C RID: 3628
	private DialogueActor result;

	// Token: 0x04000E2D RID: 3629
	public UnityEvent onShout;

	// Token: 0x04000E2E RID: 3630
	private WaitForSeconds waitForCoolDown = new WaitForSeconds(0.5f);
}
