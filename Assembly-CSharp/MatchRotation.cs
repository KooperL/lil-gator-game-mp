using System;
using UnityEngine;

// Token: 0x020001EF RID: 495
public class MatchRotation : MonoBehaviour
{
	// Token: 0x06000929 RID: 2345 RVA: 0x000390C4 File Offset: 0x000372C4
	private void OnEnable()
	{
		this.angle = this.target.rotation.eulerAngles.y;
		this.velocity = 0f;
		base.transform.rotation = Quaternion.Euler(0f, this.angle, 0f);
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x0003911C File Offset: 0x0003731C
	private void LateUpdate()
	{
		this.angle = MathUtils.SmoothDampAngle(this.angle, this.target.rotation.eulerAngles.y, ref this.velocity, this.smoothTime, 1000f, Time.deltaTime);
		base.transform.rotation = Quaternion.Euler(0f, this.angle, 0f);
	}

	// Token: 0x04000BD5 RID: 3029
	public Transform target;

	// Token: 0x04000BD6 RID: 3030
	public float smoothTime = 0.25f;

	// Token: 0x04000BD7 RID: 3031
	private float angle;

	// Token: 0x04000BD8 RID: 3032
	private float velocity;
}
