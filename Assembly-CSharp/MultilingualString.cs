using System;

[Serializable]
public struct MultilingualString
{
	// Token: 0x06000034 RID: 52 RVA: 0x00017EC8 File Offset: 0x000160C8
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
		if (array != null && array.Length != 0 && !string.IsNullOrEmpty(array[0]))
		{
			return array[0];
		}
		if (this.english != null && this.english.Length != 0 && !string.IsNullOrEmpty(this.english[0]))
		{
			return this.english[0];
		}
		return "Language unsupported";
	}

	public int id;

	public string idString;

	public string[] english;

	public string[] spanish;

	public string[] french;

	public string[] german;

	public string[] brazilianPortuguese;
}
