using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class Bird_Flying : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
		this.flyDirection += this.directionVariance * Random.insideUnitSphere;
		this.flyDirection = base.transform.TransformDirection(this.flyDirection);
		this.initialSpeed *= Random.Range(1f - this.speedVariance, 1f + this.speedVariance);
		this.acceleration *= Random.Range(1f - this.speedVariance, 1f + this.speedVariance);
		this.speed = this.initialSpeed;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
	public void Update()
	{
		this.speed = Mathf.MoveTowards(this.speed, this.maxSpeed, Time.deltaTime * this.acceleration);
		base.transform.position += Time.deltaTime * this.speed * this.flyDirection;
	}

	// Token: 0x04000001 RID: 1
	public float initialSpeed = 2f;

	// Token: 0x04000002 RID: 2
	public float maxSpeed = 10f;

	// Token: 0x04000003 RID: 3
	public float acceleration = 5f;

	// Token: 0x04000004 RID: 4
	private float speed;

	// Token: 0x04000005 RID: 5
	public Vector3 flyDirection;

	// Token: 0x04000006 RID: 6
	public float directionVariance = 0.25f;

	// Token: 0x04000007 RID: 7
	public float speedVariance = 0.5f;
}
