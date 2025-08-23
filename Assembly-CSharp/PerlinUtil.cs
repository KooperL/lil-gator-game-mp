using System;
using UnityEngine;

public static class PerlinUtil
{
	// Token: 0x060009D8 RID: 2520 RVA: 0x00009784 File Offset: 0x00007984
	public static float Perlin1(float seed, float t)
	{
		return 2f * Mathf.PerlinNoise(seed, t) - 1f;
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x0003BCD4 File Offset: 0x00039ED4
	public static Vector3 Perlin2(float seed, float t)
	{
		Vector2 vector = new Vector2(Mathf.PerlinNoise(seed, t), Mathf.PerlinNoise(10000f + seed, t));
		return 2f * vector - Vector2.one;
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x0003BD18 File Offset: 0x00039F18
	public static Vector3 Perlin3(float seed, float t)
	{
		Vector3 vector = new Vector3(Mathf.PerlinNoise(seed, t), Mathf.PerlinNoise(10000f + seed, t), Mathf.PerlinNoise(-10000f + seed, t));
		return 2f * vector - Vector3.one;
	}
}
