using System;
using UnityEngine;

public class Armature : MonoBehaviour
{
	// Token: 0x060001FC RID: 508 RVA: 0x0000AE28 File Offset: 0x00009028
	public void OnValidate()
	{
		if (this.transforms == null || (float)this.transforms.Length == 0f)
		{
			this.transforms = base.GetComponentsInChildren<Transform>();
		}
	}

	public Transform[] transforms;
}
