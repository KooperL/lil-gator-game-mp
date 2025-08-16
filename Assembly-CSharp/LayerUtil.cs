using System;
using UnityEngine;

public class LayerUtil : MonoBehaviour
{
	// (get) Token: 0x0600094E RID: 2382 RVA: 0x000090B4 File Offset: 0x000072B4
	public static LayerMask GroundLayers
	{
		get
		{
			return LayerUtil.Instance.groundLayers;
		}
	}

	// (get) Token: 0x0600094F RID: 2383 RVA: 0x000090C0 File Offset: 0x000072C0
	public static LayerMask GroundLayersMinusPlayer
	{
		get
		{
			return LayerUtil.Instance.groundLayersMinusPlayer;
		}
	}

	// (get) Token: 0x06000950 RID: 2384 RVA: 0x000090CC File Offset: 0x000072CC
	public static LayerMask BalanceBeamAnchorLayers
	{
		get
		{
			return LayerUtil.Instance.balanceBeamAnchorLayers;
		}
	}

	// (get) Token: 0x06000951 RID: 2385 RVA: 0x000090D8 File Offset: 0x000072D8
	public static LayerMask HitLayers
	{
		get
		{
			return LayerUtil.Instance.hitLayers;
		}
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x0003A1E4 File Offset: 0x000383E4
	public static void SnapToGround(Transform transform, float range = 5f)
	{
		Vector3 position = transform.position;
		if (LayerUtil.SnapToGround(ref position, range))
		{
			transform.position = position;
		}
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x0003A20C File Offset: 0x0003840C
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

	// (get) Token: 0x06000954 RID: 2388 RVA: 0x000090E4 File Offset: 0x000072E4
	private static LayerUtil Instance
	{
		get
		{
			if (LayerUtil.instance == null)
			{
				LayerUtil.instance = global::UnityEngine.Object.FindObjectOfType<LayerUtil>();
			}
			return LayerUtil.instance;
		}
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x00009102 File Offset: 0x00007302
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
