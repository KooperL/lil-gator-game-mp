using System;
using UnityEngine;

public class OffsetAtRuntime : MonoBehaviour
{
	// Token: 0x06000A4F RID: 2639 RVA: 0x00009DD8 File Offset: 0x00007FD8
	private void Start()
	{
		base.transform.position += this.worldOffset;
	}

	public Vector3 worldOffset;
}
