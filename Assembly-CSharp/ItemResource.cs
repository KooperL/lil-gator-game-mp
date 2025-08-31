using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class ItemResource : ScriptableObject
{
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

	public Sprite sprite;

	public string id;

	public bool lastChangeHidden;

	public UnityEvent<int> onAmountChanged;

	private bool forceShow;

	[ReadOnly]
	public int amountPreview;

	public GameObject collectSoundEffect;

	public Color color;

	[Header("Item Get")]
	public bool showItemGet;

	public string itemGetID;

	public GameObject itemGetObject;
}
