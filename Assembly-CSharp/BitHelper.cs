using System;
using System.Collections;
using UnityEngine;

public static class BitHelper
{
	// Token: 0x06000268 RID: 616 RVA: 0x00004085 File Offset: 0x00002285
	public static bool ReadBit(int integer, int i)
	{
		return ((integer >> i) & 1) == 1;
	}

	// Token: 0x06000269 RID: 617 RVA: 0x00004092 File Offset: 0x00002292
	public static BitArray ToBits(int integer)
	{
		return new BitArray(new int[] { integer });
	}

	// Token: 0x0600026A RID: 618 RVA: 0x00020054 File Offset: 0x0001E254
	public static int ToInteger(BitArray bitArray)
	{
		if (bitArray.Length > 32)
		{
			Debug.LogError("Argument length shall be at most 32 bits.");
		}
		int[] array = new int[1];
		bitArray.CopyTo(array, 0);
		return array[0];
	}
}
