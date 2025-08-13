using System;
using UnityEngine;

// Token: 0x020001EE RID: 494
public class MatchCameraRotation : MonoBehaviour
{
	// Token: 0x06000926 RID: 2342 RVA: 0x00008ED8 File Offset: 0x000070D8
	private void Start()
	{
		base.transform.position = PlayerOrbitCamera.active.transform.position;
		base.transform.rotation = PlayerOrbitCamera.active.transform.rotation;
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x00008F0E File Offset: 0x0000710E
	private void LateUpdate()
	{
		base.transform.rotation = PlayerOrbitCamera.active.transform.rotation;
	}
}
