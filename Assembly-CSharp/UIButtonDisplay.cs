using System;
using Rewired;
using RewiredConsts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002B7 RID: 695
public class UIButtonDisplay : MonoBehaviour
{
	// Token: 0x06000E9C RID: 3740 RVA: 0x00045CD3 File Offset: 0x00043ED3
	public void OnEnable()
	{
		if (this.updateAutomatically)
		{
			this.UpdateButtonDisplay();
			InputHelper.onLastActiveControllerChanged.AddListener(new UnityAction(this.UpdateButtonDisplay));
		}
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x00045CF9 File Offset: 0x00043EF9
	private void OnDisable()
	{
		if (this.updateAutomatically)
		{
			InputHelper.onLastActiveControllerChanged.RemoveListener(new UnityAction(this.UpdateButtonDisplay));
		}
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x00045D19 File Offset: 0x00043F19
	public void ClearButtonDisplay()
	{
		if (this.currentButtonDisplay != null)
		{
			Object.Destroy(this.currentButtonDisplay);
		}
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x00045D34 File Offset: 0x00043F34
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
			if (lastActiveControllerType > ControllerType.Mouse)
			{
				if (lastActiveControllerType == ControllerType.Joystick && !this.showForGamepad)
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
			Vector2 vector = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
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
			Vector2 vector2 = new Vector2(rectTransform3.rect.width, rectTransform3.rect.height);
			vector2 += this.buttonDisplaySettings.padding;
			rectTransform4.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vector2.x);
			rectTransform4.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, vector2.y);
			if (this.applyToLayoutElement && this.layoutElement != null)
			{
				this.layoutElement.minWidth = vector2.x;
				this.layoutElement.minHeight = vector2.y;
			}
		}
	}

	// Token: 0x04001307 RID: 4871
	public bool updateAutomatically = true;

	// Token: 0x04001308 RID: 4872
	public UIButtonDisplaySettings settings;

	// Token: 0x04001309 RID: 4873
	[ActionIdProperty(typeof(Action))]
	public int action;

	// Token: 0x0400130A RID: 4874
	[HideInInspector]
	public Controller controller;

	// Token: 0x0400130B RID: 4875
	private GameObject currentButtonDisplay;

	// Token: 0x0400130C RID: 4876
	private ButtonDisplaySettings buttonDisplaySettings;

	// Token: 0x0400130D RID: 4877
	public bool matchSizeOfDisplay;

	// Token: 0x0400130E RID: 4878
	public bool constrainDisplay;

	// Token: 0x0400130F RID: 4879
	public bool applyToLayoutElement = true;

	// Token: 0x04001310 RID: 4880
	public bool disableMasking;

	// Token: 0x04001311 RID: 4881
	public LayoutElement layoutElement;

	// Token: 0x04001312 RID: 4882
	[Header("Conditional display settings")]
	public bool showForKbM = true;

	// Token: 0x04001313 RID: 4883
	public bool showForGamepad = true;
}
