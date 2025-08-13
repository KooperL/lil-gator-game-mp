using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000256 RID: 598
public class ItemSearchObjects : ItemSearch<PersistentObject>
{
	// Token: 0x06000B4F RID: 2895 RVA: 0x0000AA7A File Offset: 0x00008C7A
	protected override PersistentObject[] GetList()
	{
		return CompletionStats.c.completionObjects;
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x0000AA86 File Offset: 0x00008C86
	protected override bool IsValid(PersistentObject item)
	{
		return !(item == null) && item.isPersistent && !item.PersistentState;
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x0000AAA8 File Offset: 0x00008CA8
	protected override void OnUse()
	{
		if (this.isSearching || this.isRunning)
		{
			return;
		}
		CoroutineUtil.c.StartCo(this.Run());
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x0000AACC File Offset: 0x00008CCC
	private IEnumerator Run()
	{
		this.isRunning = true;
		if (CompletionStats.PercentObjects == 100)
		{
			yield return DialogueManager.d.Bubble(this.document.FetchChunk(this.foundAllBubble), null, 0f, false, true, true);
			this.isRunning = false;
			yield break;
		}
		Debug.Log("Searching...");
		Player.itemManager.SetEquippedState(PlayerItemManager.EquippedState.Phone, false);
		Player.itemManager.SetItemInUse(this, true);
		Player.actor.SetEmote(Animator.StringToHash("E_Texting"), false);
		CoroutineUtil.c.StartCo(base.RunSearch());
		yield return new WaitForSeconds(1.1f);
		Player.actor.ClearEmote(true, false);
		Player.itemManager.SetItemInUse(this, false);
		UIFindObjectMarker instance = UIFindObjectMarker.Instance;
		if (instance != null && this.result != null)
		{
			Debug.Log("Found!", this.result);
			instance.SetTarget(this.result.transform);
			yield return new WaitForSeconds(4f);
			instance.ClearTarget();
		}
		this.result = null;
		this.isRunning = false;
		yield break;
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x0000AADB File Offset: 0x00008CDB
	protected override void SearchResult(PersistentObject result)
	{
		this.result = result;
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x0003F278 File Offset: 0x0003D478
	public override void SetEquipped(bool isEquipped)
	{
		Transform transform = (this.isOnRight ? Player.itemManager.satchelAnchor_r : Player.itemManager.satchelAnchor);
		if (transform != base.transform.parent)
		{
			base.transform.ApplyParent(transform);
		}
		(this.isOnRight ? Player.itemManager.hipSatchel_r : Player.itemManager.hipSatchel).SetActive(Player.itemManager.equippedState != PlayerItemManager.EquippedState.Phone);
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x0000AAE4 File Offset: 0x00008CE4
	public override void OnRemove()
	{
		Player.actor.ClearEmote(false, false);
		base.OnRemove();
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x0000AAF8 File Offset: 0x00008CF8
	public override void Cancel()
	{
		Player.actor.ClearEmote(false, false);
		base.Cancel();
	}

	// Token: 0x04000E32 RID: 3634
	private bool isRunning;

	// Token: 0x04000E33 RID: 3635
	private PersistentObject result;

	// Token: 0x04000E34 RID: 3636
	public GameObject hipPhone;

	// Token: 0x04000E35 RID: 3637
	public MultilingualTextDocument document;

	// Token: 0x04000E36 RID: 3638
	[ChunkLookup("document")]
	public string foundAllBubble;
}
