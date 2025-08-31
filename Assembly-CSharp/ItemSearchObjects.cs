using System;
using System.Collections;
using UnityEngine;

public class ItemSearchObjects : ItemSearch<PersistentObject>
{
	// Token: 0x060009AC RID: 2476 RVA: 0x0002D300 File Offset: 0x0002B500
	protected override PersistentObject[] GetList()
	{
		return CompletionStats.c.completionObjects;
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0002D30C File Offset: 0x0002B50C
	protected override bool IsValid(PersistentObject item)
	{
		return !(item == null) && item.isPersistent && !item.PersistentState;
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x0002D32E File Offset: 0x0002B52E
	protected override void OnUse()
	{
		if (this.isSearching || this.isRunning)
		{
			return;
		}
		CoroutineUtil.c.StartCo(this.Run());
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0002D352 File Offset: 0x0002B552
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

	// Token: 0x060009B0 RID: 2480 RVA: 0x0002D361 File Offset: 0x0002B561
	protected override void SearchResult(PersistentObject result)
	{
		this.result = result;
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x0002D36C File Offset: 0x0002B56C
	public override void SetEquipped(bool isEquipped)
	{
		Transform transform = (this.isOnRight ? Player.itemManager.satchelAnchor_r : Player.itemManager.satchelAnchor);
		if (transform != base.transform.parent)
		{
			base.transform.ApplyParent(transform);
		}
		(this.isOnRight ? Player.itemManager.hipSatchel_r : Player.itemManager.hipSatchel).SetActive(Player.itemManager.equippedState != PlayerItemManager.EquippedState.Phone);
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x0002D3EB File Offset: 0x0002B5EB
	public override void OnRemove()
	{
		Player.actor.ClearEmote(false, false);
		base.OnRemove();
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0002D3FF File Offset: 0x0002B5FF
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
