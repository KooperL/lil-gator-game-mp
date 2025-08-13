using System;
using UnityEngine;

// Token: 0x020001E6 RID: 486
public class LowFramerateCamera : MonoBehaviour
{
	// Token: 0x060008F9 RID: 2297 RVA: 0x00008C12 File Offset: 0x00006E12
	public void OnValidate()
	{
		if (this.camera == null)
		{
			this.camera = base.GetComponent<Camera>();
		}
		this.frameTime = 1f / this.framerate;
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00008C40 File Offset: 0x00006E40
	private void OnEnable()
	{
		this.nextFrameTime = Time.time;
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00008C4D File Offset: 0x00006E4D
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

	// Token: 0x04000B93 RID: 2963
	public float framerate = 15f;

	// Token: 0x04000B94 RID: 2964
	[ReadOnly]
	public float frameTime;

	// Token: 0x04000B95 RID: 2965
	private float nextFrameTime = -1f;

	// Token: 0x04000B96 RID: 2966
	public Camera camera;
}
