using System;
using UnityEngine;

[CreateAssetMenu]
public class StickerObject : ScriptableObject
{
	// (get) Token: 0x0600139B RID: 5019 RVA: 0x0001093E File Offset: 0x0000EB3E
	// (set) Token: 0x0600139C RID: 5020 RVA: 0x00010951 File Offset: 0x0000EB51
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

	// (get) Token: 0x0600139D RID: 5021 RVA: 0x00010964 File Offset: 0x0000EB64
	// (set) Token: 0x0600139E RID: 5022 RVA: 0x000604AC File Offset: 0x0005E6AC
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
