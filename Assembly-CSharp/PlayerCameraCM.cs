using System;
using Cinemachine;
using UnityEngine;

public class PlayerCameraCM : MonoBehaviour
{
	// Token: 0x06000A1F RID: 2591 RVA: 0x0002F0BC File Offset: 0x0002D2BC
	public void Awake()
	{
		this.cm = base.GetComponent<CinemachineFreeLook>();
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x0002F0CC File Offset: 0x0002D2CC
	private void Start()
	{
		this.rigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
		this.rigPositions = new Vector3[3];
		for (int i = 0; i < 3; i++)
		{
			this.rigPositions[i] = new Vector3(0f, this.cm.m_Orbits[i].m_Height, -this.cm.m_Orbits[i].m_Radius);
		}
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x0002F14C File Offset: 0x0002D34C
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
		while (num2 < 1f && Physics.BoxCast(vector, 0.1f * Vector3.one, vector2, Quaternion.identity, vector2.magnitude, this.raycastMask, QueryTriggerInteraction.Collide))
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

	// Token: 0x06000A22 RID: 2594 RVA: 0x0002F3BA File Offset: 0x0002D5BA
	private float AngleToYAxis(float angle)
	{
		return angle;
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x0002F3C0 File Offset: 0x0002D5C0
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

	private CinemachineFreeLook cm;

	public PlayerMovement movement;

	public Rigidbody rigidbody;

	public float defaultTarget = 0.5f;

	public float velocitySpeed = 1f;

	public float velocityImpact = 0.5f;

	private float velocityModifiedTarget = 0.5f;

	public LayerMask raycastMask;

	public float raycastStep = 0.1f;

	private float raycastVelocity;

	public float raycastAdjustTime = 0.2f;

	private bool recenteringActive;

	private float recenterVelocity;

	private Vector3[] rigPositions;

	private float yAxis = 0.5f;
}
