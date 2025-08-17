using System;
using System.Collections.Generic;
using UnityEngine;

public class UIStickerMenu : MonoBehaviour
{
	// Token: 0x060013A2 RID: 5026 RVA: 0x00060234 File Offset: 0x0005E434
	private void Awake()
	{
		this.stickerDictionary = new Dictionary<string, StickerObject>();
		for (int i = 0; i < this.allStickers.Length; i++)
		{
			this.stickerDictionary.Add(this.allStickers[i].id, this.allStickers[i]);
		}
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x000109F2 File Offset: 0x0000EBF2
	private void Start()
	{
		this.LoadStickers();
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x00002229 File Offset: 0x00000429
	[ContextMenu("Load Stickers")]
	private void LoadStickers()
	{
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x00060280 File Offset: 0x0005E480
	[ContextMenu("Place All Stickers")]
	public void PlaceAllStickers()
	{
		foreach (StickerObject stickerObject in this.allStickers)
		{
			this.PlaceSticker(stickerObject, Vector2.zero, false);
		}
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x000602B4 File Offset: 0x0005E4B4
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

	// Token: 0x060013A7 RID: 5031 RVA: 0x00060300 File Offset: 0x0005E500
	private void PlaceSticker(StickerObject stickerObject, Vector2 position, bool isInteractable = true)
	{
		UISticker component = global::UnityEngine.Object.Instantiate<GameObject>(this.placedStickerPrefab, this.placedStickersParent).GetComponent<UISticker>();
		component.transform.localPosition = position;
		component.LoadSticker(stickerObject);
		component.button.interactable = isInteractable;
		this.placedStickers.Add(component);
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x00060354 File Offset: 0x0005E554
	private void ToggleInteractability(bool isInteractable)
	{
		foreach (UISticker uisticker in this.placedStickers)
		{
			uisticker.button.interactable = isInteractable;
		}
	}

	public StickerObject[] allStickers;

	private Dictionary<string, StickerObject> stickerDictionary;

	public GameObject placedStickerPrefab;

	private List<UISticker> placedStickers = new List<UISticker>();

	public RectTransform placedStickersParent;
}
