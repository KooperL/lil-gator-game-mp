using System;
using UnityEngine;

// Token: 0x0200007B RID: 123
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

	// Token: 0x0400029E RID: 670
	public Transform[] transforms;
}
