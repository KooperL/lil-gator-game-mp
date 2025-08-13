using System;
using UnityEngine;

// Token: 0x0200032D RID: 813
public class WriteBoolToSaveData : MonoBehaviour
{
	// Token: 0x06000FF1 RID: 4081 RVA: 0x0000DC3A File Offset: 0x0000BE3A
	public void WriteBool()
	{
		GameData.g.Write(this.key, this.boolValue);
	}

	// Token: 0x040014AF RID: 5295
	public string key;

	// Token: 0x040014B0 RID: 5296
	public bool boolValue = true;
}
