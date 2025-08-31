using System;
using UnityEngine;

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

	public Pipe[] pipes;
}
