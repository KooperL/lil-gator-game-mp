using System;
using UnityEngine;

public class DestroyUnlessPinged : MonoBehaviour
{
	// Token: 0x060002F8 RID: 760 RVA: 0x0001168B File Offset: 0x0000F88B
	public void Ping()
	{
		this.lastPing = Time.time;
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00011698 File Offset: 0x0000F898
	private void Update()
	{
		if (Time.time - this.lastPing > 0.5f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private const float pingTimeout = 0.5f;

	private float lastPing = -1f;
}
