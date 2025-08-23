using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Dialogue Sequence/Attempt Purchase")]
public class DSAttemptPurchase : DSDialogue
{
	// Token: 0x06000577 RID: 1399 RVA: 0x0002EC98 File Offset: 0x0002CE98
	private new void OnValidate()
	{
		if (this.uiItemResource == null || this.uiItemResource.itemResource != this.resource)
		{
			foreach (UIItemResource uiitemResource in global::UnityEngine.Object.FindObjectsOfType<UIItemResource>(true))
			{
				if (uiitemResource.itemResource == this.resource)
				{
					this.uiItemResource = uiitemResource;
					return;
				}
			}
		}
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00005F31 File Offset: 0x00004131
	public void ShowItem()
	{
		this.resource.ForceShow = true;
		this.uiItemResource.SetPrice(this.cost);
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0002ED00 File Offset: 0x0002CF00
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

	// Token: 0x0600057A RID: 1402 RVA: 0x00005F50 File Offset: 0x00004150
	public override void Deactivate()
	{
		base.Deactivate();
		if (this.isPurchased)
		{
			this.onPurchase.Invoke();
		}
	}

	public ItemResource resource;

	public int cost;

	public int confirmPurchaseIndex;

	[ChunkLookup("document")]
	public string successfulPurchase;

	[ChunkLookup("document")]
	public string notEnough;

	[ChunkLookup("document")]
	public string cancel;

	public UnityEvent onPurchase;

	private bool isPurchased;

	public UIItemResource uiItemResource;
}
