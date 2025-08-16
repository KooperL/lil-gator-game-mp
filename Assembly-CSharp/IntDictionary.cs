using System;
using System.Collections.Generic;

[Serializable]
public class IntDictionary : SerializableDictionary<string, int>
{
	// Token: 0x0600077F RID: 1919 RVA: 0x000350C4 File Offset: 0x000332C4
	public IntDictionary Clone()
	{
		IntDictionary intDictionary = new IntDictionary();
		foreach (KeyValuePair<string, int> keyValuePair in this)
		{
			intDictionary.Add(keyValuePair.Key, keyValuePair.Value);
		}
		return intDictionary;
	}
}
