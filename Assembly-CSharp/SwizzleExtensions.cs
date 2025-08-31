using System;
using UnityEngine;

public static class SwizzleExtensions
{
	// Token: 0x06000836 RID: 2102 RVA: 0x000273F4 File Offset: 0x000255F4
	public static Vector2 xy(this Vector4 vector)
	{
		return new Vector2(vector.x, vector.y);
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00027407 File Offset: 0x00025607
	public static Vector2 zw(this Vector4 vector)
	{
		return new Vector2(vector.z, vector.w);
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x0002741A File Offset: 0x0002561A
	public static Vector2 xz(this Vector3 vector)
	{
		return new Vector2(vector.x, vector.z);
	}
}
