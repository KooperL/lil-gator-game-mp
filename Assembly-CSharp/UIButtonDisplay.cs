using System;
using Rewired;
using RewiredConsts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButtonDisplay : MonoBehaviour
{
	// Token: 0x060011CE RID: 4558 RVA: 0x0000F2C1 File Offset: 0x0000D4C1
	public void OnEnable()
	{
		if (this.updateAutomatically)
		{
			this.UpdateButtonDisplay();
			InputHelper.onLastActiveControllerChanged.AddListener(new UnityAction(this.UpdateButtonDisplay));
		}
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x0000F2E7 File Offset: 0x0000D4E7
	private void OnDisable()
	{
		if (this.updateAutomatically)
		{
			InputHelper.onLastActiveControllerChanged.RemoveListener(new UnityAction(this.UpdateButtonDisplay));
		}
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x0000F307 File Offset: 0x0000D507
	public void ClearButtonDisplay()
	{
		if (this.currentButtonDisplay != null)
		{
			global::UnityEngine.Object.Destroy(this.currentButtonDisplay);
		}
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x00059154 File Offset: 0x00057354
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

	public bool updateAutomatically = true;

	public UIButtonDisplaySettings settings;

	[ActionIdProperty(typeof(global::RewiredConsts.Action))]
	public int action;

	[HideInInspector]
	public Controller controller;

	private GameObject currentButtonDisplay;

	private ButtonDisplaySettings buttonDisplaySettings;

	public bool matchSizeOfDisplay;

	public bool constrainDisplay;

	public bool applyToLayoutElement = true;

	public bool disableMasking;

	public LayoutElement layoutElement;

	[Header("Conditional display settings")]
	public bool showForKbM = true;

	public bool showForGamepad = true;
}
