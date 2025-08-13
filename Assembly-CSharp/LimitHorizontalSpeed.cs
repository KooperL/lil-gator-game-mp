using System;
using UnityEngine;

// Token: 0x020001BE RID: 446
public class LimitHorizontalSpeed : MonoBehaviour
{
	// Token: 0x0600093E RID: 2366 RVA: 0x0002BE98 File Offset: 0x0002A098
	public void FixedUpdate()
	{
		Vector3 vector = this.rigidbody.velocity.Flat();
		float magnitude = vector.magnitude;
		if (magnitude > this.maxSpeed)
		{
			float num = magnitude - this.maxSpeed;
			Vector3 vector2 = -1f * this.dragForce * num * vector;
			vector2 = Vector3.ClampMagnitude(vector2, this.maxForce);
			this.rigidbody.AddForce(vector2, ForceMode.Acceleration);
		}
	}

	// Token: 0x04000B9B RID: 2971
	public Rigidbody rigidbody;

	// Token: 0x04000B9C RID: 2972
	public float maxSpeed = 0.5f;

	// Token: 0x04000B9D RID: 2973
	public float dragForce = 20f;

	// Token: 0x04000B9E RID: 2974
	public float maxForce = 100f;
}
