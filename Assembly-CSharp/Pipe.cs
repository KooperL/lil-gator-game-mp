using System;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class Pipe : MonoBehaviour
{
	// Token: 0x06000141 RID: 321 RVA: 0x0001B530 File Offset: 0x00019730
	public void SetWater(bool hasWater)
	{
		GameObject[] array = this.waterObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(hasWater);
		}
	}

	// Token: 0x040001D5 RID: 469
	public GameObject[] waterObjects;
}
