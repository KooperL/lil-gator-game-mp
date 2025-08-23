using System;
using UnityEngine;

public class SmearObject : MonoBehaviour
{
	// Token: 0x06000695 RID: 1685 RVA: 0x00031E84 File Offset: 0x00030084
	private void Start()
	{
		this.initialScale = base.transform.localScale;
		this.initialRotation = base.transform.localRotation;
		this.planeNormal = base.transform.localRotation * Vector3.forward;
		this.lastPosition = base.transform.position;
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x00031EE0 File Offset: 0x000300E0
	private void LateUpdate()
	{
		if (Time.deltaTime == 0f)
		{
			return;
		}
		base.transform.localRotation = this.initialRotation;
		Vector3 position = base.transform.position;
		this.velocity = Vector3.SmoothDamp(this.velocity, (position - this.lastPosition) / Time.deltaTime, ref this.velocityVelocity, 0.05f);
		this.localVelocity = base.transform.InverseTransformDirection(this.velocity);
		this.localVelocity.z = 0f;
		Vector3 vector = this.localVelocity / this.referenceFramerate;
		float magnitude = vector.magnitude;
		if (magnitude > 0f)
		{
			Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, vector);
			base.transform.localRotation = this.initialRotation * quaternion;
			base.transform.localScale = new Vector3(1f - magnitude, 1f + magnitude, 1f);
			this.childObject.localPosition = 0.25f * magnitude * Vector3.down;
			this.childObject.localRotation = Quaternion.Inverse(quaternion);
		}
		else
		{
			base.transform.rotation = this.initialRotation;
			this.childObject.transform.rotation = Quaternion.identity;
			this.childObject.transform.localPosition = Vector3.zero;
		}
		this.lastPosition = position;
	}

	private Vector3 lastPosition;

	private Vector3 initialScale;

	private Quaternion initialRotation;

	public Transform childObject;

	private Vector3 planeNormal;

	public Vector3 velocity;

	private Vector3 velocityVelocity;

	public float referenceFramerate = 30f;

	public Vector3 localVelocity;
}
