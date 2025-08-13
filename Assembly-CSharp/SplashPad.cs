using System;
using UnityEngine;

// Token: 0x02000045 RID: 69
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

	// Token: 0x0400017F RID: 383
	public GameObject nonWaterObjects;

	// Token: 0x04000180 RID: 384
	public GameObject waterObjects;

	// Token: 0x04000181 RID: 385
	public WaterPump[] pumps;
}
