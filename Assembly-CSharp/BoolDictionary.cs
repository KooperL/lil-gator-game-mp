using System;
using System.Collections.Generic;

[Serializable]
public class BoolDictionary : SerializableDictionary<string, bool>
{
	// Token: 0x0600077D RID: 1917 RVA: 0x00035240 File Offset: 0x00033440
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
