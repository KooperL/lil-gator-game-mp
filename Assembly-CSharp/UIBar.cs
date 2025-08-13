using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000390 RID: 912
public class UIBar : MonoBehaviour
{
	// Token: 0x0600115E RID: 4446 RVA: 0x00056E7C File Offset: 0x0005507C
	private void Enable()
	{
		this.leftEdge = base.transform.InverseTransformPoint(base.transform.parent.TransformPoint(new Vector2(this.rectTransform.rect.xMin, 0f)));
		this.rightEdge = base.transform.InverseTransformPoint(base.transform.parent.TransformPoint(new Vector2(this.rectTransform.rect.xMax, 0f)));
		this.leftEdge.y = (this.rightEdge.y = 0f);
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x00056F30 File Offset: 0x00055130
	public void Load(UIBar.UIBarChunk[] chunks, ItemResource newResource, UIBar.Callbacks newCallbackObject)
	{
		base.gameObject.SetActive(true);
		this.Enable();
		this.chunks = chunks;
		this.callbackObject = newCallbackObject;
		this.upgradeResource = newResource;
		while (this.dividers.Count < chunks.Length)
		{
			this.dividers.Add(Object.Instantiate<GameObject>(this.dividerPrefab, base.transform).GetComponent<UIBarDivider>());
		}
		while (this.dividers.Count > chunks.Length)
		{
			Object.Destroy(this.dividers[this.dividers.Count - 1].gameObject);
			this.dividers.RemoveAt(this.dividers.Count - 1);
		}
		int num = 0;
		bool flag = false;
		bool flag2 = false;
		int num2 = 0;
		bool flag3 = false;
		int num3 = chunks.Length;
		this.isPurchasable = false;
		for (int i = 0; i < chunks.Length; i++)
		{
			this.dividers[i].costText.text = chunks[i].cost.ToString("0");
			this.dividers[i].costObject.SetActive(chunks[i].isUnlocked && !chunks[i].isPurchased);
			this.dividers[i].lockedText.text = chunks[i].unlockCost.ToString("0");
			this.dividers[i].lockedObject.SetActive(!chunks[i].isUnlocked);
			if (chunks[i].isUnlocked && !chunks[i].isPurchased && !flag)
			{
				flag = true;
				this.dividers[i].confirmButton.gameObject.SetActive(true);
				this.dividers[i].confirmButton.onClick.AddListener(new UnityAction(this.Buy));
				this.isPurchasable = true;
			}
			else
			{
				this.dividers[i].confirmButton.gameObject.SetActive(false);
			}
			this.dividers[i].divider.SetActive(i < chunks.Length - 1);
			if (!flag2 && !chunks[i].isPurchased)
			{
				flag2 = true;
				num2 = num;
			}
			if (!flag3 && !chunks[i].isUnlocked)
			{
				flag3 = true;
				num3 = num;
			}
			num += chunks[i].cost;
		}
		if (!flag3)
		{
			num3 = num;
		}
		if (!flag2)
		{
			num2 = num;
		}
		int num4 = 0;
		for (int j = 0; j < chunks.Length; j++)
		{
			num4 += chunks[j].cost;
			this.dividers[j].rectTransform.anchoredPosition = Vector3.Lerp(this.leftEdge, this.rightEdge, (float)num4 / (float)num);
		}
		this.purchasedFill.SetInsetAndSizeFromParentEdge(1, this.rectTransform.rect.width * (float)(num - num2) / (float)num, this.rectTransform.rect.width);
		this.lockedFill.SetInsetAndSizeFromParentEdge(0, this.rectTransform.rect.width * (float)num3 / (float)num, this.rectTransform.rect.width);
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x0000EE0E File Offset: 0x0000D00E
	public void Buy()
	{
		base.gameObject.SetActive(false);
		this.callbackObject.Buy();
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x0000EE27 File Offset: 0x0000D027
	public void Cancel()
	{
		base.gameObject.SetActive(false);
		this.callbackObject.Cancel();
	}

	// Token: 0x04001658 RID: 5720
	public UIBar.UIBarChunk[] chunks;

	// Token: 0x04001659 RID: 5721
	public GameObject dividerPrefab;

	// Token: 0x0400165A RID: 5722
	private List<UIBarDivider> dividers = new List<UIBarDivider>();

	// Token: 0x0400165B RID: 5723
	public RectTransform purchasedFill;

	// Token: 0x0400165C RID: 5724
	public RectTransform lockedFill;

	// Token: 0x0400165D RID: 5725
	private RectTransform rectTransform;

	// Token: 0x0400165E RID: 5726
	private Vector3 leftEdge;

	// Token: 0x0400165F RID: 5727
	private Vector3 rightEdge;

	// Token: 0x04001660 RID: 5728
	private bool isPurchasable;

	// Token: 0x04001661 RID: 5729
	private UIBar.Callbacks callbackObject;

	// Token: 0x04001662 RID: 5730
	private ItemResource upgradeResource;

	// Token: 0x02000391 RID: 913
	[Serializable]
	public struct UIBarChunk
	{
		// Token: 0x04001663 RID: 5731
		public int unlockCost;

		// Token: 0x04001664 RID: 5732
		public bool isUnlocked;

		// Token: 0x04001665 RID: 5733
		public int cost;

		// Token: 0x04001666 RID: 5734
		public bool isPurchased;
	}

	// Token: 0x02000392 RID: 914
	public interface Callbacks
	{
		// Token: 0x06001164 RID: 4452
		void Buy();

		// Token: 0x06001165 RID: 4453
		void Cancel();
	}
}
