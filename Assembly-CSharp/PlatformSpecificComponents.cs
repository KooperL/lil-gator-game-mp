using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class PlatformSpecificComponents : MonoBehaviour
{
	// Token: 0x060000C7 RID: 199 RVA: 0x00005AD8 File Offset: 0x00003CD8
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

	// Token: 0x0400010B RID: 267
	public MonoBehaviour[] components;

	// Token: 0x0400010C RID: 268
	public LODGroup lodGroup;

	// Token: 0x0400010D RID: 269
	public bool pc = true;

	// Token: 0x0400010E RID: 270
	public bool nx = true;
}
