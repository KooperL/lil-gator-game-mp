using System;
using UnityEngine;

// Token: 0x02000246 RID: 582
public class StaticBatchObject : MonoBehaviour
{
	// Token: 0x06000CAC RID: 3244 RVA: 0x0003D6BA File Offset: 0x0003B8BA
	private void OnEnable()
	{
		StaticBatchingUtility.Combine(base.gameObject);
	}
}
