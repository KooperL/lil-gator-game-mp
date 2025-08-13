using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000395 RID: 917
public class UIButton : MonoBehaviour
{
	// Token: 0x0600116A RID: 4458 RVA: 0x00057298 File Offset: 0x00055498
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

	// Token: 0x0600116B RID: 4459 RVA: 0x0000EEC1 File Offset: 0x0000D0C1
	private void OnDisable()
	{
		if (this.buttonObject != null)
		{
			this.buttonObject.onDeviceChanged.RemoveListener(new UnityAction(this.UpdateSprite));
		}
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x000572F0 File Offset: 0x000554F0
	private void UpdateSprite()
	{
		Sprite inputSprite = this.buttonObject.InputSprite;
		this.image.enabled = inputSprite != null;
		this.image.sprite = inputSprite;
	}

	// Token: 0x04001671 RID: 5745
	private Image image;

	// Token: 0x04001672 RID: 5746
	public UIButtonObject buttonObject;
}
