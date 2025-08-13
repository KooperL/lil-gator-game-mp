using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002EE RID: 750
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

	// Token: 0x040014DD RID: 5341
	public RectTransform rectTransform;

	// Token: 0x040014DE RID: 5342
	public Image spriteDisplay;

	// Token: 0x040014DF RID: 5343
	public Image alternateDisplay;

	// Token: 0x040014E0 RID: 5344
	public Text nameDisplay;

	// Token: 0x040014E1 RID: 5345
	public Text description;

	// Token: 0x040014E2 RID: 5346
	public GameObject unpurchasedOject;

	// Token: 0x040014E3 RID: 5347
	public GameObject purchasedObject;

	// Token: 0x040014E4 RID: 5348
	public GameObject equippedObject;

	// Token: 0x040014E5 RID: 5349
	public GameObject equippedAltObject;

	// Token: 0x040014E6 RID: 5350
	public bool showPurchasedWhenEquipped;
}
