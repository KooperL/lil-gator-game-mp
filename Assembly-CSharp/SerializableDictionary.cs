using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000122 RID: 290
[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	// Token: 0x06000616 RID: 1558 RVA: 0x0001FC48 File Offset: 0x0001DE48
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

	// Token: 0x06000617 RID: 1559 RVA: 0x0001FCCC File Offset: 0x0001DECC
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

	// Token: 0x04000856 RID: 2134
	[SerializeField]
	private List<TKey> keys = new List<TKey>();

	// Token: 0x04000857 RID: 2135
	[SerializeField]
	private List<TValue> values = new List<TValue>();
}
