using System;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class WriteBoolToSaveData : MonoBehaviour
{
	// Token: 0x06000D43 RID: 3395 RVA: 0x000405B9 File Offset: 0x0003E7B9
	public void WriteBool()
	{
		GameData.g.Write(this.key, this.boolValue);
	}

	// Token: 0x04001189 RID: 4489
	public string key;

	// Token: 0x0400118A RID: 4490
	public bool boolValue = true;
}
