using System;
using System.Collections.Generic;

[Serializable]
public class StringDictionary : SerializableDictionary<string, string>
{
	// Token: 0x06000781 RID: 1921 RVA: 0x000352E4 File Offset: 0x000334E4
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
