using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class DestroyUnlessPinged : MonoBehaviour
{
	// Token: 0x0600033B RID: 827 RVA: 0x00004827 File Offset: 0x00002A27
	public void Ping()
	{
		this.lastPing = Time.time;
	}

	// Token: 0x0600033C RID: 828 RVA: 0x00004834 File Offset: 0x00002A34
	private void Update()
	{
		if (Time.time - this.lastPing > 0.5f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040004A9 RID: 1193
	private const float pingTimeout = 0.5f;

	// Token: 0x040004AA RID: 1194
	private float lastPing = -1f;
}
