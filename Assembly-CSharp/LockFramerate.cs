using System;
using UnityEngine;

public class LockFramerate : MonoBehaviour
{
	// Token: 0x060008C9 RID: 2249 RVA: 0x000089B9 File Offset: 0x00006BB9
	private void OnEnable()
	{
		Application.targetFrameRate = this.targetFrameRate;
	}

	public int targetFrameRate = 60;
}
