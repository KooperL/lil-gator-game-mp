using System;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class DisableDynamicOcclusion : MonoBehaviour
{
	// Token: 0x060004C5 RID: 1221 RVA: 0x0001A0DC File Offset: 0x000182DC
	private void Start()
	{
		Renderer[] array = Object.FindObjectsOfType<Renderer>(true);
		for (int i = 0; i < array.Length; i++)
		{
			array[i].allowOcclusionWhenDynamic = false;
		}
	}
}
