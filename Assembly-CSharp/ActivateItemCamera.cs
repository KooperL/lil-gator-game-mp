using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class ActivateItemCamera : MonoBehaviour
{
	// Token: 0x060001DC RID: 476 RVA: 0x0000A09D File Offset: 0x0000829D
	private void OnEnable()
	{
		if (this != null && Player.uiItemCamera != null)
		{
			Player.uiItemCamera.SetActive(true);
		}
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000A0C0 File Offset: 0x000082C0
	private void OnDisable()
	{
		if (this != null && Player.uiItemCamera != null)
		{
			Player.uiItemCamera.SetActive(false);
		}
	}
}
