using System;
using UnityEngine;
using UnityEngine.UI;

public class UIItemDisplay : MonoBehaviour
{
	// Token: 0x06000FF3 RID: 4083 RVA: 0x0004C29B File Offset: 0x0004A49B
	private void OnValidate()
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x0004C2B8 File Offset: 0x0004A4B8
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

	// Token: 0x06000FF5 RID: 4085 RVA: 0x0004C428 File Offset: 0x0004A628
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

	// Token: 0x06000FF6 RID: 4086 RVA: 0x0004C48B File Offset: 0x0004A68B
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
