using System;
using UnityEngine;

public class LayerUtil : MonoBehaviour
{
	// (get) Token: 0x060007AE RID: 1966 RVA: 0x00025923 File Offset: 0x00023B23
	public static LayerMask GroundLayers
	{
		get
		{
			return LayerUtil.Instance.groundLayers;
		}
	}

	// (get) Token: 0x060007AF RID: 1967 RVA: 0x0002592F File Offset: 0x00023B2F
	public static LayerMask GroundLayersMinusPlayer
	{
		get
		{
			return LayerUtil.Instance.groundLayersMinusPlayer;
		}
	}

	// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0002593B File Offset: 0x00023B3B
	public static LayerMask BalanceBeamAnchorLayers
	{
		get
		{
			return LayerUtil.Instance.balanceBeamAnchorLayers;
		}
	}

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

	public const int physics = 26;

	public const int staticOnly = 25;

	public const int physicsNoPlayer = 23;

	private static LayerUtil instance;

	public LayerMask groundLayers;

	public LayerMask groundLayersMinusPlayer;

	public LayerMask balanceBeamAnchorLayers;

	public LayerMask hitLayers;
}
