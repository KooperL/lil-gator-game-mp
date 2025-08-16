using System;
using UnityEngine;

public class HeadController : MonoBehaviour
{
	// Token: 0x0600079F RID: 1951 RVA: 0x00007985 File Offset: 0x00005B85
	private void Start()
	{
		this.forward = this.neck.forward;
		this.velocityForward = this.forward;
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x00035600 File Offset: 0x00033800
	private void FixedUpdate()
	{
		float magnitude = this.rigidbody.velocity.magnitude;
		if (magnitude > 0f)
		{
			this.velocityForward = Vector3.RotateTowards(this.body.forward, this.rigidbody.velocity / magnitude, 2f * magnitude, 0f);
		}
		else
		{
			this.velocityForward = this.body.forward;
		}
		if (this.velocityForward != this.forward)
		{
			this.forward = Vector3.Slerp(this.forward, this.velocityForward, this.rotationSpeed * Time.fixedDeltaTime);
		}
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x000079A4 File Offset: 0x00005BA4
	private void LateUpdate()
	{
		this.neck.forward = this.forward;
	}

	public Transform body;

	public Transform neck;

	public Transform head;

	public Rigidbody rigidbody;

	public float rotationSpeed = 2f;

	private Vector3 velocityForward;

	private Vector3 forward;
}
