using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("Dialogue Sequence/Item")]
public class DSItem : DialogueSequence
{
	// Token: 0x06000496 RID: 1174 RVA: 0x00019A50 File Offset: 0x00017C50
	private void OnValidate()
	{
		if (this.uiItemGet == null)
		{
			this.uiItemGet = Object.FindObjectOfType<UIItemGet>(true);
		}
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00019A6C File Offset: 0x00017C6C
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

	// Token: 0x06000498 RID: 1176 RVA: 0x00019AC9 File Offset: 0x00017CC9
	public override void Activate()
	{
		if (this.uiItemGet == null)
		{
			this.uiItemGet = Object.FindObjectOfType<UIItemGet>(true);
		}
		base.Activate();
		DialogueManager.d.SetDialogueCamera(this.actors);
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x00019AFB File Offset: 0x00017CFB
	public override YieldInstruction Run()
	{
		return CoroutineUtil.Start(this.RunItemSequence());
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x00019B08 File Offset: 0x00017D08
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

	// Token: 0x0600049B RID: 1179 RVA: 0x00019B17 File Offset: 0x00017D17
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
