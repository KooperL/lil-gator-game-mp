using System;
using UnityEngine;

public class MatchCameraRotation : MonoBehaviour
{
	// Token: 0x06000967 RID: 2407 RVA: 0x00009209 File Offset: 0x00007409
	private void Start()
	{
		base.transform.position = PlayerOrbitCamera.active.transform.position;
		base.transform.rotation = PlayerOrbitCamera.active.transform.rotation;
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x0000923F File Offset: 0x0000743F
	private void LateUpdate()
	{
		base.transform.rotation = PlayerOrbitCamera.active.transform.rotation;
	}
}
