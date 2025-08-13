using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class CameraSpaceCanvas : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000019 RID: 25 RVA: 0x000021D6 File Offset: 0x000003D6
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

	// Token: 0x0600001A RID: 26 RVA: 0x000021F4 File Offset: 0x000003F4
	private void OnEnable()
	{
		CameraSpaceCanvas.instance = this;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000021FC File Offset: 0x000003FC
	public void Set(bool isInCamera)
	{
		this.canvas.renderMode = (isInCamera ? 1 : 0);
	}

	// Token: 0x04000019 RID: 25
	private static CameraSpaceCanvas instance;

	// Token: 0x0400001A RID: 26
	public Canvas canvas;
}
