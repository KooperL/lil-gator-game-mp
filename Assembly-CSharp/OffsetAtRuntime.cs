using System;
using UnityEngine;

public class OffsetAtRuntime : MonoBehaviour
{
	// Token: 0x06000883 RID: 2179 RVA: 0x000283F9 File Offset: 0x000265F9
	private void Start()
	{
		base.transform.position += this.worldOffset;
	}

	public Vector3 worldOffset;
}
