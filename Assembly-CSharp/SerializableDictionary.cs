using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200017B RID: 379
[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	// Token: 0x0600073A RID: 1850 RVA: 0x000337C4 File Offset: 0x000319C4
	public void OnBeforeSerialize()
	{
		this.keys.Clear();
		this.values.Clear();
		foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
		{
			this.keys.Add(keyValuePair.Key);
			this.values.Add(keyValuePair.Value);
		}
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00033848 File Offset: 0x00031A48
	public void OnAfterDeserialize()
	{
		base.Clear();
		if (this.keys.Count != this.values.Count)
		{
			throw new Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable.", this.keys.Count, this.values.Count));
		}
		for (int i = 0; i < this.keys.Count; i++)
		{
			base.Add(this.keys[i], this.values[i]);
		}
	}

	// Token: 0x040009B5 RID: 2485
	[SerializeField]
	private List<TKey> keys = new List<TKey>();

	// Token: 0x040009B6 RID: 2486
	[SerializeField]
	private List<TValue> values = new List<TValue>();
}
