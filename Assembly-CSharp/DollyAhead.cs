using System;
using Cinemachine;
using UnityEngine;

public class DollyAhead : MonoBehaviour, IManagedUpdate
{
	// Token: 0x0600062D RID: 1581 RVA: 0x000309DC File Offset: 0x0002EBDC
	private void OnValidate()
	{
		if (this.cart == null)
		{
			this.cart = base.GetComponent<CinemachineDollyCart>();
		}
		if (this.cart != null)
		{
			this.cart.m_PositionUnits = 1;
		}
		if (this.path == null && this.cart != null)
		{
			this.path = this.cart.m_Path;
		}
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x000026C1 File Offset: 0x000008C1
	private void OnEnable()
	{
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x000026CE File Offset: 0x000008CE
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x00030A4C File Offset: 0x0002EC4C
	public void ManagedUpdate()
	{
		float num = this.path.FromPathNativeUnits(this.path.FindClosestPoint(Player.Position, 0, -1, 4), 1);
		this.cart.m_Position = num + this.distanceAhead;
	}

	public CinemachineDollyCart cart;

	public CinemachinePathBase path;

	public float distanceAhead = 20f;
}
