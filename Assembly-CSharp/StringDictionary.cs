using System;
using System.Collections.Generic;

// Token: 0x0200017E RID: 382
[Serializable]
public class StringDictionary : SerializableDictionary<string, string>
{
	// Token: 0x06000741 RID: 1857 RVA: 0x000339A0 File Offset: 0x00031BA0
	public StringDictionary Clone()
	{
		StringDictionary stringDictionary = new StringDictionary();
		foreach (KeyValuePair<string, string> keyValuePair in this)
		{
			stringDictionary.Add(keyValuePair.Key, keyValuePair.Value);
		}
		return stringDictionary;
	}
}
