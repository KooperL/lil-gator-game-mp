using System;
using System.Collections.Generic;

// Token: 0x02000124 RID: 292
[Serializable]
public class IntDictionary : SerializableDictionary<string, int>
{
	// Token: 0x0600061B RID: 1563 RVA: 0x0001FDE8 File Offset: 0x0001DFE8
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
