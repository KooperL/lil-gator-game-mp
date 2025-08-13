using System;
using UnityEngine;

// Token: 0x02000274 RID: 628
public class PlayerInventoryCamera : MonoBehaviour
{
	// Token: 0x06000C0E RID: 3086 RVA: 0x0000B516 File Offset: 0x00009716
	public void Activate()
	{
		if (this.mainCamera == null)
		{
			this.mainCamera = Camera.main;
			this.cameraCullingMask = this.mainCamera.cullingMask;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x0000B553 File Offset: 0x00009753
	public void Deactivate()
	{
		this.mainCamera.cullingMask = this.cameraCullingMask;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x0000B577 File Offset: 0x00009777
	private void Update()
	{
		if (this.mainCamera.transform.position == this.thisCamera.position)
		{
			this.mainCamera.cullingMask = this.playerOnlyMask;
		}
	}

	// Token: 0x04000F58 RID: 3928
	private Camera mainCamera;

	// Token: 0x04000F59 RID: 3929
	private LayerMask cameraCullingMask;

	// Token: 0x04000F5A RID: 3930
	public LayerMask playerOnlyMask;

	// Token: 0x04000F5B RID: 3931
	public Transform thisCamera;
}
