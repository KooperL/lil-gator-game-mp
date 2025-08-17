using System;
using UnityEngine;

public class LockFramerate : MonoBehaviour
{
	// Token: 0x060008C9 RID: 2249 RVA: 0x000089AF File Offset: 0x00006BAF
	private void OnEnable()
	{
		Application.targetFrameRate = this.targetFrameRate;
	}

	public int targetFrameRate = 60;
}
