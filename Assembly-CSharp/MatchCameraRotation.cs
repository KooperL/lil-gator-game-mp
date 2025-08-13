using System;
using UnityEngine;

// Token: 0x02000178 RID: 376
public class MatchCameraRotation : MonoBehaviour
{
	// Token: 0x060007C4 RID: 1988 RVA: 0x00025EEB File Offset: 0x000240EB
	private void Start()
	{
		base.transform.position = PlayerOrbitCamera.active.transform.position;
		base.transform.rotation = PlayerOrbitCamera.active.transform.rotation;
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00025F21 File Offset: 0x00024121
	private void LateUpdate()
	{
		base.transform.rotation = PlayerOrbitCamera.active.transform.rotation;
	}
}
