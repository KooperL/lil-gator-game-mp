using System;
using UnityEngine;

public class Bird_Flying : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00017530 File Offset: 0x00015730
	private void Start()
	{
		this.flyDirection += this.directionVariance * global::UnityEngine.Random.insideUnitSphere;
		this.flyDirection = base.transform.TransformDirection(this.flyDirection);
		this.initialSpeed *= global::UnityEngine.Random.Range(1f - this.speedVariance, 1f + this.speedVariance);
		this.acceleration *= global::UnityEngine.Random.Range(1f - this.speedVariance, 1f + this.speedVariance);
		this.speed = this.initialSpeed;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000175D8 File Offset: 0x000157D8
	public void Update()
	{
		this.speed = Mathf.MoveTowards(this.speed, this.maxSpeed, Time.deltaTime * this.acceleration);
		base.transform.position += Time.deltaTime * this.speed * this.flyDirection;
	}

	public float initialSpeed = 2f;

	public float maxSpeed = 10f;

	public float acceleration = 5f;

	private float speed;

	public Vector3 flyDirection;

	public float directionVariance = 0.25f;

	public float speedVariance = 0.5f;
}
