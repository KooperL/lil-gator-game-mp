using System;
using UnityEngine;

// Token: 0x0200018C RID: 396
public static class PerlinUtil
{
	// Token: 0x06000821 RID: 2081 RVA: 0x00026FFC File Offset: 0x000251FC
	public static float Perlin1(float seed, float t)
	{
		return 2f * Mathf.PerlinNoise(seed, t) - 1f;
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x00027014 File Offset: 0x00025214
	public static Vector3 Perlin2(float seed, float t)
	{
		Vector2 vector = new Vector2(Mathf.PerlinNoise(seed, t), Mathf.PerlinNoise(10000f + seed, t));
		return 2f * vector - Vector2.one;
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00027058 File Offset: 0x00025258
	public static Vector3 Perlin3(float seed, float t)
	{
		Vector3 vector = new Vector3(Mathf.PerlinNoise(seed, t), Mathf.PerlinNoise(10000f + seed, t), Mathf.PerlinNoise(-10000f + seed, t));
		return 2f * vector - Vector3.one;
	}
}
