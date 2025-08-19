using System;
using System.Collections.Generic;

[Serializable]
public class BoolDictionary : SerializableDictionary<string, bool>
{
	// Token: 0x0600077D RID: 1917 RVA: 0x0003521C File Offset: 0x0003341C
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
