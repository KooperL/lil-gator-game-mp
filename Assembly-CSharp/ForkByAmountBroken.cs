using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200015B RID: 347
public class ForkByAmountBroken : MonoBehaviour
{
	// Token: 0x06000741 RID: 1857 RVA: 0x000243A0 File Offset: 0x000225A0
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

	// Token: 0x04000980 RID: 2432
	public ForkByAmountBroken.ForkDirection[] forkDirections;

	// Token: 0x04000981 RID: 2433
	public BreakableObject[] breakableObjects;

	// Token: 0x020003C0 RID: 960
	[Serializable]
	public struct ForkDirection
	{
		// Token: 0x04001BBA RID: 7098
		public int minAmount;

		// Token: 0x04001BBB RID: 7099
		public UnityEvent onFork;
	}
}
