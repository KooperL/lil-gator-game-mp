using System;
using UnityEngine.Events;

// Token: 0x020001E0 RID: 480
public class LogicStateCollectGrass : LogicState
{
	// Token: 0x060008E4 RID: 2276 RVA: 0x00008A5F File Offset: 0x00006C5F
	private void OnEnable()
	{
		TerrainDetails.onCutDetails.AddListener(new UnityAction<int>(this.OnDetailsCut));
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00008A77 File Offset: 0x00006C77
	private void OnDisable()
	{
		TerrainDetails.onCutDetails.RemoveListener(new UnityAction<int>(this.OnDetailsCut));
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00008A8F File Offset: 0x00006C8F
	public void OnDetailsCut(int cutAmount)
	{
		this.currentCutAmount += cutAmount;
		if (this.currentCutAmount > this.cutAmountNeeded)
		{
			this.LogicCompleted();
		}
	}

	// Token: 0x04000B82 RID: 2946
	public int cutAmountNeeded = 30;

	// Token: 0x04000B83 RID: 2947
	private int currentCutAmount;
}
