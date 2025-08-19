using System;
using UnityEngine;

public class LimitHorizontalSpeed : MonoBehaviour
{
	// Token: 0x06000B21 RID: 2849 RVA: 0x0003FBE4 File Offset: 0x0003DDE4
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

	public Rigidbody rigidbody;

	public float maxSpeed = 0.5f;

	public float dragForce = 20f;

	public float maxForce = 100f;
}
