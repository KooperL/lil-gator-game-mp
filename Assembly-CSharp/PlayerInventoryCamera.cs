using System;
using UnityEngine;

// Token: 0x020001E9 RID: 489
public class PlayerInventoryCamera : MonoBehaviour
{
	// Token: 0x06000A59 RID: 2649 RVA: 0x00030DFB File Offset: 0x0002EFFB
	public void Activate()
	{
		if (this.mainCamera == null)
		{
			this.mainCamera = Camera.main;
			this.cameraCullingMask = this.mainCamera.cullingMask;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x00030E38 File Offset: 0x0002F038
	public void Deactivate()
	{
		this.mainCamera.cullingMask = this.cameraCullingMask;
		base.gameObject.SetActive(false);
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x00030E5C File Offset: 0x0002F05C
	private void Update()
	{
		if (this.mainCamera.transform.position == this.thisCamera.position)
		{
			this.mainCamera.cullingMask = this.playerOnlyMask;
		}
	}

	// Token: 0x04000D13 RID: 3347
	private Camera mainCamera;

	// Token: 0x04000D14 RID: 3348
	private LayerMask cameraCullingMask;

	// Token: 0x04000D15 RID: 3349
	public LayerMask playerOnlyMask;

	// Token: 0x04000D16 RID: 3350
	public Transform thisCamera;
}
