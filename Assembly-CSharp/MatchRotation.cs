using System;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
	// Token: 0x0600096B RID: 2411 RVA: 0x0003ACFC File Offset: 0x00038EFC
	private void OnEnable()
	{
		this.angle = this.target.rotation.eulerAngles.y;
		this.velocity = 0f;
		base.transform.rotation = Quaternion.Euler(0f, this.angle, 0f);
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0003AD54 File Offset: 0x00038F54
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
