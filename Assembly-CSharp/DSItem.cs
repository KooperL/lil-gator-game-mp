using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000129 RID: 297
[AddComponentMenu("Dialogue Sequence/Item")]
public class DSItem : DialogueSequence
{
	// Token: 0x0600058D RID: 1421 RVA: 0x00005FCA File Offset: 0x000041CA
	private void OnValidate()
	{
		if (this.uiItemGet == null)
		{
			this.uiItemGet = Object.FindObjectOfType<UIItemGet>(true);
		}
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x0002E4DC File Offset: 0x0002C6DC
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

	// Token: 0x0600058F RID: 1423 RVA: 0x00005FE6 File Offset: 0x000041E6
	public override void Activate()
	{
		if (this.uiItemGet == null)
		{
			this.uiItemGet = Object.FindObjectOfType<UIItemGet>(true);
		}
		base.Activate();
		DialogueManager.d.SetDialogueCamera(this.actors);
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00006018 File Offset: 0x00004218
	public override YieldInstruction Run()
	{
		return CoroutineUtil.Start(this.RunItemSequence());
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00006025 File Offset: 0x00004225
	public IEnumerator RunItemSequence()
	{
		if ((!this.isRealItem || this.overrideName) && this.document != null && !string.IsNullOrEmpty(this.itemName_ID))
		{
			this.itemName = this.document.FetchString(this.itemName_ID, Language.English);
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

	// Token: 0x06000592 RID: 1426 RVA: 0x00006034 File Offset: 0x00004234
	public override void Deactivate()
	{
		base.Deactivate();
	}

	// Token: 0x0400079A RID: 1946
	public UIItemGet uiItemGet;

	// Token: 0x0400079B RID: 1947
	public bool isRealItem;

	// Token: 0x0400079C RID: 1948
	[ConditionalHide("isRealItem", true)]
	public ItemObject item;

	// Token: 0x0400079D RID: 1949
	[ConditionalHide("isRealItem", true)]
	public bool unlockItem;

	// Token: 0x0400079E RID: 1950
	[ConditionalHide("isRealItem", true)]
	public bool equipItem;

	// Token: 0x0400079F RID: 1951
	[ConditionalHide("isRealItem", true)]
	public bool overrideSprite;

	// Token: 0x040007A0 RID: 1952
	[ConditionalHide("isRealItem", true, InverseCondition1 = true, ConditionalSourceField2 = "overrideSprite", UseOrLogic = true)]
	public Sprite itemSprite;

	// Token: 0x040007A1 RID: 1953
	[ConditionalHide("isRealItem", true)]
	public bool overrideName;

	// Token: 0x040007A2 RID: 1954
	[ConditionalHide("isRealItem", true, InverseCondition1 = true, ConditionalSourceField2 = "overrideName", UseOrLogic = true)]
	public string itemName;

	// Token: 0x040007A3 RID: 1955
	[TextLookup("document")]
	public string itemName_ID;

	// Token: 0x040007A4 RID: 1956
	[Space]
	public MultilingualTextDocument document;

	// Token: 0x040007A5 RID: 1957
	[ChunkLookup("document")]
	public string dialogue;

	// Token: 0x040007A6 RID: 1958
	public DialogueActor[] actors;
}
