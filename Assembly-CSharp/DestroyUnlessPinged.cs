using System;
using UnityEngine;

public class DestroyUnlessPinged : MonoBehaviour
{
	// Token: 0x06000361 RID: 865 RVA: 0x00004A0B File Offset: 0x00002C0B
	public void Ping()
	{
		this.lastPing = Time.time;
	}

	// Token: 0x06000362 RID: 866 RVA: 0x00004A18 File Offset: 0x00002C18
	private void Update()
	{
		if (Time.time - this.lastPing > 0.5f)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private const float pingTimeout = 0.5f;

	private float lastPing = -1f;
}
