using System;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
	// Token: 0x060007C7 RID: 1991 RVA: 0x00025F48 File Offset: 0x00024148
	private void OnEnable()
	{
		this.angle = this.target.rotation.eulerAngles.y;
		this.velocity = 0f;
		base.transform.rotation = Quaternion.Euler(0f, this.angle, 0f);
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x00025FA0 File Offset: 0x000241A0
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
