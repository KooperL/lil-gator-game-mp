using System;
using UnityEngine;

public class LowFramerateCamera : MonoBehaviour
{
	// Token: 0x0600093A RID: 2362 RVA: 0x00008F43 File Offset: 0x00007143
	public void OnValidate()
	{
		if (this.camera == null)
		{
			this.camera = base.GetComponent<Camera>();
		}
		this.frameTime = 1f / this.framerate;
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x00008F71 File Offset: 0x00007171
	private void OnEnable()
	{
		this.nextFrameTime = Time.time;
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00008F7E File Offset: 0x0000717E
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
