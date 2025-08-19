using System;
using UnityEngine;

public static class PerlinUtil
{
	// Token: 0x060009D7 RID: 2519 RVA: 0x00009784 File Offset: 0x00007984
	public static float Perlin1(float seed, float t)
	{
		return 2f * Mathf.PerlinNoise(seed, t) - 1f;
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0003B9E8 File Offset: 0x00039BE8
	public static Vector3 Perlin2(float seed, float t)
	{
		Vector2 vector = new Vector2(Mathf.PerlinNoise(seed, t), Mathf.PerlinNoise(10000f + seed, t));
		return 2f * vector - Vector2.one;
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x0003BA2C File Offset: 0x00039C2C
	public static Vector3 Perlin3(float seed, float t)
	{
		Vector3 vector = new Vector3(Mathf.PerlinNoise(seed, t), Mathf.PerlinNoise(10000f + seed, t), Mathf.PerlinNoise(-10000f + seed, t));
		return 2f * vector - Vector3.one;
	}
}
