using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000CA RID: 202
[AddComponentMenu("Dialogue Sequence/Attempt Purchase")]
public class DSAttemptPurchase : DSDialogue
{
	// Token: 0x06000461 RID: 1121 RVA: 0x00018CF4 File Offset: 0x00016EF4
	private new void OnValidate()
	{
		if (this.uiItemResource == null || this.uiItemResource.itemResource != this.resource)
		{
			foreach (UIItemResource uiitemResource in Object.FindObjectsOfType<UIItemResource>(true))
			{
				if (uiitemResource.itemResource == this.resource)
				{
					this.uiItemResource = uiitemResource;
					return;
				}
			}
		}
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x00018D5B File Offset: 0x00016F5B
	public void ShowItem()
	{
		this.resource.ForceShow = true;
		this.uiItemResource.SetPrice(this.cost);
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x00018D7C File Offset: 0x00016F7C
	public override void Activate()
	{
		this.resource.ForceShow = false;
		this.uiItemResource.ClearPrice();
		if (DialogueManager.optionChosen == this.confirmPurchaseIndex)
		{
			if (this.resource.Amount >= this.cost)
			{
				this.isPurchased = true;
				this.resource.Amount -= this.cost;
				this.dialogue = this.successfulPurchase;
			}
			else
			{
				this.dialogue = this.notEnough;
			}
		}
		else
		{
			this.dialogue = this.cancel;
		}
		base.Activate();
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x00018E0D File Offset: 0x0001700D
	public override void Deactivate()
	{
		base.Deactivate();
		if (this.isPurchased)
		{
			this.onPurchase.Invoke();
		}
	}

	// Token: 0x04000622 RID: 1570
	public ItemResource resource;

	// Token: 0x04000623 RID: 1571
	public int cost;

	// Token: 0x04000624 RID: 1572
	public int confirmPurchaseIndex;

	// Token: 0x04000625 RID: 1573
	[ChunkLookup("document")]
	public string successfulPurchase;

	// Token: 0x04000626 RID: 1574
	[ChunkLookup("document")]
	public string notEnough;

	// Token: 0x04000627 RID: 1575
	[ChunkLookup("document")]
	public string cancel;

	// Token: 0x04000628 RID: 1576
	public UnityEvent onPurchase;

	// Token: 0x04000629 RID: 1577
	private bool isPurchased;

	// Token: 0x0400062A RID: 1578
	public UIItemResource uiItemResource;
}
