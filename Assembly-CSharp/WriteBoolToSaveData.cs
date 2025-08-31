using System;
using UnityEngine;

public class WriteBoolToSaveData : MonoBehaviour
{
	// Token: 0x06000D43 RID: 3395 RVA: 0x000405B9 File Offset: 0x0003E7B9
	public void WriteBool()
	{
		GameData.g.Write(this.key, this.boolValue);
	}

	public string key;

	public bool boolValue = true;
}
