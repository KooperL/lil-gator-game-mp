using System;
using System.Collections.Generic;

[Serializable]
public class IntDictionary : SerializableDictionary<string, int>
{
	// Token: 0x06000780 RID: 1920 RVA: 0x0003556C File Offset: 0x0003376C
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
