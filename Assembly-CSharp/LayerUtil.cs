using System;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class LayerUtil : MonoBehaviour
{
	// Token: 0x17000068 RID: 104
	// (get) Token: 0x060007AE RID: 1966 RVA: 0x00025923 File Offset: 0x00023B23
	public static LayerMask GroundLayers
	{
		get
		{
			return LayerUtil.Instance.groundLayers;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x060007AF RID: 1967 RVA: 0x0002592F File Offset: 0x00023B2F
	public static LayerMask GroundLayersMinusPlayer
	{
		get
		{
			return LayerUtil.Instance.groundLayersMinusPlayer;
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0002593B File Offset: 0x00023B3B
	public static LayerMask BalanceBeamAnchorLayers
	{
		get
		{
			return LayerUtil.Instance.balanceBeamAnchorLayers;
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00025947 File Offset: 0x00023B47
	public static LayerMask HitLayers
	{
		get
		{
			return LayerUtil.Instance.hitLayers;
		}
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00025954 File Offset: 0x00023B54
	public static void SnapToGround(Transform transform, float range = 5f)
	{
		Vector3 position = transform.position;
		if (LayerUtil.SnapToGround(ref position, range))
		{
			transform.position = position;
		}
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0002597C File Offset: 0x00023B7C
	public static bool SnapToGround(ref Vector3 point, float range = 5f)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(point + 0.5f * range * Vector3.up, Vector3.down, out raycastHit, range, LayerUtil.GroundLayers))
		{
			point = raycastHit.point;
			return true;
		}
		return false;
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000259CE File Offset: 0x00023BCE
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

	// Token: 0x060007B5 RID: 1973 RVA: 0x000259EC File Offset: 0x00023BEC
	private void Awake()
	{
		LayerUtil.instance = this;
	}

	// Token: 0x040009DF RID: 2527
	public const int physics = 26;

	// Token: 0x040009E0 RID: 2528
	public const int staticOnly = 25;

	// Token: 0x040009E1 RID: 2529
	public const int physicsNoPlayer = 23;

	// Token: 0x040009E2 RID: 2530
	private static LayerUtil instance;

	// Token: 0x040009E3 RID: 2531
	public LayerMask groundLayers;

	// Token: 0x040009E4 RID: 2532
	public LayerMask groundLayersMinusPlayer;

	// Token: 0x040009E5 RID: 2533
	public LayerMask balanceBeamAnchorLayers;

	// Token: 0x040009E6 RID: 2534
	public LayerMask hitLayers;
}
