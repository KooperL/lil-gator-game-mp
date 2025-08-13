using System;

// Token: 0x02000013 RID: 19
[Serializable]
public struct DialogueLine
{
	// Token: 0x06000032 RID: 50 RVA: 0x00002A66 File Offset: 0x00000C66
	public bool HasText()
	{
		return this.english != null && this.english.Length != 0 && !string.IsNullOrEmpty(this.english[0]);
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002A8C File Offset: 0x00000C8C
	public string GetText(Language language = Language.Auto)
	{
		if (language == Language.Auto)
		{
			language = Settings.language;
		}
		string[] array = this.english;
		switch (language)
		{
		case Language.French:
			array = this.french;
			break;
		case Language.Spanish:
			array = this.spanish;
			break;
		case Language.German:
			array = this.german;
			break;
		case Language.BrazilianPortuguese:
			array = this.brazilianPortuguese;
			break;
		}
		string text = "";
		if (array != null && array.Length != 0)
		{
			text = array[0];
		}
		if (string.IsNullOrEmpty(text) && this.english != null && this.english.Length != 0)
		{
			text = this.english[0];
		}
		if (text.Contains("["))
		{
			text = text.Replace("[0]", MultilingualTextDocument.placeholders[0]);
			text = text.Replace("[playername]", GameData.PlayerName);
			text = text.Replace("[Playername]", GameData.PlayerName);
		}
		return text;
	}

	// Token: 0x0400003A RID: 58
	public string descriptor;

	// Token: 0x0400003B RID: 59
	public string comments;

	// Token: 0x0400003C RID: 60
	public string[] english;

	// Token: 0x0400003D RID: 61
	public string[] spanish;

	// Token: 0x0400003E RID: 62
	public string[] french;

	// Token: 0x0400003F RID: 63
	public string[] german;

	// Token: 0x04000040 RID: 64
	public string[] brazilianPortuguese;

	// Token: 0x04000041 RID: 65
	public int actorIndex;

	// Token: 0x04000042 RID: 66
	public int emote;

	// Token: 0x04000043 RID: 67
	public bool cue;

	// Token: 0x04000044 RID: 68
	public int lookTarget;

	// Token: 0x04000045 RID: 69
	public int[] look;

	// Token: 0x04000046 RID: 70
	public bool holdEmote;

	// Token: 0x04000047 RID: 71
	public float waitTime;

	// Token: 0x04000048 RID: 72
	public bool noInput;

	// Token: 0x04000049 RID: 73
	public int state;

	// Token: 0x0400004A RID: 74
	public int position;
}
