using System;
using UnityEngine;

public class SplashPad : MonoBehaviour
{
	// Token: 0x0600013D RID: 317 RVA: 0x0000319A File Offset: 0x0000139A
	private void Start()
	{
		this.RefreshState();
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0001BBE4 File Offset: 0x00019DE4
	[ContextMenu("RefreshState")]
	public void RefreshState()
	{
		bool flag = true;
		WaterPump[] array = this.pumps;
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].State)
			{
				flag = false;
				break;
			}
		}
		if (this.nonWaterObjects != null)
		{
			this.nonWaterObjects.SetActive(!flag);
		}
		this.waterObjects.SetActive(flag);
	}

	public GameObject nonWaterObjects;

	public GameObject waterObjects;

	public WaterPump[] pumps;
}
