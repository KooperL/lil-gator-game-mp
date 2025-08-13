using System;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class PlatformSpecificComponents : MonoBehaviour
{
	// Token: 0x060000DA RID: 218 RVA: 0x0001A1B4 File Offset: 0x000183B4
	private void Start()
	{
		bool flag = this.pc;
		for (int i = 0; i < this.components.Length; i++)
		{
			this.components[i].enabled = flag;
		}
		if (this.lodGroup != null)
		{
			this.lodGroup.enabled = flag;
		}
	}

	// Token: 0x04000138 RID: 312
	public MonoBehaviour[] components;

	// Token: 0x04000139 RID: 313
	public LODGroup lodGroup;

	// Token: 0x0400013A RID: 314
	public bool pc = true;

	// Token: 0x0400013B RID: 315
	public bool nx = true;
}
