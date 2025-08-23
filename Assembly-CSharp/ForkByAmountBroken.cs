using System;
using UnityEngine;
using UnityEngine.Events;

public class ForkByAmountBroken : MonoBehaviour
{
	// Token: 0x060008CC RID: 2252 RVA: 0x000392A8 File Offset: 0x000374A8
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

	public ForkByAmountBroken.ForkDirection[] forkDirections;

	public BreakableObject[] breakableObjects;

	[Serializable]
	public struct ForkDirection
	{
		public int minAmount;

		public UnityEvent onFork;
	}
}
