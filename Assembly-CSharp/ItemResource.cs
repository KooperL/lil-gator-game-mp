using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class ItemResource : ScriptableObject
{
	// (get) Token: 0x060001A9 RID: 425 RVA: 0x00003641 File Offset: 0x00001841
	// (set) Token: 0x060001AA RID: 426 RVA: 0x00003649 File Offset: 0x00001849
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

	// (get) Token: 0x060001AB RID: 427 RVA: 0x00003685 File Offset: 0x00001885
	// (set) Token: 0x060001AC RID: 428 RVA: 0x0001D578 File Offset: 0x0001B778
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
				global::UnityEngine.Object.Instantiate<GameObject>(this.itemGetObject);
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

	// Token: 0x060001AD RID: 429 RVA: 0x000036A7 File Offset: 0x000018A7
	public bool HasBeenCollected()
	{
		return GameData.g.ReadBool(this.itemGetID, false) || this.Amount > 0;
	}

	// Token: 0x060001AE RID: 430 RVA: 0x000036C7 File Offset: 0x000018C7
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
