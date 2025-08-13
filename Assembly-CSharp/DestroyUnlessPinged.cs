using System;
using UnityEngine;

// Token: 0x0200009F RID: 159
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

	// Token: 0x04000413 RID: 1043
	private const float pingTimeout = 0.5f;

	// Token: 0x04000414 RID: 1044
	private float lastPing = -1f;
}
