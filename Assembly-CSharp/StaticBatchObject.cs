using System;
using UnityEngine;

// Token: 0x0200030A RID: 778
public class StaticBatchObject : MonoBehaviour
{
	// Token: 0x06000F58 RID: 3928 RVA: 0x0000D5CC File Offset: 0x0000B7CC
	private void OnEnable()
	{
		StaticBatchingUtility.Combine(base.gameObject);
	}
}
