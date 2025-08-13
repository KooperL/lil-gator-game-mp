using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003E0 RID: 992
public class UIItemDisplay : MonoBehaviour
{
	// Token: 0x06001320 RID: 4896 RVA: 0x0001040C File Offset: 0x0000E60C
	private void OnValidate()
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x0005D608 File Offset: 0x0005B808
	public void LoadItem(ItemObject item)
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		if (this.spriteDisplay != null)
		{
			this.spriteDisplay.sprite = item.sprite;
		}
		if (this.alternateDisplay != null)
		{
			this.alternateDisplay.sprite = item.sprite;
		}
		if (this.nameDisplay != null)
		{
			this.SetText(this.nameDisplay, item.Name);
		}
		if (this.description != null)
		{
			this.SetText(this.description, item.Description);
		}
		if (this.equippedObject != null || this.purchasedObject != null)
		{
			bool flag = ItemManager.i.IsEquipped(item);
			bool flag2 = false;
			if (item.itemType == ItemManager.ItemType.Item && ItemManager.i.IsEquippedAlt(item))
			{
				flag = false;
				flag2 = true;
			}
			if (this.equippedObject != null)
			{
				this.equippedObject.SetActive(flag);
			}
			if (this.equippedAltObject != null)
			{
				this.equippedAltObject.SetActive(flag2);
			}
			if (this.purchasedObject != null)
			{
				this.purchasedObject.SetActive((!flag || this.showPurchasedWhenEquipped) && item.IsUnlocked);
			}
		}
		if (this.unpurchasedOject != null)
		{
			this.unpurchasedOject.SetActive(!item.IsUnlocked);
		}
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x0005D778 File Offset: 0x0005B978
	public void ClearItem()
	{
		if (this.spriteDisplay != null)
		{
			this.spriteDisplay.sprite = null;
		}
		if (this.nameDisplay != null)
		{
			this.nameDisplay.text = "";
		}
		if (this.description != null)
		{
			this.description.text = "";
		}
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x000103D3 File Offset: 0x0000E5D3
	private void SetText(Text textObject, string textString)
	{
		if (!string.IsNullOrEmpty(textString))
		{
			textObject.gameObject.SetActive(true);
			textObject.text = textString;
			return;
		}
		textObject.gameObject.SetActive(false);
	}

	// Token: 0x0400189C RID: 6300
	public RectTransform rectTransform;

	// Token: 0x0400189D RID: 6301
	public Image spriteDisplay;

	// Token: 0x0400189E RID: 6302
	public Image alternateDisplay;

	// Token: 0x0400189F RID: 6303
	public Text nameDisplay;

	// Token: 0x040018A0 RID: 6304
	public Text description;

	// Token: 0x040018A1 RID: 6305
	public GameObject unpurchasedOject;

	// Token: 0x040018A2 RID: 6306
	public GameObject purchasedObject;

	// Token: 0x040018A3 RID: 6307
	public GameObject equippedObject;

	// Token: 0x040018A4 RID: 6308
	public GameObject equippedAltObject;

	// Token: 0x040018A5 RID: 6309
	public bool showPurchasedWhenEquipped;
}
