using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public static class BitHelper
{
	// Token: 0x0600025B RID: 603 RVA: 0x00003F99 File Offset: 0x00002199
	public static bool ReadBit(int integer, int i)
	{
		return ((integer >> i) & 1) == 1;
	}

	// Token: 0x0600025C RID: 604 RVA: 0x00003FA6 File Offset: 0x000021A6
	public static BitArray ToBits(int integer)
	{
		return new BitArray(new int[] { integer });
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0001F610 File Offset: 0x0001D810
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
