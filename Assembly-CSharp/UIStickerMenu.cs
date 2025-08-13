using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003E5 RID: 997
public class UIStickerMenu : MonoBehaviour
{
	// Token: 0x06001342 RID: 4930 RVA: 0x0005E20C File Offset: 0x0005C40C
	private void Awake()
	{
		this.stickerDictionary = new Dictionary<string, StickerObject>();
		for (int i = 0; i < this.allStickers.Length; i++)
		{
			this.stickerDictionary.Add(this.allStickers[i].id, this.allStickers[i]);
		}
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x000105F5 File Offset: 0x0000E7F5
	private void Start()
	{
		this.LoadStickers();
	}

	// Token: 0x06001344 RID: 4932 RVA: 0x00002229 File Offset: 0x00000429
	[ContextMenu("Load Stickers")]
	private void LoadStickers()
	{
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x0005E258 File Offset: 0x0005C458
	[ContextMenu("Place All Stickers")]
	public void PlaceAllStickers()
	{
		foreach (StickerObject stickerObject in this.allStickers)
		{
			this.PlaceSticker(stickerObject, Vector2.zero, false);
		}
	}

	// Token: 0x06001346 RID: 4934 RVA: 0x0005E28C File Offset: 0x0005C48C
	[ContextMenu("Rebuild Sticker List")]
	public void RebuildStickerList()
	{
		int childCount = this.placedStickersParent.childCount;
		this.placedStickers = new List<UISticker>();
		for (int i = 0; i < childCount; i++)
		{
			this.placedStickers.Add(this.placedStickersParent.GetChild(i).GetComponent<UISticker>());
		}
	}

	// Token: 0x06001347 RID: 4935 RVA: 0x0005E2D8 File Offset: 0x0005C4D8
	private void PlaceSticker(StickerObject stickerObject, Vector2 position, bool isInteractable = true)
	{
		UISticker component = Object.Instantiate<GameObject>(this.placedStickerPrefab, this.placedStickersParent).GetComponent<UISticker>();
		component.transform.localPosition = position;
		component.LoadSticker(stickerObject);
		component.button.interactable = isInteractable;
		this.placedStickers.Add(component);
	}

	// Token: 0x06001348 RID: 4936 RVA: 0x0005E32C File Offset: 0x0005C52C
	private void ToggleInteractability(bool isInteractable)
	{
		foreach (UISticker uisticker in this.placedStickers)
		{
			uisticker.button.interactable = isInteractable;
		}
	}

	// Token: 0x040018DD RID: 6365
	public StickerObject[] allStickers;

	// Token: 0x040018DE RID: 6366
	private Dictionary<string, StickerObject> stickerDictionary;

	// Token: 0x040018DF RID: 6367
	public GameObject placedStickerPrefab;

	// Token: 0x040018E0 RID: 6368
	private List<UISticker> placedStickers = new List<UISticker>();

	// Token: 0x040018E1 RID: 6369
	public RectTransform placedStickersParent;
}
