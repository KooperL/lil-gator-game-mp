using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020002B6 RID: 694
public class UIButton : MonoBehaviour
{
	// Token: 0x06000E98 RID: 3736 RVA: 0x00045C10 File Offset: 0x00043E10
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

	// Token: 0x06000E99 RID: 3737 RVA: 0x00045C67 File Offset: 0x00043E67
	private void OnDisable()
	{
		if (this.buttonObject != null)
		{
			this.buttonObject.onDeviceChanged.RemoveListener(new UnityAction(this.UpdateSprite));
		}
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x00045C94 File Offset: 0x00043E94
	private void UpdateSprite()
	{
		Sprite inputSprite = this.buttonObject.InputSprite;
		this.image.enabled = inputSprite != null;
		this.image.sprite = inputSprite;
	}

	// Token: 0x04001305 RID: 4869
	private Image image;

	// Token: 0x04001306 RID: 4870
	public UIButtonObject buttonObject;
}
