using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class CharacterProfile : ScriptableObject
{
	// (get) Token: 0x060003B1 RID: 945 RVA: 0x00004CFF File Offset: 0x00002EFF
	// (set) Token: 0x060003B2 RID: 946 RVA: 0x00004D1C File Offset: 0x00002F1C
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

	// (get) Token: 0x060003B3 RID: 947 RVA: 0x00004D45 File Offset: 0x00002F45
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

	// (add) Token: 0x060003B4 RID: 948 RVA: 0x0002731C File Offset: 0x0002551C
	// (remove) Token: 0x060003B5 RID: 949 RVA: 0x00027354 File Offset: 0x00025554
	public event EventHandler<bool> OnChange = delegate
	{
	};

	// Token: 0x060003B6 RID: 950 RVA: 0x00004D7D File Offset: 0x00002F7D
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

	// Token: 0x060003B7 RID: 951 RVA: 0x00004DAD File Offset: 0x00002FAD
	private void OnValidate()
	{
		if (this.midColor == Color.white)
		{
			this.midColor = this.darkColor;
		}
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x00004DCD File Offset: 0x00002FCD
	public void SetName(string nameID, MultilingualTextDocument document)
	{
		this.name = document.FetchString(nameID, Language.Auto);
		UINameplate.UpdateNameplates(this);
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00004DE4 File Offset: 0x00002FE4
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
