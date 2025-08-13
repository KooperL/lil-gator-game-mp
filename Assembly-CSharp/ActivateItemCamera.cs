using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class ActivateItemCamera : MonoBehaviour
{
	// Token: 0x06000213 RID: 531 RVA: 0x00003B70 File Offset: 0x00001D70
	private void OnEnable()
	{
		if (this != null && Player.uiItemCamera != null)
		{
			Player.uiItemCamera.SetActive(true);
		}
	}

	// Token: 0x06000214 RID: 532 RVA: 0x00003B93 File Offset: 0x00001D93
	private void OnDisable()
	{
		if (this != null && Player.uiItemCamera != null)
		{
			Player.uiItemCamera.SetActive(false);
		}
	}
}
