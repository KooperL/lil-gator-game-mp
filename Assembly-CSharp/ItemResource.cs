using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200007F RID: 127
[CreateAssetMenu]
public class ItemResource : ScriptableObject
{
	// Token: 0x1700001C RID: 28
	// (get) Token: 0x0600019C RID: 412 RVA: 0x00003557 File Offset: 0x00001757
	// (set) Token: 0x0600019D RID: 413 RVA: 0x0000355F File Offset: 0x0000175F
	public bool ForceShow
	{
		get
		{
			return this.forceShow;
		}
		set
		{
			if (this.forceShow == value)
			{
				return;
			}
			this.forceShow = value;
			if (GameData.g != null)
			{
				this.onAmountChanged.Invoke(GameData.g.ReadInt(this.id, 0));
			}
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x0600019E RID: 414 RVA: 0x0000359B File Offset: 0x0000179B
	// (set) Token: 0x0600019F RID: 415 RVA: 0x0001CB5C File Offset: 0x0001AD5C
	public int Amount
	{
		get
		{
			if (GameData.g != null)
			{
				return GameData.g.ReadInt(this.id, 0);
			}
			return 0;
		}
		set
		{
			if (value > 0 && this.showItemGet && !GameData.g.ReadBool(this.itemGetID, false))
			{
				Object.Instantiate<GameObject>(this.itemGetObject);
			}
			GameData.g.Write(this.itemGetID, true);
			this.lastChangeHidden = false;
			if (value < 0)
			{
				value = 0;
			}
			GameData.g.Write(this.id, value);
			this.onAmountChanged.Invoke(value);
			this.amountPreview = value;
		}
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x000035BD File Offset: 0x000017BD
	public bool HasBeenCollected()
	{
		return GameData.g.ReadBool(this.itemGetID, false) || this.Amount > 0;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x000035DD File Offset: 0x000017DD
	public void SetAmountSecret(int newAmount)
	{
		this.lastChangeHidden = true;
		GameData.g.Write(this.id, newAmount);
	}

	// Token: 0x04000287 RID: 647
	public Sprite sprite;

	// Token: 0x04000288 RID: 648
	public string id;

	// Token: 0x04000289 RID: 649
	public bool lastChangeHidden;

	// Token: 0x0400028A RID: 650
	public UnityEvent<int> onAmountChanged;

	// Token: 0x0400028B RID: 651
	private bool forceShow;

	// Token: 0x0400028C RID: 652
	[ReadOnly]
	public int amountPreview;

	// Token: 0x0400028D RID: 653
	public GameObject collectSoundEffect;

	// Token: 0x0400028E RID: 654
	public Color color;

	// Token: 0x0400028F RID: 655
	[Header("Item Get")]
	public bool showItemGet;

	// Token: 0x04000290 RID: 656
	public string itemGetID;

	// Token: 0x04000291 RID: 657
	public GameObject itemGetObject;
}
