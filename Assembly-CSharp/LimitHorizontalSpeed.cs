using System;
using UnityEngine;

// Token: 0x0200023F RID: 575
public class LimitHorizontalSpeed : MonoBehaviour
{
	// Token: 0x06000AD5 RID: 2773 RVA: 0x0003E100 File Offset: 0x0003C300
	public void FixedUpdate()
	{
		Vector3 vector = this.rigidbody.velocity.Flat();
		float magnitude = vector.magnitude;
		if (magnitude > this.maxSpeed)
		{
			float num = magnitude - this.maxSpeed;
			Vector3 vector2 = -1f * this.dragForce * num * vector;
			vector2 = Vector3.ClampMagnitude(vector2, this.maxForce);
			this.rigidbody.AddForce(vector2, 5);
		}
	}

	// Token: 0x04000DB7 RID: 3511
	public Rigidbody rigidbody;

	// Token: 0x04000DB8 RID: 3512
	public float maxSpeed = 0.5f;

	// Token: 0x04000DB9 RID: 3513
	public float dragForce = 20f;

	// Token: 0x04000DBA RID: 3514
	public float maxForce = 100f;
}
