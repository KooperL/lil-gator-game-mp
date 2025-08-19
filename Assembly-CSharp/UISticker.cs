using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISticker : MonoBehaviour
{
	// Token: 0x0600139F RID: 5023 RVA: 0x000109A1 File Offset: 0x0000EBA1
	private void OnValidate()
	{
		if (this.image == null)
		{
			this.image = base.GetComponent<Image>();
		}
		if (this.button == null)
		{
			this.button = base.GetComponent<Button>();
		}
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x000109D7 File Offset: 0x0000EBD7
	public void LoadSticker(StickerObject stickerObject)
	{
		this.sticker = stickerObject;
		this.image.sprite = stickerObject.sprite;
		this.image.SetNativeSize();
	}

	public Image image;

	public Button button;

	public StickerObject sticker;
}
