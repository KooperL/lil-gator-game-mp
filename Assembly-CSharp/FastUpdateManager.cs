using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000169 RID: 361
public class FastUpdateManager : MonoBehaviour
{
	// Token: 0x060006C2 RID: 1730 RVA: 0x00031B3C File Offset: 0x0002FD3C
	private void FixedUpdate()
	{
		this.isFixedUpdate = true;
		this.fixedIndex++;
		this.fixedIndex %= 8;
		for (int i = this.fixedIndex % 4; i < FastUpdateManager.fixedUpdate4.Count; i += 4)
		{
			if (FastUpdateManager.fixedUpdate4[i] == null)
			{
				FastUpdateManager.fixedUpdate4.RemoveAt(i);
				i--;
			}
			else
			{
				FastUpdateManager.fixedUpdate4[i].ManagedUpdate();
			}
		}
		for (int j = this.fixedIndex % 8; j < FastUpdateManager.fixedUpdate8.Count; j += 8)
		{
			if (FastUpdateManager.fixedUpdate8[j] == null)
			{
				FastUpdateManager.fixedUpdate8.RemoveAt(j);
				j--;
			}
			else
			{
				FastUpdateManager.fixedUpdate8[j].ManagedUpdate();
			}
		}
		for (int k = this.fixedIndex % 16; k < FastUpdateManager.fixedUpdate16.Count; k += 16)
		{
			if (FastUpdateManager.fixedUpdate16[k] == null)
			{
				FastUpdateManager.fixedUpdate16.RemoveAt(k);
				k--;
			}
			else
			{
				FastUpdateManager.fixedUpdate16[k].ManagedUpdate();
			}
		}
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00031C4C File Offset: 0x0002FE4C
	private void Update()
	{
		this.index++;
		this.index %= 8;
		for (int i = 0; i < FastUpdateManager.updateEvery1.Count; i++)
		{
			if (FastUpdateManager.updateEvery1[i] == null)
			{
				FastUpdateManager.updateEvery1.RemoveAt(i);
				i--;
			}
			else
			{
				FastUpdateManager.updateEvery1[i].ManagedUpdate();
			}
		}
		if (this.isFixedUpdate)
		{
			this.hasSkipped8 = true;
			this.skipped8[this.index % 8] = true;
			this.hasSkipped4 = true;
			this.skipped4[this.index % 4] = true;
		}
		if (this.hasSkipped8)
		{
			this.skipped8FrameCount++;
		}
		if (this.hasSkipped4)
		{
			this.skipped4FrameCount++;
		}
		if (!this.isFixedUpdate || this.nonFixedCounter >= 25)
		{
			for (int j = 0; j < FastUpdateManager.updateEveryNonFixed.Count; j++)
			{
				if (FastUpdateManager.updateEveryNonFixed[j] == null)
				{
					FastUpdateManager.updateEveryNonFixed.RemoveAt(j);
					j--;
				}
				else
				{
					FastUpdateManager.updateEveryNonFixed[j].ManagedUpdate();
				}
			}
			this.nonFixedCounter = 0;
		}
		else
		{
			this.nonFixedCounter++;
		}
		if (!this.isFixedUpdate || this.skipped8FrameCount >= 8)
		{
			int num = this.index % 8;
			for (int k = 0; k < FastUpdateManager.updateEvery8.Count; k++)
			{
				if (this.skipped8[k % 8] || k % 8 == num)
				{
					if (FastUpdateManager.updateEvery8[k] == null)
					{
						FastUpdateManager.updateEvery8.RemoveAt(k);
						k--;
					}
					else
					{
						FastUpdateManager.updateEvery8[k].ManagedUpdate();
					}
				}
			}
			if (this.hasSkipped8)
			{
				for (int l = 0; l < this.skipped8.Length; l++)
				{
					this.skipped8[l] = false;
				}
				this.skipped8FrameCount = 0;
				this.hasSkipped8 = false;
			}
		}
		if (!this.isFixedUpdate || this.skipped4FrameCount >= 4)
		{
			int num2 = this.index % 4;
			for (int m = 0; m < FastUpdateManager.updateEvery4.Count; m++)
			{
				if (this.skipped4[m % 4] || m % 4 == num2)
				{
					if (FastUpdateManager.updateEvery4[m] == null)
					{
						FastUpdateManager.updateEvery4.RemoveAt(m);
						m--;
					}
					else
					{
						FastUpdateManager.updateEvery4[m].ManagedUpdate();
					}
				}
			}
			if (this.hasSkipped4)
			{
				for (int n = 0; n < this.skipped4.Length; n++)
				{
					this.skipped4[n] = false;
				}
				this.skipped4FrameCount = 0;
				this.hasSkipped4 = false;
			}
		}
		this.isFixedUpdate = false;
	}

	// Token: 0x0400090D RID: 2317
	public static List<IManagedUpdate> updateEvery1 = new List<IManagedUpdate>();

	// Token: 0x0400090E RID: 2318
	public static List<IManagedUpdate> updateEvery4 = new List<IManagedUpdate>();

	// Token: 0x0400090F RID: 2319
	public static List<IManagedUpdate> updateEvery8 = new List<IManagedUpdate>();

	// Token: 0x04000910 RID: 2320
	public static List<IManagedUpdate> updateEveryNonFixed = new List<IManagedUpdate>();

	// Token: 0x04000911 RID: 2321
	private const int maxFixedBeforeNonFixed = 25;

	// Token: 0x04000912 RID: 2322
	private int nonFixedCounter;

	// Token: 0x04000913 RID: 2323
	public static List<IManagedUpdate> fixedUpdate4 = new List<IManagedUpdate>();

	// Token: 0x04000914 RID: 2324
	public static List<IManagedUpdate> fixedUpdate8 = new List<IManagedUpdate>();

	// Token: 0x04000915 RID: 2325
	public static List<IManagedUpdate> fixedUpdate16 = new List<IManagedUpdate>();

	// Token: 0x04000916 RID: 2326
	public int index;

	// Token: 0x04000917 RID: 2327
	public int fixedIndex;

	// Token: 0x04000918 RID: 2328
	private bool isFixedUpdate;

	// Token: 0x04000919 RID: 2329
	private bool[] skipped8 = new bool[8];

	// Token: 0x0400091A RID: 2330
	private bool hasSkipped8;

	// Token: 0x0400091B RID: 2331
	private int skipped8FrameCount;

	// Token: 0x0400091C RID: 2332
	private bool[] skipped4 = new bool[4];

	// Token: 0x0400091D RID: 2333
	private bool hasSkipped4;

	// Token: 0x0400091E RID: 2334
	private int skipped4FrameCount;
}
