using System;
using UnityEngine;

public class PlayerInventoryCamera : MonoBehaviour
{
	// Token: 0x06000C5A RID: 3162 RVA: 0x0000B828 File Offset: 0x00009A28
	public void Activate()
	{
		if (this.mainCamera == null)
		{
			this.mainCamera = Camera.main;
			this.cameraCullingMask = this.mainCamera.cullingMask;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x0000B865 File Offset: 0x00009A65
	public void Deactivate()
	{
		this.mainCamera.cullingMask = this.cameraCullingMask;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x0000B889 File Offset: 0x00009A89
	private void Update()
	{
		if (this.mainCamera.transform.position == this.thisCamera.position)
		{
			this.mainCamera.cullingMask = this.playerOnlyMask;
		}
	}

	private Camera mainCamera;

	private LayerMask cameraCullingMask;

	public LayerMask playerOnlyMask;

	public Transform thisCamera;
}
