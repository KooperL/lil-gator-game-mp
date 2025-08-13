using System;
using UnityEngine;

// Token: 0x02000172 RID: 370
public class LowFramerateCamera : MonoBehaviour
{
	// Token: 0x0600079B RID: 1947 RVA: 0x000255DA File Offset: 0x000237DA
	public void OnValidate()
	{
		if (this.camera == null)
		{
			this.camera = base.GetComponent<Camera>();
		}
		this.frameTime = 1f / this.framerate;
	}

	// Token: 0x0600079C RID: 1948 RVA: 0x00025608 File Offset: 0x00023808
	private void OnEnable()
	{
		this.nextFrameTime = Time.time;
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x00025615 File Offset: 0x00023815
	private void Update()
	{
		if (Time.time > this.nextFrameTime)
		{
			this.nextFrameTime += this.frameTime;
			this.camera.enabled = true;
			return;
		}
		this.camera.enabled = false;
	}

	// Token: 0x040009C5 RID: 2501
	public float framerate = 15f;

	// Token: 0x040009C6 RID: 2502
	[ReadOnly]
	public float frameTime;

	// Token: 0x040009C7 RID: 2503
	private float nextFrameTime = -1f;

	// Token: 0x040009C8 RID: 2504
	public Camera camera;
}
