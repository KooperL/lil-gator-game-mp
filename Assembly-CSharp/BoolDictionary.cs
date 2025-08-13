using System;
using System.Collections.Generic;

// Token: 0x02000123 RID: 291
[Serializable]
public class BoolDictionary : SerializableDictionary<string, bool>
{
	// Token: 0x06000619 RID: 1561 RVA: 0x0001FD7C File Offset: 0x0001DF7C
	public BoolDictionary Clone()
	{
		BoolDictionary boolDictionary = new BoolDictionary();
		foreach (KeyValuePair<string, bool> keyValuePair in this)
		{
			boolDictionary.Add(keyValuePair.Key, keyValuePair.Value);
		}
		return boolDictionary;
	}
}
