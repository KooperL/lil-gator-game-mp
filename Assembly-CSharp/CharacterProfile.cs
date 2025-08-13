using System;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x020000AB RID: 171
[CreateAssetMenu]
public class CharacterProfile : ScriptableObject
{
	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000340 RID: 832 RVA: 0x00013087 File Offset: 0x00011287
	// (set) Token: 0x06000341 RID: 833 RVA: 0x000130A4 File Offset: 0x000112A4
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

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000342 RID: 834 RVA: 0x000130CD File Offset: 0x000112CD
	public string Name
	{
		get
		{
			if (this.isPlayer)
			{
				return GameData.PlayerName;
			}
			if (this.document != null)
			{
				return this.document.FetchString(this.name, Language.Auto);
			}
			return this.name;
		}
	}

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000343 RID: 835 RVA: 0x00013108 File Offset: 0x00011308
	// (remove) Token: 0x06000344 RID: 836 RVA: 0x00013140 File Offset: 0x00011340
	public event EventHandler<bool> OnChange = delegate
	{
	};

	// Token: 0x06000345 RID: 837 RVA: 0x00013175 File Offset: 0x00011375
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

	// Token: 0x06000346 RID: 838 RVA: 0x000131A5 File Offset: 0x000113A5
	private void OnValidate()
	{
		if (this.midColor == Color.white)
		{
			this.midColor = this.darkColor;
		}
	}

	// Token: 0x06000347 RID: 839 RVA: 0x000131C5 File Offset: 0x000113C5
	public void SetName(string nameID, MultilingualTextDocument document)
	{
		this.name = document.FetchString(nameID, Language.Auto);
		UINameplate.UpdateNameplates(this);
	}

	// Token: 0x06000348 RID: 840 RVA: 0x000131DC File Offset: 0x000113DC
	[ContextMenu("Add Name To Document")]
	public void AddNameToDocument()
	{
		if (!this.document.HasString(this.name))
		{
			this.document.AddStringEntry(this.name, this.name);
		}
	}

	// Token: 0x0400046C RID: 1132
	public string id;

	// Token: 0x0400046D RID: 1133
	public bool startsUnlocked;

	// Token: 0x0400046E RID: 1134
	public MultilingualTextDocument document;

	// Token: 0x0400046F RID: 1135
	[TextLookup("document")]
	public new string name;

	// Token: 0x04000470 RID: 1136
	[HideInInspector]
	public Sprite nameplate;

	// Token: 0x04000471 RID: 1137
	[HideInInspector]
	public Sprite picture;

	// Token: 0x04000472 RID: 1138
	[FormerlySerializedAs("color")]
	public Color brightColor = Color.white;

	// Token: 0x04000473 RID: 1139
	public Color midColor = Color.grey;

	// Token: 0x04000474 RID: 1140
	[FormerlySerializedAs("backgroundColor")]
	public Color darkColor = Color.black;

	// Token: 0x04000475 RID: 1141
	[HideInInspector]
	public Sprite dialogueDecoration;

	// Token: 0x04000476 RID: 1142
	public Sprite pattern;

	// Token: 0x04000477 RID: 1143
	public bool isPlayer;

	// Token: 0x02000381 RID: 897
	public enum CharacterColor
	{
		// Token: 0x04001AA0 RID: 6816
		Bright,
		// Token: 0x04001AA1 RID: 6817
		Mid,
		// Token: 0x04001AA2 RID: 6818
		Dark
	}
}
