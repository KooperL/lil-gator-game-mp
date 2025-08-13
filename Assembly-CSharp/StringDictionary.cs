using System;
using System.Collections.Generic;

// Token: 0x02000125 RID: 293
[Serializable]
public class StringDictionary : SerializableDictionary<string, string>
{
	// Token: 0x0600061D RID: 1565 RVA: 0x0001FE54 File Offset: 0x0001E054
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
