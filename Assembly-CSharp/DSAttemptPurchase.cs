using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000111 RID: 273
[AddComponentMenu("Dialogue Sequence/Attempt Purchase")]
public class DSAttemptPurchase : DSDialogue
{
	// Token: 0x0600053D RID: 1341 RVA: 0x0002D588 File Offset: 0x0002B788
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

	// Token: 0x0600053E RID: 1342 RVA: 0x00005C6B File Offset: 0x00003E6B
	public void ShowItem()
	{
		this.resource.ForceShow = true;
		this.uiItemResource.SetPrice(this.cost);
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
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

	// Token: 0x06000540 RID: 1344 RVA: 0x00005C8A File Offset: 0x00003E8A
	public override void Deactivate()
	{
		base.Deactivate();
		if (this.isPurchased)
		{
			this.onPurchase.Invoke();
		}
	}

	// Token: 0x0400073A RID: 1850
	public ItemResource resource;

	// Token: 0x0400073B RID: 1851
	public int cost;

	// Token: 0x0400073C RID: 1852
	public int confirmPurchaseIndex;

	// Token: 0x0400073D RID: 1853
	[ChunkLookup("document")]
	public string successfulPurchase;

	// Token: 0x0400073E RID: 1854
	[ChunkLookup("document")]
	public string notEnough;

	// Token: 0x0400073F RID: 1855
	[ChunkLookup("document")]
	public string cancel;

	// Token: 0x04000740 RID: 1856
	public UnityEvent onPurchase;

	// Token: 0x04000741 RID: 1857
	private bool isPurchased;

	// Token: 0x04000742 RID: 1858
	public UIItemResource uiItemResource;
}
