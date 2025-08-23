using System;
using UnityEngine.Events;

public class LogicStateCollectGrass : LogicState
{
	// Token: 0x06000925 RID: 2341 RVA: 0x00008D92 File Offset: 0x00006F92
	private void OnEnable()
	{
		TerrainDetails.onCutDetails.AddListener(new UnityAction<int>(this.OnDetailsCut));
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x00008DAA File Offset: 0x00006FAA
	private void OnDisable()
	{
		TerrainDetails.onCutDetails.RemoveListener(new UnityAction<int>(this.OnDetailsCut));
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x00008DC2 File Offset: 0x00006FC2
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
