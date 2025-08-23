using System;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDisplay : MonoBehaviour
{
	// Token: 0x06001381 RID: 4993 RVA: 0x00010813 File Offset: 0x0000EA13
	private void OnValidate()
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x0005F8F8 File Offset: 0x0005DAF8
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

	// Token: 0x06001383 RID: 4995 RVA: 0x0005FA68 File Offset: 0x0005DC68
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

	// Token: 0x06001384 RID: 4996 RVA: 0x000107DA File Offset: 0x0000E9DA
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

	public RectTransform rectTransform;

	public Image spriteDisplay;

	public Image alternateDisplay;

	public Text nameDisplay;

	public Text description;

	public GameObject unpurchasedOject;

	public GameObject purchasedObject;

	public GameObject equippedObject;

	public GameObject equippedAltObject;

	public bool showPurchasedWhenEquipped;
}
