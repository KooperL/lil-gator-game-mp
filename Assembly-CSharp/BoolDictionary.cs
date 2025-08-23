using System;
using System.Collections.Generic;

[Serializable]
public class BoolDictionary : SerializableDictionary<string, bool>
{
	// Token: 0x0600077E RID: 1918 RVA: 0x00035508 File Offset: 0x00033708
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
