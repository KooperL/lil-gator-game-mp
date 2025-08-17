using System;
using UnityEngine;

[CreateAssetMenu]
public class StickerObject : ScriptableObject
{
	// (get) Token: 0x0600139A RID: 5018 RVA: 0x00010934 File Offset: 0x0000EB34
	// (set) Token: 0x0600139B RID: 5019 RVA: 0x00010947 File Offset: 0x0000EB47
	public bool IsUnlocked
	{
		get
		{
			return GameData.g.ReadBool(this.id, false);
		}
		set
		{
			GameData.g.Write(this.id, value);
		}
	}

	// (get) Token: 0x0600139C RID: 5020 RVA: 0x0001095A File Offset: 0x0000EB5A
	// (set) Token: 0x0600139D RID: 5021 RVA: 0x000601E4 File Offset: 0x0005E3E4
	public Vector2Int SavedPosition
	{
		get
		{
			return new Vector2Int(GameData.g.ReadInt(this.id + "x", 0), GameData.g.ReadInt(this.id + "y", 0));
		}
		set
		{
			GameData.g.Write(this.id + "x", value.x);
			GameData.g.Write(this.id + "y", value.y);
		}
	}

	public string id;

	public Sprite sprite;
}
