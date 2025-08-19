using System;
using UnityEngine;

public class Pipe : MonoBehaviour
{
	// Token: 0x06000149 RID: 329 RVA: 0x0001BD20 File Offset: 0x00019F20
	public void SetWater(bool hasWater)
	{
		GameObject[] array = this.waterObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(hasWater);
		}
	}

	public GameObject[] waterObjects;
}
