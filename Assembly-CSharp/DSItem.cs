using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Item")]
public class DSItem : DialogueSequence
{
	// Token: 0x060005C7 RID: 1479 RVA: 0x00006290 File Offset: 0x00004490
	private void OnValidate()
	{
		if (this.uiItemGet == null)
		{
			this.uiItemGet = global::UnityEngine.Object.FindObjectOfType<UIItemGet>(true);
		}
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x0002FBD8 File Offset: 0x0002DDD8
	[ContextMenu("Add Name Entry")]
	public void AddNameEntry()
	{
		if (this.isRealItem && !this.overrideName)
		{
			return;
		}
		if (this.itemName_ID == "")
		{
			this.itemName_ID = "ItemName_" + this.itemName;
		}
		this.document.AddStringEntry(this.itemName_ID, this.itemName);
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x000062AC File Offset: 0x000044AC
	public override void Activate()
	{
		if (this.uiItemGet == null)
		{
			this.uiItemGet = global::UnityEngine.Object.FindObjectOfType<UIItemGet>(true);
		}
		base.Activate();
		DialogueManager.d.SetDialogueCamera(this.actors);
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x000062DE File Offset: 0x000044DE
	public override YieldInstruction Run()
	{
		return CoroutineUtil.Start(this.RunItemSequence());
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x000062EB File Offset: 0x000044EB
	public IEnumerator RunItemSequence()
	{
		if ((!this.isRealItem || this.overrideName) && this.document != null && !string.IsNullOrEmpty(this.itemName_ID))
		{
			this.itemName = this.document.FetchString(this.itemName_ID, Language.Auto);
		}
		if (this.isRealItem)
		{
			if (!this.overrideSprite)
			{
				this.itemSprite = this.item.sprite;
			}
			if (!this.overrideName)
			{
				this.itemName = this.item.DisplayName;
			}
		}
		if (this.isRealItem)
		{
			if (this.unlockItem)
			{
				this.item.IsUnlocked = true;
				PlayerItemManager.p.Refresh();
			}
			if (this.equipItem)
			{
				ItemManager.i.EquipItem(this.item, true);
			}
		}
		if (this.document == null)
		{
			yield return CoroutineUtil.Start(this.uiItemGet.RunSequence(this.itemSprite, this.itemName, this.dialogue, this.actors));
		}
		else
		{
			yield return CoroutineUtil.Start(this.uiItemGet.RunSequence(this.itemSprite, this.itemName, this.document.FetchChunk(this.dialogue), this.actors));
		}
		yield break;
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x000062FA File Offset: 0x000044FA
	public override void Deactivate()
	{
		base.Deactivate();
	}

	public UIItemGet uiItemGet;

	public bool isRealItem;

	[ConditionalHide("isRealItem", true)]
	public ItemObject item;

	[ConditionalHide("isRealItem", true)]
	public bool unlockItem;

	[ConditionalHide("isRealItem", true)]
	public bool equipItem;

	[ConditionalHide("isRealItem", true)]
	public bool overrideSprite;

	[ConditionalHide("isRealItem", true, InverseCondition1 = true, ConditionalSourceField2 = "overrideSprite", UseOrLogic = true)]
	public Sprite itemSprite;

	[ConditionalHide("isRealItem", true)]
	public bool overrideName;

	[ConditionalHide("isRealItem", true, InverseCondition1 = true, ConditionalSourceField2 = "overrideName", UseOrLogic = true)]
	public string itemName;

	[TextLookup("document")]
	public string itemName_ID;

	[Space]
	public MultilingualTextDocument document;

	[ChunkLookup("document")]
	public string dialogue;

	public DialogueActor[] actors;
}
