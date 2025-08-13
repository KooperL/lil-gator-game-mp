using System;
using UnityEngine;

// Token: 0x02000217 RID: 535
public class OffsetAtRuntime : MonoBehaviour
{
	// Token: 0x06000A06 RID: 2566 RVA: 0x00009A9A File Offset: 0x00007C9A
	private void Start()
	{
		base.transform.position += this.worldOffset;
	}

	// Token: 0x04000C81 RID: 3201
	public Vector3 worldOffset;
}
