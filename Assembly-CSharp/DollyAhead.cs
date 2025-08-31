using System;
using Cinemachine;
using UnityEngine;

public class DollyAhead : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060004E7 RID: 1255 RVA: 0x0001A77C File Offset: 0x0001897C
	private void OnValidate()
	{
		if (this.cart == null)
		{
			this.cart = base.GetComponent<CinemachineDollyCart>();
		}
		if (this.cart != null)
		{
			this.cart.m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;
		}
		if (this.path == null && this.cart != null)
		{
			this.path = this.cart.m_Path;
		}
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x0001A7EA File Offset: 0x000189EA
	private void Awake()
	{
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x0001A7EC File Offset: 0x000189EC
	private void OnEnable()
	{
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x0001A7F9 File Offset: 0x000189F9
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0001A808 File Offset: 0x00018A08
	public void ManagedUpdate()
	{
		float num = this.path.FromPathNativeUnits(this.path.FindClosestPoint(Player.Position, 0, -1, 4), CinemachinePathBase.PositionUnits.Distance);
		this.cart.m_Position = num + this.distanceAhead;
	}

	public CinemachineDollyCart cart;

	public CinemachinePathBase path;

	public float distanceAhead = 20f;
}
