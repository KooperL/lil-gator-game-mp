using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class SetPipes : MonoBehaviour
{
	// Token: 0x06000143 RID: 323 RVA: 0x0001B55C File Offset: 0x0001975C
	public void SetWater(bool hasWater)
	{
		Pipe[] array = this.pipes;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetWater(hasWater);
		}
	}

	// Token: 0x040001D6 RID: 470
	public Pipe[] pipes;
}
