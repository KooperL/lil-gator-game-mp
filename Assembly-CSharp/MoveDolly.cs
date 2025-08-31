using System;
using Cinemachine;
using UnityEngine;

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

	public CinemachineVirtualCamera virtualCamera;

	private CinemachineTrackedDolly trackedDolly;

	public CinemachineDollyCart dollyCart;

	public float smoothTime = 1f;

	public float maxSpeed = 0.5f;

	public float position;

	[ReadOnly]
	public float velocity;

	public float targetPosition;
}
