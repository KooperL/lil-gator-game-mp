using System;
using UnityEngine;

// Token: 0x020003E3 RID: 995
[CreateAssetMenu]
public class StickerObject : ScriptableObject
{
	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x0600133A RID: 4922 RVA: 0x00010537 File Offset: 0x0000E737
	// (set) Token: 0x0600133B RID: 4923 RVA: 0x0001054A File Offset: 0x0000E74A
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

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x0600133C RID: 4924 RVA: 0x0001055D File Offset: 0x0000E75D
	// (set) Token: 0x0600133D RID: 4925 RVA: 0x0005E1BC File Offset: 0x0005C3BC
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

	// Token: 0x040018D8 RID: 6360
	public string id;

	// Token: 0x040018D9 RID: 6361
	public Sprite sprite;
}
