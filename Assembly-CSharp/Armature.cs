using System;
using UnityEngine;

public class Armature : MonoBehaviour
{
	// Token: 0x06000240 RID: 576 RVA: 0x00003DAA File Offset: 0x00001FAA
	public void OnValidate()
	{
		if (this.transforms == null || (float)this.transforms.Length == 0f)
		{
			this.transforms = base.GetComponentsInChildren<Transform>();
		}
	}

	public Transform[] transforms;
}
