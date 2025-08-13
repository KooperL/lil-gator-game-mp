using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class SetPipes : MonoBehaviour
{
	// Token: 0x0600011E RID: 286 RVA: 0x00006ED0 File Offset: 0x000050D0
	public void SetWater(bool hasWater)
	{
		Pipe[] array = this.pipes;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetWater(hasWater);
		}
	}

	// Token: 0x0400018C RID: 396
	public Pipe[] pipes;
}
