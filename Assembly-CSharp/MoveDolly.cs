using System;
using Cinemachine;
using UnityEngine;

public class MoveDolly : MonoBehaviour
{
	// Token: 0x06000602 RID: 1538 RVA: 0x00006490 File Offset: 0x00004690
	private void OnEnable()
	{
		if (this.virtualCamera != null)
		{
			this.trackedDolly = this.virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
		}
		this.dollyCart.m_PositionUnits = CinemachinePathBase.PositionUnits.PathUnits;
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x00030370 File Offset: 0x0002E570
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

	// Token: 0x06000604 RID: 1540 RVA: 0x000303C0 File Offset: 0x0002E5C0
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

	// Token: 0x06000605 RID: 1541 RVA: 0x000064BD File Offset: 0x000046BD
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
