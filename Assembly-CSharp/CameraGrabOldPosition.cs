using System;
using UnityEngine;

public class CameraGrabOldPosition : MonoBehaviour
{
	// Token: 0x0600024B RID: 587 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
	private void OnEnable()
	{
		if (this.mainCamera == null)
		{
			this.mainCamera = Camera.main;
		}
		base.transform.position = this.mainCamera.transform.position;
		base.transform.rotation = this.mainCamera.transform.rotation;
	}

	private Camera mainCamera;
}
