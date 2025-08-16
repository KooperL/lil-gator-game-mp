using System;
using UnityEngine.Events;

public class LogicStateCollectGrass : LogicState
{
	// Token: 0x06000924 RID: 2340 RVA: 0x00008D73 File Offset: 0x00006F73
	private void OnEnable()
	{
		TerrainDetails.onCutDetails.AddListener(new UnityAction<int>(this.OnDetailsCut));
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x00008D8B File Offset: 0x00006F8B
	private void OnDisable()
	{
		TerrainDetails.onCutDetails.RemoveListener(new UnityAction<int>(this.OnDetailsCut));
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x00008DA3 File Offset: 0x00006FA3
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
