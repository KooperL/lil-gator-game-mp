using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003E4 RID: 996
[RequireComponent(typeof(Image))]
public class UISticker : MonoBehaviour
{
	// Token: 0x0600133F RID: 4927 RVA: 0x0001059A File Offset: 0x0000E79A
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

	// Token: 0x06001340 RID: 4928 RVA: 0x000105D0 File Offset: 0x0000E7D0
	public void LoadSticker(StickerObject stickerObject)
	{
		this.sticker = stickerObject;
		this.image.sprite = stickerObject.sprite;
		this.image.SetNativeSize();
	}

	// Token: 0x040018DA RID: 6362
	public Image image;

	// Token: 0x040018DB RID: 6363
	public Button button;

	// Token: 0x040018DC RID: 6364
	public StickerObject sticker;
}
