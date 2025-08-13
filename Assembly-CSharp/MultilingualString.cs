using System;

// Token: 0x02000014 RID: 20
[Serializable]
public struct MultilingualString
{
	// Token: 0x06000034 RID: 52 RVA: 0x00002B60 File Offset: 0x00000D60
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

	// Token: 0x0400004B RID: 75
	public int id;

	// Token: 0x0400004C RID: 76
	public string idString;

	// Token: 0x0400004D RID: 77
	public string[] english;

	// Token: 0x0400004E RID: 78
	public string[] spanish;

	// Token: 0x0400004F RID: 79
	public string[] french;

	// Token: 0x04000050 RID: 80
	public string[] german;

	// Token: 0x04000051 RID: 81
	public string[] brazilianPortuguese;
}
