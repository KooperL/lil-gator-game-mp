using System;
using UnityEngine;

public class WriteBoolToSaveData : MonoBehaviour
{
	// Token: 0x0600104D RID: 4173 RVA: 0x0000DFAD File Offset: 0x0000C1AD
	public void WriteBool()
	{
		GameData.g.Write(this.key, this.boolValue);
	}

	public string key;

	public bool boolValue = true;
}
