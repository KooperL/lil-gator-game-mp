using System;
using UnityEngine;

public class ActivateItemCamera : MonoBehaviour
{
	// Token: 0x06000220 RID: 544 RVA: 0x00003C5C File Offset: 0x00001E5C
	private void OnEnable()
	{
		if (this != null && Player.uiItemCamera != null)
		{
			Player.uiItemCamera.SetActive(true);
		}
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00003C7F File Offset: 0x00001E7F
	private void OnDisable()
	{
		if (this != null && Player.uiItemCamera != null)
		{
			Player.uiItemCamera.SetActive(false);
		}
	}
}
