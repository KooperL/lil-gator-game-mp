using System;
using UnityEngine;

public class HeadController : MonoBehaviour
{
	// Token: 0x0600063A RID: 1594 RVA: 0x00020457 File Offset: 0x0001E657
	private void Start()
	{
		this.forward = this.neck.forward;
		this.velocityForward = this.forward;
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x00020478 File Offset: 0x0001E678
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

	// Token: 0x0600063C RID: 1596 RVA: 0x0002051D File Offset: 0x0001E71D
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
