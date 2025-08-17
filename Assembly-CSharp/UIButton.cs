using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
	// Token: 0x060011CA RID: 4554 RVA: 0x00059258 File Offset: 0x00057458
	private void OnEnable()
	{
		if (this.image == null)
		{
			this.image = base.GetComponent<Image>();
		}
		if (this.buttonObject != null)
		{
			this.UpdateSprite();
			this.buttonObject.onDeviceChanged.AddListener(new UnityAction(this.UpdateSprite));
		}
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x0000F2AA File Offset: 0x0000D4AA
	private void OnDisable()
	{
		if (this.buttonObject != null)
		{
			this.buttonObject.onDeviceChanged.RemoveListener(new UnityAction(this.UpdateSprite));
		}
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x000592B0 File Offset: 0x000574B0
	private void UpdateSprite()
	{
		Sprite inputSprite = this.buttonObject.InputSprite;
		this.image.enabled = inputSprite != null;
		this.image.sprite = inputSprite;
	}

	private Image image;

	public UIButtonObject buttonObject;
}
