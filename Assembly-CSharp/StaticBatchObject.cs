using System;
using UnityEngine;

public class StaticBatchObject : MonoBehaviour
{
	// Token: 0x06000FB4 RID: 4020 RVA: 0x0000D95F File Offset: 0x0000BB5F
	private void OnEnable()
	{
		StaticBatchingUtility.Combine(base.gameObject);
	}
}
