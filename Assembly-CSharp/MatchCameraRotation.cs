using System;
using UnityEngine;

public class MatchCameraRotation : MonoBehaviour
{
	// Token: 0x06000967 RID: 2407 RVA: 0x00009213 File Offset: 0x00007413
	private void Start()
	{
		base.transform.position = PlayerOrbitCamera.active.transform.position;
		base.transform.rotation = PlayerOrbitCamera.active.transform.rotation;
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00009249 File Offset: 0x00007449
	private void LateUpdate()
	{
		base.transform.rotation = PlayerOrbitCamera.active.transform.rotation;
	}
}
