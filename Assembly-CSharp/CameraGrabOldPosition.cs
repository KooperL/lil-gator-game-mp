using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class CameraGrabOldPosition : MonoBehaviour
{
	// Token: 0x06000295 RID: 661 RVA: 0x0002034C File Offset: 0x0001E54C
	private void OnEnable()
	{
		if (this.mainCamera == null)
		{
			this.mainCamera = Camera.main;
		}
		base.transform.position = this.mainCamera.transform.position;
		base.transform.rotation = this.mainCamera.transform.rotation;
	}

	// Token: 0x0400039A RID: 922
	private Camera mainCamera;
}
