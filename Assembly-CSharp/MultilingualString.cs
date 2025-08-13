using System;

// Token: 0x02000013 RID: 19
[Serializable]
public struct MultilingualString
{
	// Token: 0x06000030 RID: 48 RVA: 0x000022B4 File Offset: 0x000004B4
	public string GetText(Language language = Language.English)
	{
		if (language == Language.English)
		{
			return this.english;
		}
		return "Language unsupported";
	}

	// Token: 0x0400003D RID: 61
	public int id;

	// Token: 0x0400003E RID: 62
	public string idString;

	// Token: 0x0400003F RID: 63
	public string english;

	// Token: 0x04000040 RID: 64
	public string french;
}
