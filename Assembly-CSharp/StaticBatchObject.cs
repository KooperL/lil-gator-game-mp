using System;
using UnityEngine;

public class StaticBatchObject : MonoBehaviour
{
	// Token: 0x06000FB4 RID: 4020 RVA: 0x0000D97E File Offset: 0x0000BB7E
	private void OnEnable()
	{
		StaticBatchingUtility.Combine(base.gameObject);
	}
}
