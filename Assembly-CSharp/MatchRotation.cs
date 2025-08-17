using System;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
	// Token: 0x0600096A RID: 2410 RVA: 0x0003AA34 File Offset: 0x00038C34
	private void OnEnable()
	{
		this.angle = this.target.rotation.eulerAngles.y;
		this.velocity = 0f;
		base.transform.rotation = Quaternion.Euler(0f, this.angle, 0f);
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x0003AA8C File Offset: 0x00038C8C
	private void LateUpdate()
	{
		this.angle = MathUtils.SmoothDampAngle(this.angle, this.target.rotation.eulerAngles.y, ref this.velocity, this.smoothTime, 1000f, Time.deltaTime);
		base.transform.rotation = Quaternion.Euler(0f, this.angle, 0f);
	}

	public Transform target;

	public float smoothTime = 0.25f;

	private float angle;

	private float velocity;
}
