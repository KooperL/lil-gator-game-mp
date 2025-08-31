using System;
using UnityEngine;

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

	public MonoBehaviour[] components;

	public LODGroup lodGroup;

	public bool pc = true;

	public bool nx = true;
}
