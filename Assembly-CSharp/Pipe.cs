using System;
using UnityEngine;

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

	public GameObject[] waterObjects;
}
