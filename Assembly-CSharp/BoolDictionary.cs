using System;
using System.Collections.Generic;

// Token: 0x0200017C RID: 380
[Serializable]
public class BoolDictionary : SerializableDictionary<string, bool>
{
	// Token: 0x0600073D RID: 1853 RVA: 0x000338D8 File Offset: 0x00031AD8
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
