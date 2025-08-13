using System;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class SmearObject : MonoBehaviour
{
	// Token: 0x0600065B RID: 1627 RVA: 0x00030774 File Offset: 0x0002E974
	private void Start()
	{
		this.initialScale = base.transform.localScale;
		this.initialRotation = base.transform.localRotation;
		this.planeNormal = base.transform.localRotation * Vector3.forward;
		this.lastPosition = base.transform.position;
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x000307D0 File Offset: 0x0002E9D0
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

	// Token: 0x04000882 RID: 2178
	private Vector3 lastPosition;

	// Token: 0x04000883 RID: 2179
	private Vector3 initialScale;

	// Token: 0x04000884 RID: 2180
	private Quaternion initialRotation;

	// Token: 0x04000885 RID: 2181
	public Transform childObject;

	// Token: 0x04000886 RID: 2182
	private Vector3 planeNormal;

	// Token: 0x04000887 RID: 2183
	public Vector3 velocity;

	// Token: 0x04000888 RID: 2184
	private Vector3 velocityVelocity;

	// Token: 0x04000889 RID: 2185
	public float referenceFramerate = 30f;

	// Token: 0x0400088A RID: 2186
	public Vector3 localVelocity;
}
