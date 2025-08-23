using System;
using System.Collections;
using UnityEngine;

public class ItemSearchObjects : ItemSearch<PersistentObject>
{
	// Token: 0x06000B9C RID: 2972 RVA: 0x0000AD8C File Offset: 0x00008F8C
	protected override PersistentObject[] GetList()
	{
		return CompletionStats.c.completionObjects;
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x0000AD98 File Offset: 0x00008F98
	protected override bool IsValid(PersistentObject item)
	{
		return !(item == null) && item.isPersistent && !item.PersistentState;
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x0000ADBA File Offset: 0x00008FBA
	protected override void OnUse()
	{
		if (this.isSearching || this.isRunning)
		{
			return;
		}
		CoroutineUtil.c.StartCo(this.Run());
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x0000ADDE File Offset: 0x00008FDE
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

	// Token: 0x06000BA0 RID: 2976 RVA: 0x0000ADED File Offset: 0x00008FED
	protected override void SearchResult(PersistentObject result)
	{
		this.result = result;
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x000410C8 File Offset: 0x0003F2C8
	public override void SetEquipped(bool isEquipped)
	{
		Transform transform = (this.isOnRight ? Player.itemManager.satchelAnchor_r : Player.itemManager.satchelAnchor);
		if (transform != base.transform.parent)
		{
			base.transform.ApplyParent(transform);
		}
		(this.isOnRight ? Player.itemManager.hipSatchel_r : Player.itemManager.hipSatchel).SetActive(Player.itemManager.equippedState != PlayerItemManager.EquippedState.Phone);
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x0000ADF6 File Offset: 0x00008FF6
	public override void OnRemove()
	{
		Player.actor.ClearEmote(false, false);
		base.OnRemove();
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x0000AE0A File Offset: 0x0000900A
	public override void Cancel()
	{
		Player.actor.ClearEmote(false, false);
		base.Cancel();
	}

	private bool isRunning;

	private PersistentObject result;

	public GameObject hipPhone;

	public MultilingualTextDocument document;

	[ChunkLookup("document")]
	public string foundAllBubble;
}
