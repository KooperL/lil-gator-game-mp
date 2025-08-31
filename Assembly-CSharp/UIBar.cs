using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIBar : MonoBehaviour
{
	// Token: 0x06000E8E RID: 3726 RVA: 0x00045738 File Offset: 0x00043938
	private void Enable()
	{
		this.leftEdge = base.transform.InverseTransformPoint(base.transform.parent.TransformPoint(new Vector2(this.rectTransform.rect.xMin, 0f)));
		this.rightEdge = base.transform.InverseTransformPoint(base.transform.parent.TransformPoint(new Vector2(this.rectTransform.rect.xMax, 0f)));
		this.leftEdge.y = (this.rightEdge.y = 0f);
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x000457E9 File Offset: 0x000439E9
	private void OnDisable()
	{
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x000457EC File Offset: 0x000439EC
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
		this.purchasedFill.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, this.rectTransform.rect.width * (float)(num - num2) / (float)num, this.rectTransform.rect.width);
		this.lockedFill.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, this.rectTransform.rect.width * (float)num3 / (float)num, this.rectTransform.rect.width);
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x00045B54 File Offset: 0x00043D54
	public void Buy()
	{
		base.gameObject.SetActive(false);
		this.callbackObject.Buy();
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x00045B6D File Offset: 0x00043D6D
	public void Cancel()
	{
		base.gameObject.SetActive(false);
		this.callbackObject.Cancel();
	}

	public UIBar.UIBarChunk[] chunks;

	public GameObject dividerPrefab;

	private List<UIBarDivider> dividers = new List<UIBarDivider>();

	public RectTransform purchasedFill;

	public RectTransform lockedFill;

	private RectTransform rectTransform;

	private Vector3 leftEdge;

	private Vector3 rightEdge;

	private bool isPurchasable;

	private UIBar.Callbacks callbackObject;

	private ItemResource upgradeResource;

	[Serializable]
	public struct UIBarChunk
	{
		public int unlockCost;

		public bool isUnlocked;

		public int cost;

		public bool isPurchased;
	}

	public interface Callbacks
	{
		void Buy();

		void Cancel();
	}
}
