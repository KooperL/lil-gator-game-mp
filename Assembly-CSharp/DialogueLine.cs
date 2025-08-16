using System;

[Serializable]
public struct DialogueLine
{
	// Token: 0x06000032 RID: 50 RVA: 0x000022D2 File Offset: 0x000004D2
	public bool HasText()
	{
		return this.english != null && this.english.Length != 0 && !string.IsNullOrEmpty(this.english[0]);
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00017C9C File Offset: 0x00015E9C
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

	public string descriptor;

	public string comments;

	public string[] english;

	public string[] spanish;

	public string[] french;

	public string[] german;

	public string[] brazilianPortuguese;

	public int actorIndex;

	public int emote;

	public bool cue;

	public int lookTarget;

	public int[] look;

	public bool holdEmote;

	public float waitTime;

	public bool noInput;

	public int state;

	public int position;
}
