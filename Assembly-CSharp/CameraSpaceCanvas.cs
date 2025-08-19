using System;
using UnityEngine;

public class CameraSpaceCanvas : MonoBehaviour
{
	// (get) Token: 0x06000019 RID: 25 RVA: 0x000021D6 File Offset: 0x000003D6
	public static CameraSpaceCanvas c
	{
		get
		{
			if (CameraSpaceCanvas.instance == null)
			{
				CameraSpaceCanvas.instance = global::UnityEngine.Object.FindObjectOfType<CameraSpaceCanvas>();
			}
			return CameraSpaceCanvas.instance;
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000021F4 File Offset: 0x000003F4
	private void OnEnable()
	{
		CameraSpaceCanvas.instance = this;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000021FC File Offset: 0x000003FC
	public void Set(bool isInCamera)
	{
		this.canvas.renderMode = (isInCamera ? RenderMode.ScreenSpaceCamera : RenderMode.ScreenSpaceOverlay);
	}

	private static CameraSpaceCanvas instance;

	public Canvas canvas;
}
