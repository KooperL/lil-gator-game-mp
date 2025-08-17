using System;
using UnityEngine;

public class StaticBatchObject : MonoBehaviour
{
	// Token: 0x06000FB4 RID: 4020 RVA: 0x0000D974 File Offset: 0x0000BB74
	private void OnEnable()
	{
		StaticBatchingUtility.Combine(base.gameObject);
	}
}
