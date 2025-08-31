using System;
using UnityEngine.Events;

public class LogicStateCollectGrass : LogicState
{
	// Token: 0x06000785 RID: 1925 RVA: 0x000252E2 File Offset: 0x000234E2
	private void OnEnable()
	{
		TerrainDetails.onCutDetails.AddListener(new UnityAction<int>(this.OnDetailsCut));
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x000252FA File Offset: 0x000234FA
	private void OnDisable()
	{
		TerrainDetails.onCutDetails.RemoveListener(new UnityAction<int>(this.OnDetailsCut));
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x00025312 File Offset: 0x00023512
	public void OnDetailsCut(int cutAmount)
	{
		this.currentCutAmount += cutAmount;
		if (this.currentCutAmount > this.cutAmountNeeded)
		{
			this.LogicCompleted();
		}
	}

	public int cutAmountNeeded = 30;

	private int currentCutAmount;
}
