using System;
using UnityEngine;

// Token: 0x02000188 RID: 392
public class HeadController : MonoBehaviour
{
	// Token: 0x0600075F RID: 1887 RVA: 0x0000768B File Offset: 0x0000588B
	private void Start()
	{
		this.forward = this.neck.forward;
		this.velocityForward = this.forward;
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x00033E78 File Offset: 0x00032078
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

	// Token: 0x06000761 RID: 1889 RVA: 0x000076AA File Offset: 0x000058AA
	private void LateUpdate()
	{
		this.neck.forward = this.forward;
	}

	// Token: 0x040009CF RID: 2511
	public Transform body;

	// Token: 0x040009D0 RID: 2512
	public Transform neck;

	// Token: 0x040009D1 RID: 2513
	public Transform head;

	// Token: 0x040009D2 RID: 2514
	public Rigidbody rigidbody;

	// Token: 0x040009D3 RID: 2515
	public float rotationSpeed = 2f;

	// Token: 0x040009D4 RID: 2516
	private Vector3 velocityForward;

	// Token: 0x040009D5 RID: 2517
	private Vector3 forward;
}
