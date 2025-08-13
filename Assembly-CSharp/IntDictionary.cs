using System;
using System.Collections.Generic;

// Token: 0x0200017D RID: 381
[Serializable]
public class IntDictionary : SerializableDictionary<string, int>
{
	// Token: 0x0600073F RID: 1855 RVA: 0x0003393C File Offset: 0x00031B3C
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
