using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class CharacterProfile : ScriptableObject
{
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

	public string id;

	public bool startsUnlocked;

	public MultilingualTextDocument document;

	[TextLookup("document")]
	public new string name;

	[HideInInspector]
	public Sprite nameplate;

	[HideInInspector]
	public Sprite picture;

	[FormerlySerializedAs("color")]
	public Color brightColor = Color.white;

	public Color midColor = Color.grey;

	[FormerlySerializedAs("backgroundColor")]
	public Color darkColor = Color.black;

	[HideInInspector]
	public Sprite dialogueDecoration;

	public Sprite pattern;

	public bool isPlayer;

	public enum CharacterColor
	{
		Bright,
		Mid,
		Dark
	}
}
