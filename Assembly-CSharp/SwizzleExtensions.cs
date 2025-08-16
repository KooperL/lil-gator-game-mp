using System;
using UnityEngine;

public static class SwizzleExtensions
{
	// Token: 0x060009F5 RID: 2549 RVA: 0x00009903 File Offset: 0x00007B03
	public static Vector2 xy(this Vector4 vector)
	{
		return new Vector2(vector.x, vector.y);
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x00009916 File Offset: 0x00007B16
	public static Vector2 zw(this Vector4 vector)
	{
		return new Vector2(vector.z, vector.w);
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x00009929 File Offset: 0x00007B29
	public static Vector2 xz(this Vector3 vector)
	{
		return new Vector2(vector.x, vector.z);
	}
}
