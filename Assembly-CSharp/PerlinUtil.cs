using System;
using UnityEngine;

// Token: 0x02000203 RID: 515
public static class PerlinUtil
{
	// Token: 0x06000990 RID: 2448 RVA: 0x00009446 File Offset: 0x00007646
	public static float Perlin1(float seed, float t)
	{
		return 2f * Mathf.PerlinNoise(seed, t) - 1f;
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x00039F9C File Offset: 0x0003819C
	public static Vector3 Perlin2(float seed, float t)
	{
		Vector2 vector;
		vector..ctor(Mathf.PerlinNoise(seed, t), Mathf.PerlinNoise(10000f + seed, t));
		return 2f * vector - Vector2.one;
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x00039FE0 File Offset: 0x000381E0
	public static Vector3 Perlin3(float seed, float t)
	{
		Vector3 vector;
		vector..ctor(Mathf.PerlinNoise(seed, t), Mathf.PerlinNoise(10000f + seed, t), Mathf.PerlinNoise(-10000f + seed, t));
		return 2f * vector - Vector3.one;
	}
}
