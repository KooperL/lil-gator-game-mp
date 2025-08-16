using System;
using UnityEngine;

public class LowFramerateCamera : MonoBehaviour
{
	// Token: 0x0600093A RID: 2362 RVA: 0x00008F2E File Offset: 0x0000712E
	public void OnValidate()
	{
		if (this.camera == null)
		{
			this.camera = base.GetComponent<Camera>();
		}
		this.frameTime = 1f / this.framerate;
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x00008F5C File Offset: 0x0000715C
	private void OnEnable()
	{
		this.nextFrameTime = Time.time;
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00008F69 File Offset: 0x00007169
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

	public float framerate = 15f;

	[ReadOnly]
	public float frameTime;

	private float nextFrameTime = -1f;

	public Camera camera;
}
