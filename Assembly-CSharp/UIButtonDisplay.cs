using System;
using Rewired;
using RewiredConsts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000396 RID: 918
public class UIButtonDisplay : MonoBehaviour
{
	// Token: 0x0600116E RID: 4462 RVA: 0x0000EEED File Offset: 0x0000D0ED
	public void OnEnable()
	{
		if (this.updateAutomatically)
		{
			this.UpdateButtonDisplay();
			InputHelper.onLastActiveControllerChanged.AddListener(new UnityAction(this.UpdateButtonDisplay));
		}
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x0000EF13 File Offset: 0x0000D113
	private void OnDisable()
	{
		if (this.updateAutomatically)
		{
			InputHelper.onLastActiveControllerChanged.RemoveListener(new UnityAction(this.UpdateButtonDisplay));
		}
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x0000EF33 File Offset: 0x0000D133
	public void ClearButtonDisplay()
	{
		if (this.currentButtonDisplay != null)
		{
			Object.Destroy(this.currentButtonDisplay);
		}
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x00057328 File Offset: 0x00055528
	[ContextMenu("Update Button Display")]
	public void UpdateButtonDisplay()
	{
		this.ClearButtonDisplay();
		if (this.controller != null)
		{
			this.currentButtonDisplay = this.settings.GetDisplay(ReInput.mapping.GetAction(this.action), this.controller);
		}
		else
		{
			ControllerType lastActiveControllerType = InputHelper.lastActiveControllerType;
			if (lastActiveControllerType > 1)
			{
				if (lastActiveControllerType == 2 && !this.showForGamepad)
				{
					return;
				}
			}
			else if (!this.showForKbM)
			{
				return;
			}
			this.currentButtonDisplay = this.settings.GetDisplay(ReInput.mapping.GetAction(this.action));
		}
		if (this.currentButtonDisplay == null)
		{
			return;
		}
		this.buttonDisplaySettings = this.currentButtonDisplay.GetComponent<ButtonDisplaySettings>();
		this.currentButtonDisplay.transform.SetParent(base.transform, false);
		this.currentButtonDisplay.transform.localPosition = this.buttonDisplaySettings.positionOffset;
		this.currentButtonDisplay.transform.localScale = Vector3.one;
		this.currentButtonDisplay.transform.localRotation = Quaternion.identity;
		if (this.disableMasking)
		{
			MaskableGraphic[] componentsInChildren = this.currentButtonDisplay.GetComponentsInChildren<MaskableGraphic>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].maskable = false;
			}
		}
		if (this.constrainDisplay)
		{
			RectTransform rectTransform = this.currentButtonDisplay.transform as RectTransform;
			RectTransform rectTransform2 = base.transform as RectTransform;
			rectTransform.ForceUpdateRectTransforms();
			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
			Vector2 vector;
			vector..ctor(rectTransform.rect.width, rectTransform.rect.height);
			vector += this.buttonDisplaySettings.padding;
			float num = Mathf.Min(rectTransform2.rect.size.x / vector.x, rectTransform2.rect.size.y / vector.y);
			if (num < 1f)
			{
				rectTransform.localScale = new Vector3(num, num, 1f);
			}
		}
		if (this.matchSizeOfDisplay)
		{
			RectTransform rectTransform3 = this.currentButtonDisplay.transform as RectTransform;
			RectTransform rectTransform4 = base.transform as RectTransform;
			rectTransform3.ForceUpdateRectTransforms();
			LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform3);
			Vector2 vector2;
			vector2..ctor(rectTransform3.rect.width, rectTransform3.rect.height);
			vector2 += this.buttonDisplaySettings.padding;
			rectTransform4.SetSizeWithCurrentAnchors(0, vector2.x);
			rectTransform4.SetSizeWithCurrentAnchors(1, vector2.y);
			if (this.applyToLayoutElement && this.layoutElement != null)
			{
				this.layoutElement.minWidth = vector2.x;
				this.layoutElement.minHeight = vector2.y;
			}
		}
	}

	// Token: 0x04001673 RID: 5747
	public bool updateAutomatically = true;

	// Token: 0x04001674 RID: 5748
	public UIButtonDisplaySettings settings;

	// Token: 0x04001675 RID: 5749
	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int action;

	// Token: 0x04001676 RID: 5750
	[HideInInspector]
	public Controller controller;

	// Token: 0x04001677 RID: 5751
	private GameObject currentButtonDisplay;

	// Token: 0x04001678 RID: 5752
	private ButtonDisplaySettings buttonDisplaySettings;

	// Token: 0x04001679 RID: 5753
	public bool matchSizeOfDisplay;

	// Token: 0x0400167A RID: 5754
	public bool constrainDisplay;

	// Token: 0x0400167B RID: 5755
	public bool applyToLayoutElement = true;

	// Token: 0x0400167C RID: 5756
	public bool disableMasking;

	// Token: 0x0400167D RID: 5757
	public LayoutElement layoutElement;

	// Token: 0x0400167E RID: 5758
	[Header("Conditional display settings")]
	public bool showForKbM = true;

	// Token: 0x0400167F RID: 5759
	public bool showForGamepad = true;
}
