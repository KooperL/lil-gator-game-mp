using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
public class SplashPad : MonoBehaviour
{
	// Token: 0x06000135 RID: 309 RVA: 0x000030F7 File Offset: 0x000012F7
	private void Start()
	{
		this.RefreshState();
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0001B3F4 File Offset: 0x000195F4
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

	// Token: 0x040001C9 RID: 457
	public GameObject nonWaterObjects;

	// Token: 0x040001CA RID: 458
	public GameObject waterObjects;

	// Token: 0x040001CB RID: 459
	public WaterPump[] pumps;
}
