using System;
using UnityEngine;

public class DebugOnly : MonoBehaviour
{
	// Token: 0x060002EF RID: 751 RVA: 0x0001157F File Offset: 0x0000F77F
	private void Update()
	{
		Object.Destroy(base.gameObject);
	}
}
