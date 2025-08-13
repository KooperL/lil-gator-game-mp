using System;
using Cinemachine;
using UnityEngine;

// Token: 0x020000E5 RID: 229
public class MoveDolly : MonoBehaviour
{
	// Token: 0x060004BC RID: 1212 RVA: 0x00019F03 File Offset: 0x00018103
	private void OnEnable()
	{
		if (this.virtualCamera != null)
		{
			this.trackedDolly = this.virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
		}
		this.dollyCart.m_PositionUnits = CinemachinePathBase.PositionUnits.PathUnits;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00019F30 File Offset: 0x00018130
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

	// Token: 0x060004BE RID: 1214 RVA: 0x00019F80 File Offset: 0x00018180
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

	// Token: 0x060004BF RID: 1215 RVA: 0x0001A005 File Offset: 0x00018205
	public void SetTargetPosition(float newTargetPosition)
	{
		this.targetPosition = newTargetPosition;
	}

	// Token: 0x04000691 RID: 1681
	public CinemachineVirtualCamera virtualCamera;

	// Token: 0x04000692 RID: 1682
	private CinemachineTrackedDolly trackedDolly;

	// Token: 0x04000693 RID: 1683
	public CinemachineDollyCart dollyCart;

	// Token: 0x04000694 RID: 1684
	public float smoothTime = 1f;

	// Token: 0x04000695 RID: 1685
	public float maxSpeed = 0.5f;

	// Token: 0x04000696 RID: 1686
	public float position;

	// Token: 0x04000697 RID: 1687
	[ReadOnly]
	public float velocity;

	// Token: 0x04000698 RID: 1688
	public float targetPosition;
}
