using System;
using UnityEngine;

public class DisableDynamicOcclusion : MonoBehaviour
{
	// Token: 0x0600060B RID: 1547 RVA: 0x000304F8 File Offset: 0x0002E6F8
	private void Start()
	{
		Renderer[] array = global::UnityEngine.Object.FindObjectsOfType<Renderer>(true);
		for (int i = 0; i < array.Length; i++)
		{
			array[i].allowOcclusionWhenDynamic = false;
		}
	}
}
