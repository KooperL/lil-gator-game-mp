using System;
using UnityEngine;
using UnityEngine.Events;

public class ForkByAmountBroken : MonoBehaviour
{
	// Token: 0x060008CB RID: 2251 RVA: 0x00038FBC File Offset: 0x000371BC
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
