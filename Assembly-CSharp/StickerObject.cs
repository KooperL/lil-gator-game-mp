using System;
using UnityEngine;

// Token: 0x020002F0 RID: 752
[CreateAssetMenu]
public class StickerObject : ScriptableObject
{
	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x0600100D RID: 4109 RVA: 0x0004CFB3 File Offset: 0x0004B1B3
	// (set) Token: 0x0600100E RID: 4110 RVA: 0x0004CFC6 File Offset: 0x0004B1C6
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

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x0600100F RID: 4111 RVA: 0x0004CFD9 File Offset: 0x0004B1D9
	// (set) Token: 0x06001010 RID: 4112 RVA: 0x0004D018 File Offset: 0x0004B218
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

	// Token: 0x04001515 RID: 5397
	public string id;

	// Token: 0x04001516 RID: 5398
	public Sprite sprite;
}
