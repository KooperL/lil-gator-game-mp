using System;
using UnityEngine;

public class SetPipes : MonoBehaviour
{
	// Token: 0x0600014B RID: 331 RVA: 0x0001BBF4 File Offset: 0x00019DF4
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
