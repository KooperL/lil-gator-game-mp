using System;
using System.Collections.Generic;

[Serializable]
public class StringDictionary : SerializableDictionary<string, string>
{
	// Token: 0x06000782 RID: 1922 RVA: 0x000355D0 File Offset: 0x000337D0
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
