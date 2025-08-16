using System;
using UnityEngine;

public class LockFramerate : MonoBehaviour
{
	// Token: 0x060008C9 RID: 2249 RVA: 0x0000899A File Offset: 0x00006B9A
	private void OnEnable()
	{
		Application.targetFrameRate = this.targetFrameRate;
	}

	public int targetFrameRate = 60;
}
