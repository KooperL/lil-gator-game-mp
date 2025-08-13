using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class Pipe : MonoBehaviour
{
	// Token: 0x0600011C RID: 284 RVA: 0x00006E9C File Offset: 0x0000509C
	public void SetWater(bool hasWater)
	{
		GameObject[] array = this.waterObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(hasWater);
		}
	}

	// Token: 0x0400018B RID: 395
	public GameObject[] waterObjects;
}
