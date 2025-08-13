using System;
using UnityEngine;

// Token: 0x0200009C RID: 156
public class Armature : MonoBehaviour
{
	// Token: 0x06000233 RID: 563 RVA: 0x00003CBE File Offset: 0x00001EBE
	public void OnValidate()
	{
		if (this.transforms == null || (float)this.transforms.Length == 0f)
		{
			this.transforms = base.GetComponentsInChildren<Transform>();
		}
	}

	// Token: 0x04000324 RID: 804
	public Transform[] transforms;
}
