using System;
using Cinemachine;
using UnityEngine;

// Token: 0x0200026B RID: 619
public class PlayerCameraCM : MonoBehaviour
{
	// Token: 0x06000BD4 RID: 3028 RVA: 0x0000B20B File Offset: 0x0000940B
	public void Awake()
	{
		this.cm = base.GetComponent<CinemachineFreeLook>();
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x00040B80 File Offset: 0x0003ED80
	private void Start()
	{
		this.rigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
		this.rigPositions = new Vector3[3];
		for (int i = 0; i < 3; i++)
		{
			this.rigPositions[i] = new Vector3(0f, this.cm.m_Orbits[i].m_Height, -this.cm.m_Orbits[i].m_Radius);
		}
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x00040C00 File Offset: 0x0003EE00
	private void LateUpdate()
	{
		this.yAxis = this.cm.m_YAxis.Value;
		if (Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.05f)
		{
			this.recenteringActive = false;
		}
		if (this.rigidbody.velocity.sqrMagnitude > 0.5f)
		{
			this.recenteringActive = true;
			this.velocityModifiedTarget = Mathf.MoveTowards(this.velocityModifiedTarget, this.defaultTarget - this.velocityImpact * this.rigidbody.velocity.normalized.y, this.velocitySpeed * Time.deltaTime);
		}
		else
		{
			this.velocityModifiedTarget = Mathf.MoveTowards(this.velocityModifiedTarget, this.defaultTarget, this.velocitySpeed * Time.deltaTime);
		}
		if (this.recenteringActive)
		{
			this.yAxis = Mathf.SmoothDamp(this.yAxis, this.velocityModifiedTarget, ref this.recenterVelocity, this.cm.m_YAxisRecentering.m_RecenteringTime);
			this.cm.m_RecenterToTargetHeading.m_enabled = true;
		}
		else
		{
			this.yAxis = this.cm.m_YAxis.Value;
			this.recenterVelocity = 0f;
			this.cm.m_RecenterToTargetHeading.m_enabled = false;
		}
		float num = 0f;
		float num2 = this.yAxis;
		Vector3 vector = this.rigidbody.transform.position + 0.75f * Vector3.up;
		Vector3 vector2 = this.YAxisToPosition(num2) - vector;
		Debug.DrawLine(vector, vector + vector2);
		while (num2 < 1f && Physics.BoxCast(vector, 0.1f * Vector3.one, vector2, Quaternion.identity, vector2.magnitude, this.raycastMask, 2))
		{
			num += this.raycastStep;
			num2 += this.raycastStep;
			vector2 = this.YAxisToPosition(num2) - vector;
			Debug.DrawLine(vector, vector + vector2);
		}
		if (num > 0f || this.raycastVelocity != 0f)
		{
			this.cm.m_YAxis.Value = Mathf.SmoothDamp(this.cm.m_YAxis.Value, this.yAxis + num, ref this.raycastVelocity, this.raycastAdjustTime);
			return;
		}
		this.cm.m_YAxis.Value = this.yAxis;
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x0000B219 File Offset: 0x00009419
	private float AngleToYAxis(float angle)
	{
		return angle;
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00040E70 File Offset: 0x0003F070
	private Vector3 YAxisToPosition(float yAxis)
	{
		yAxis = Mathf.Clamp01(yAxis);
		yAxis = 2f - 2f * yAxis;
		Vector3 vector;
		if (Mathf.Approximately(yAxis, (float)Mathf.FloorToInt(yAxis)))
		{
			vector = this.rigPositions[Mathf.FloorToInt(yAxis)];
		}
		else if (yAxis > 1f)
		{
			vector = Vector3.Slerp(this.rigPositions[1], this.rigPositions[2], yAxis - 1f);
		}
		else
		{
			vector = Vector3.Slerp(this.rigPositions[0], this.rigPositions[1], yAxis);
		}
		return this.rigidbody.transform.position + Quaternion.Euler(0f, this.cm.m_XAxis.Value, 0f) * vector;
	}

	// Token: 0x04000EE0 RID: 3808
	private CinemachineFreeLook cm;

	// Token: 0x04000EE1 RID: 3809
	public PlayerMovement movement;

	// Token: 0x04000EE2 RID: 3810
	public Rigidbody rigidbody;

	// Token: 0x04000EE3 RID: 3811
	public float defaultTarget = 0.5f;

	// Token: 0x04000EE4 RID: 3812
	public float velocitySpeed = 1f;

	// Token: 0x04000EE5 RID: 3813
	public float velocityImpact = 0.5f;

	// Token: 0x04000EE6 RID: 3814
	private float velocityModifiedTarget = 0.5f;

	// Token: 0x04000EE7 RID: 3815
	public LayerMask raycastMask;

	// Token: 0x04000EE8 RID: 3816
	public float raycastStep = 0.1f;

	// Token: 0x04000EE9 RID: 3817
	private float raycastVelocity;

	// Token: 0x04000EEA RID: 3818
	public float raycastAdjustTime = 0.2f;

	// Token: 0x04000EEB RID: 3819
	private bool recenteringActive;

	// Token: 0x04000EEC RID: 3820
	private float recenterVelocity;

	// Token: 0x04000EED RID: 3821
	private Vector3[] rigPositions;

	// Token: 0x04000EEE RID: 3822
	private float yAxis = 0.5f;
}
