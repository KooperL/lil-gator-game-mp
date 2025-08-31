using System;
using UnityEngine;

public class SplashPad : MonoBehaviour
{
	// Token: 0x06000110 RID: 272 RVA: 0x00006CAC File Offset: 0x00004EAC
	private void Start()
	{
		this.RefreshState();
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00006CB4 File Offset: 0x00004EB4
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
