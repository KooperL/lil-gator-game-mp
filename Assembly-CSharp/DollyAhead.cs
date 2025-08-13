using System;
using Cinemachine;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class DollyAhead : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060005F3 RID: 1523 RVA: 0x0002F2E0 File Offset: 0x0002D4E0
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

	// Token: 0x060005F4 RID: 1524 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x0000265D File Offset: 0x0000085D
	private void OnEnable()
	{
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0000266A File Offset: 0x0000086A
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0002F350 File Offset: 0x0002D550
	public void ManagedUpdate()
	{
		float num = this.path.FromPathNativeUnits(this.path.FindClosestPoint(Player.Position, 0, -1, 4), 1);
		this.cart.m_Position = num + this.distanceAhead;
	}

	// Token: 0x040007FA RID: 2042
	public CinemachineDollyCart cart;

	// Token: 0x040007FB RID: 2043
	public CinemachinePathBase path;

	// Token: 0x040007FC RID: 2044
	public float distanceAhead = 20f;
}
