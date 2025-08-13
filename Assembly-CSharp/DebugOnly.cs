using System;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class DebugOnly : MonoBehaviour
{
	// Token: 0x06000332 RID: 818 RVA: 0x000047FB File Offset: 0x000029FB
	private void Update()
	{
		Object.Destroy(base.gameObject);
	}
}
