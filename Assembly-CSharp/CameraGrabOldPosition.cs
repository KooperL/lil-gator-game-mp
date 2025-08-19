using System;
using UnityEngine;

public class CameraGrabOldPosition : MonoBehaviour
{
	// Token: 0x060002A2 RID: 674 RVA: 0x00020D80 File Offset: 0x0001EF80
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
