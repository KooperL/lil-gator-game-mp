using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001C4 RID: 452
public class ForkByAmountBroken : MonoBehaviour
{
	// Token: 0x0600088B RID: 2187 RVA: 0x00037670 File Offset: 0x00035870
	public void Fork()
	{
		int num = 0;
		BreakableObject[] array = this.breakableObjects;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].isBroken)
			{
				num++;
			}
		}
		int num2 = -1;
		int num3 = -1;
		for (int j = 0; j < this.forkDirections.Length; j++)
		{
			if (num >= this.forkDirections[j].minAmount && (this.forkDirections[j].minAmount > num3 || num2 == -1))
			{
				num2 = j;
				num3 = this.forkDirections[j].minAmount;
			}
		}
		this.forkDirections[num2].onFork.Invoke();
	}

	// Token: 0x04000B1E RID: 2846
	public ForkByAmountBroken.ForkDirection[] forkDirections;

	// Token: 0x04000B1F RID: 2847
	public BreakableObject[] breakableObjects;

	// Token: 0x020001C5 RID: 453
	[Serializable]
	public struct ForkDirection
	{
		// Token: 0x04000B20 RID: 2848
		public int minAmount;

		// Token: 0x04000B21 RID: 2849
		public UnityEvent onFork;
	}
}
