using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISticker : MonoBehaviour
{
	// Token: 0x06001012 RID: 4114 RVA: 0x0004D06F File Offset: 0x0004B26F
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

	// Token: 0x06001013 RID: 4115 RVA: 0x0004D0A5 File Offset: 0x0004B2A5
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
