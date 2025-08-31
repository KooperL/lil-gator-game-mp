using System;
using System.Collections.Generic;
using UnityEngine;

public class UIStickerMenu : MonoBehaviour
{
	// Token: 0x06001015 RID: 4117 RVA: 0x0004D0D4 File Offset: 0x0004B2D4
	private void Awake()
	{
		this.stickerDictionary = new Dictionary<string, StickerObject>();
		for (int i = 0; i < this.allStickers.Length; i++)
		{
			this.stickerDictionary.Add(this.allStickers[i].id, this.allStickers[i]);
		}
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x0004D11F File Offset: 0x0004B31F
	private void Start()
	{
		this.LoadStickers();
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x0004D127 File Offset: 0x0004B327
	[ContextMenu("Load Stickers")]
	private void LoadStickers()
	{
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x0004D12C File Offset: 0x0004B32C
	[ContextMenu("Place All Stickers")]
	public void PlaceAllStickers()
	{
		foreach (StickerObject stickerObject in this.allStickers)
		{
			this.PlaceSticker(stickerObject, Vector2.zero, false);
		}
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x0004D160 File Offset: 0x0004B360
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

	// Token: 0x0600101A RID: 4122 RVA: 0x0004D1AC File Offset: 0x0004B3AC
	private void PlaceSticker(StickerObject stickerObject, Vector2 position, bool isInteractable = true)
	{
		UISticker component = Object.Instantiate<GameObject>(this.placedStickerPrefab, this.placedStickersParent).GetComponent<UISticker>();
		component.transform.localPosition = position;
		component.LoadSticker(stickerObject);
		component.button.interactable = isInteractable;
		this.placedStickers.Add(component);
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x0004D200 File Offset: 0x0004B400
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
