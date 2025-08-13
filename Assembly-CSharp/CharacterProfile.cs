using System;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x020000D6 RID: 214
[CreateAssetMenu]
public class CharacterProfile : ScriptableObject
{
	// Token: 0x1700003A RID: 58
	// (get) Token: 0x0600038B RID: 907 RVA: 0x00004B1B File Offset: 0x00002D1B
	// (set) Token: 0x0600038C RID: 908 RVA: 0x00004B38 File Offset: 0x00002D38
	public bool IsUnlocked
	{
		get
		{
			return this.startsUnlocked || GameData.g.ReadBool(this.id, false);
		}
		set
		{
			if (this.startsUnlocked)
			{
				return;
			}
			GameData.g.Write(this.id, value);
			this.OnChange(this, value);
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x0600038D RID: 909 RVA: 0x00004B61 File Offset: 0x00002D61
	public string Name
	{
		get
		{
			if (this.isPlayer)
			{
				return GameData.PlayerName;
			}
			return this.name;
		}
	}

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x0600038E RID: 910 RVA: 0x00026394 File Offset: 0x00024594
	// (remove) Token: 0x0600038F RID: 911 RVA: 0x000263CC File Offset: 0x000245CC
	public event EventHandler<bool> OnChange = delegate
	{
	};

	// Token: 0x06000390 RID: 912 RVA: 0x00004B77 File Offset: 0x00002D77
	public Color GetColor(CharacterProfile.CharacterColor colorType)
	{
		switch (colorType)
		{
		case CharacterProfile.CharacterColor.Bright:
			return this.brightColor;
		case CharacterProfile.CharacterColor.Mid:
			return this.midColor;
		case CharacterProfile.CharacterColor.Dark:
			return this.darkColor;
		default:
			return Color.white;
		}
	}

	// Token: 0x06000391 RID: 913 RVA: 0x00004BA7 File Offset: 0x00002DA7
	private void OnValidate()
	{
		if (this.midColor == Color.white)
		{
			this.midColor = this.darkColor;
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00004BC7 File Offset: 0x00002DC7
	public void SetName(string nameID, MultilingualTextDocument document)
	{
		this.name = document.FetchString(nameID, Language.English);
		UINameplate.UpdateNameplates(this);
	}

	// Token: 0x0400050C RID: 1292
	public string id;

	// Token: 0x0400050D RID: 1293
	public bool startsUnlocked;

	// Token: 0x0400050E RID: 1294
	public string name;

	// Token: 0x0400050F RID: 1295
	[HideInInspector]
	public Sprite nameplate;

	// Token: 0x04000510 RID: 1296
	[HideInInspector]
	public Sprite picture;

	// Token: 0x04000511 RID: 1297
	[FormerlySerializedAs("color")]
	public Color brightColor = Color.white;

	// Token: 0x04000512 RID: 1298
	public Color midColor = Color.grey;

	// Token: 0x04000513 RID: 1299
	[FormerlySerializedAs("backgroundColor")]
	public Color darkColor = Color.black;

	// Token: 0x04000514 RID: 1300
	[HideInInspector]
	public Sprite dialogueDecoration;

	// Token: 0x04000515 RID: 1301
	public Sprite pattern;

	// Token: 0x04000516 RID: 1302
	public bool isPlayer;

	// Token: 0x020000D7 RID: 215
	public enum CharacterColor
	{
		// Token: 0x04000519 RID: 1305
		Bright,
		// Token: 0x0400051A RID: 1306
		Mid,
		// Token: 0x0400051B RID: 1307
		Dark
	}
}
