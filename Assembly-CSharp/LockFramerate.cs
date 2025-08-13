using System;
using UnityEngine;

// Token: 0x020001C3 RID: 451
public class LockFramerate : MonoBehaviour
{
	// Token: 0x06000889 RID: 2185 RVA: 0x00008692 File Offset: 0x00006892
	private void OnEnable()
	{
		Application.targetFrameRate = this.targetFrameRate;
	}

	// Token: 0x04000B1D RID: 2845
	public int targetFrameRate = 60;
}
