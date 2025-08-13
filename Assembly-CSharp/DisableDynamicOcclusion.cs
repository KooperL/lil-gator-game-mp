using System;
using UnityEngine;

// Token: 0x02000138 RID: 312
public class DisableDynamicOcclusion : MonoBehaviour
{
	// Token: 0x060005D1 RID: 1489 RVA: 0x0002EDE8 File Offset: 0x0002CFE8
	private void Start()
	{
		Renderer[] array = Object.FindObjectsOfType<Renderer>(true);
		for (int i = 0; i < array.Length; i++)
		{
			array[i].allowOcclusionWhenDynamic = false;
		}
	}
}
