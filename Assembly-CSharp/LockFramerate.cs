using System;
using UnityEngine;

public class LockFramerate : MonoBehaviour
{
	// Token: 0x0600073F RID: 1855 RVA: 0x00024381 File Offset: 0x00022581
	private void OnEnable()
	{
		Application.targetFrameRate = this.targetFrameRate;
	}

	public int targetFrameRate = 60;
}
