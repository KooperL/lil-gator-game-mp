using System;
using UnityEngine;

// Token: 0x0200020B RID: 523
public static class SwizzleExtensions
{
	// Token: 0x060009AD RID: 2477 RVA: 0x000095E4 File Offset: 0x000077E4
	public static Vector2 xy(this Vector4 vector)
	{
		return new Vector2(vector.x, vector.y);
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x000095F7 File Offset: 0x000077F7
	public static Vector2 zw(this Vector4 vector)
	{
		return new Vector2(vector.z, vector.w);
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0000960A File Offset: 0x0000780A
	public static Vector2 xz(this Vector3 vector)
	{
		return new Vector2(vector.x, vector.z);
	}
}
