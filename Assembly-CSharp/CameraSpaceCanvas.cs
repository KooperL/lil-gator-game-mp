using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class CameraSpaceCanvas : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000019 RID: 25 RVA: 0x0000267C File Offset: 0x0000087C
	public static CameraSpaceCanvas c
	{
		get
		{
			if (CameraSpaceCanvas.instance == null)
			{
				CameraSpaceCanvas.instance = Object.FindObjectOfType<CameraSpaceCanvas>();
			}
			return CameraSpaceCanvas.instance;
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000269A File Offset: 0x0000089A
	private void OnEnable()
	{
		CameraSpaceCanvas.instance = this;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000026A2 File Offset: 0x000008A2
	public void Set(bool isInCamera)
	{
		this.canvas.renderMode = (isInCamera ? RenderMode.ScreenSpaceCamera : RenderMode.ScreenSpaceOverlay);
	}

	// Token: 0x04000019 RID: 25
	private static CameraSpaceCanvas instance;

	// Token: 0x0400001A RID: 26
	public Canvas canvas;
}
