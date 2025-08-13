using System;
using UnityEngine;

// Token: 0x020001EB RID: 491
public class LayerUtil : MonoBehaviour
{
	// Token: 0x170000DA RID: 218
	// (get) Token: 0x0600090D RID: 2317 RVA: 0x00008D98 File Offset: 0x00006F98
	public static LayerMask GroundLayers
	{
		get
		{
			return LayerUtil.Instance.groundLayers;
		}
	}

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x0600090E RID: 2318 RVA: 0x00008DA4 File Offset: 0x00006FA4
	public static LayerMask GroundLayersMinusPlayer
	{
		get
		{
			return LayerUtil.Instance.groundLayersMinusPlayer;
		}
	}

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x0600090F RID: 2319 RVA: 0x00008DB0 File Offset: 0x00006FB0
	public static LayerMask BalanceBeamAnchorLayers
	{
		get
		{
			return LayerUtil.Instance.balanceBeamAnchorLayers;
		}
	}

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x06000910 RID: 2320 RVA: 0x00008DBC File Offset: 0x00006FBC
	public static LayerMask HitLayers
	{
		get
		{
			return LayerUtil.Instance.hitLayers;
		}
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00038A54 File Offset: 0x00036C54
	public static void SnapToGround(Transform transform, float range = 5f)
	{
		Vector3 position = transform.position;
		if (LayerUtil.SnapToGround(ref position, range))
		{
			transform.position = position;
		}
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x00038A7C File Offset: 0x00036C7C
	public static bool SnapToGround(ref Vector3 point, float range = 5f)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(point + 0.5f * range * Vector3.up, Vector3.down, ref raycastHit, range, LayerUtil.GroundLayers))
		{
			point = raycastHit.point;
			return true;
		}
		return false;
	}

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x06000913 RID: 2323 RVA: 0x00008DC8 File Offset: 0x00006FC8
	private static LayerUtil Instance
	{
		get
		{
			if (LayerUtil.instance == null)
			{
				LayerUtil.instance = Object.FindObjectOfType<LayerUtil>();
			}
			return LayerUtil.instance;
		}
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x00008DE6 File Offset: 0x00006FE6
	private void Awake()
	{
		LayerUtil.instance = this;
	}

	// Token: 0x04000BB0 RID: 2992
	public const int physics = 26;

	// Token: 0x04000BB1 RID: 2993
	public const int staticOnly = 25;

	// Token: 0x04000BB2 RID: 2994
	public const int physicsNoPlayer = 23;

	// Token: 0x04000BB3 RID: 2995
	private static LayerUtil instance;

	// Token: 0x04000BB4 RID: 2996
	public LayerMask groundLayers;

	// Token: 0x04000BB5 RID: 2997
	public LayerMask groundLayersMinusPlayer;

	// Token: 0x04000BB6 RID: 2998
	public LayerMask balanceBeamAnchorLayers;

	// Token: 0x04000BB7 RID: 2999
	public LayerMask hitLayers;
}
