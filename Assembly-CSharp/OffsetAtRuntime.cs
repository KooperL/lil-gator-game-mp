using System;
using UnityEngine;

// Token: 0x0200019B RID: 411
public class OffsetAtRuntime : MonoBehaviour
{
	// Token: 0x06000883 RID: 2179 RVA: 0x000283F9 File Offset: 0x000265F9
	private void Start()
	{
		base.transform.position += this.worldOffset;
	}

	// Token: 0x04000A7C RID: 2684
	public Vector3 worldOffset;
}
