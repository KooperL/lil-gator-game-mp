using System;
using UnityEngine;

// Token: 0x0200015A RID: 346
public class LockFramerate : MonoBehaviour
{
	// Token: 0x0600073F RID: 1855 RVA: 0x00024381 File Offset: 0x00022581
	private void OnEnable()
	{
		Application.targetFrameRate = this.targetFrameRate;
	}

	// Token: 0x0400097F RID: 2431
	public int targetFrameRate = 60;
}
