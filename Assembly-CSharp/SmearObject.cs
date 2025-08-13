using System;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class SmearObject : MonoBehaviour
{
	// Token: 0x06000549 RID: 1353 RVA: 0x0001C16C File Offset: 0x0001A36C
	private void Start()
	{
		this.initialScale = base.transform.localScale;
		this.initialRotation = base.transform.localRotation;
		this.planeNormal = base.transform.localRotation * Vector3.forward;
		this.lastPosition = base.transform.position;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x0001C1C8 File Offset: 0x0001A3C8
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

	// Token: 0x0400073B RID: 1851
	private Vector3 lastPosition;

	// Token: 0x0400073C RID: 1852
	private Vector3 initialScale;

	// Token: 0x0400073D RID: 1853
	private Quaternion initialRotation;

	// Token: 0x0400073E RID: 1854
	public Transform childObject;

	// Token: 0x0400073F RID: 1855
	private Vector3 planeNormal;

	// Token: 0x04000740 RID: 1856
	public Vector3 velocity;

	// Token: 0x04000741 RID: 1857
	private Vector3 velocityVelocity;

	// Token: 0x04000742 RID: 1858
	public float referenceFramerate = 30f;

	// Token: 0x04000743 RID: 1859
	public Vector3 localVelocity;
}
