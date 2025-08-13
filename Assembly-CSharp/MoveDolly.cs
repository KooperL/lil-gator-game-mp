using System;
using Cinemachine;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class MoveDolly : MonoBehaviour
{
	// Token: 0x060005C8 RID: 1480 RVA: 0x000061CA File Offset: 0x000043CA
	private void OnEnable()
	{
		if (this.virtualCamera != null)
		{
			this.trackedDolly = this.virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
		}
		this.dollyCart.m_PositionUnits = 0;
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x0002EC98 File Offset: 0x0002CE98
	private void OnValidate()
	{
		if (this.virtualCamera != null)
		{
			this.virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = this.position;
		}
		if (this.dollyCart != null)
		{
			this.dollyCart.m_Position = this.position;
		}
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x0002ECE8 File Offset: 0x0002CEE8
	private void Update()
	{
		this.position = Mathf.SmoothDamp(this.position, this.targetPosition, ref this.velocity, this.smoothTime);
		this.velocity = Mathf.Min(this.velocity, this.maxSpeed);
		if (this.trackedDolly != null)
		{
			this.trackedDolly.m_PathPosition = this.position;
		}
		if (this.dollyCart != null)
		{
			this.dollyCart.m_Position = this.position;
		}
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x000061F7 File Offset: 0x000043F7
	public void SetTargetPosition(float newTargetPosition)
	{
		this.targetPosition = newTargetPosition;
	}

	// Token: 0x040007D3 RID: 2003
	public CinemachineVirtualCamera virtualCamera;

	// Token: 0x040007D4 RID: 2004
	private CinemachineTrackedDolly trackedDolly;

	// Token: 0x040007D5 RID: 2005
	public CinemachineDollyCart dollyCart;

	// Token: 0x040007D6 RID: 2006
	public float smoothTime = 1f;

	// Token: 0x040007D7 RID: 2007
	public float maxSpeed = 0.5f;

	// Token: 0x040007D8 RID: 2008
	public float position;

	// Token: 0x040007D9 RID: 2009
	[ReadOnly]
	public float velocity;

	// Token: 0x040007DA RID: 2010
	public float targetPosition;
}
