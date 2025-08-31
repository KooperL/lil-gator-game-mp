using System;
using System.Collections;
using UnityEngine;

public static class BitHelper
{
	// Token: 0x06000223 RID: 547 RVA: 0x0000BCE6 File Offset: 0x00009EE6
	public static bool ReadBit(int integer, int i)
	{
		return ((integer >> i) & 1) == 1;
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0000BCF3 File Offset: 0x00009EF3
	public static BitArray ToBits(int integer)
	{
		return new BitArray(new int[] { integer });
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000BD04 File Offset: 0x00009F04
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
