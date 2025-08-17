using System;
using UnityEngine;

public class PlayerInventoryCamera : MonoBehaviour
{
	// Token: 0x06000C5A RID: 3162 RVA: 0x0000B81E File Offset: 0x00009A1E
	public void Activate()
	{
		if (this.mainCamera == null)
		{
			this.mainCamera = Camera.main;
			this.cameraCullingMask = this.mainCamera.cullingMask;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x0000B85B File Offset: 0x00009A5B
	public void Deactivate()
	{
		this.mainCamera.cullingMask = this.cameraCullingMask;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x0000B87F File Offset: 0x00009A7F
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
