using System;

// Token: 0x02000012 RID: 18
[Serializable]
public struct DialogueLine
{
	// Token: 0x0600002E RID: 46 RVA: 0x000022A4 File Offset: 0x000004A4
	public bool HasText()
	{
		return !string.IsNullOrEmpty(this.english);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00017724 File Offset: 0x00015924
	public string GetText(Language language = Language.English)
	{
		string text;
		if (language == Language.English)
		{
			text = this.english;
		}
		else
		{
			text = "Language unsupported";
		}
		if (text.Contains("["))
		{
			text = text.Replace("[0]", MultilingualTextDocument.placeholders[0]);
			text = text.Replace("[playername]", GameData.PlayerName);
		}
		return text;
	}

	// Token: 0x04000030 RID: 48
	public string descriptor;

	// Token: 0x04000031 RID: 49
	public string comments;

	// Token: 0x04000032 RID: 50
	public string english;

	// Token: 0x04000033 RID: 51
	public int actorIndex;

	// Token: 0x04000034 RID: 52
	public int emote;

	// Token: 0x04000035 RID: 53
	public bool cue;

	// Token: 0x04000036 RID: 54
	public int lookTarget;

	// Token: 0x04000037 RID: 55
	public int[] look;

	// Token: 0x04000038 RID: 56
	public bool holdEmote;

	// Token: 0x04000039 RID: 57
	public float waitTime;

	// Token: 0x0400003A RID: 58
	public bool noInput;

	// Token: 0x0400003B RID: 59
	public int state;

	// Token: 0x0400003C RID: 60
	public int position;
}
