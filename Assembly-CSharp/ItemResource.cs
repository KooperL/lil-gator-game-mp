using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000063 RID: 99
[CreateAssetMenu]
public class ItemResource : ScriptableObject
{
	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000170 RID: 368 RVA: 0x00008875 File Offset: 0x00006A75
	// (set) Token: 0x06000171 RID: 369 RVA: 0x0000887D File Offset: 0x00006A7D
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

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000172 RID: 370 RVA: 0x000088B9 File Offset: 0x00006AB9
	// (set) Token: 0x06000173 RID: 371 RVA: 0x000088DC File Offset: 0x00006ADC
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

	// Token: 0x06000174 RID: 372 RVA: 0x00008957 File Offset: 0x00006B57
	public bool HasBeenCollected()
	{
		return GameData.g.ReadBool(this.itemGetID, false) || this.Amount > 0;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00008977 File Offset: 0x00006B77
	public void SetAmountSecret(int newAmount)
	{
		this.lastChangeHidden = true;
		GameData.g.Write(this.id, newAmount);
	}

	// Token: 0x04000215 RID: 533
	public Sprite sprite;

	// Token: 0x04000216 RID: 534
	public string id;

	// Token: 0x04000217 RID: 535
	public bool lastChangeHidden;

	// Token: 0x04000218 RID: 536
	public UnityEvent<int> onAmountChanged;

	// Token: 0x04000219 RID: 537
	private bool forceShow;

	// Token: 0x0400021A RID: 538
	[ReadOnly]
	public int amountPreview;

	// Token: 0x0400021B RID: 539
	public GameObject collectSoundEffect;

	// Token: 0x0400021C RID: 540
	public Color color;

	// Token: 0x0400021D RID: 541
	[Header("Item Get")]
	public bool showItemGet;

	// Token: 0x0400021E RID: 542
	public string itemGetID;

	// Token: 0x0400021F RID: 543
	public GameObject itemGetObject;
}
