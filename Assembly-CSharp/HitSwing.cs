using System;
using UnityEngine;

public class HitSwing : MonoBehaviour, SpeedInterface, IHit
{
	// Token: 0x06000828 RID: 2088 RVA: 0x0000808C File Offset: 0x0000628C
	private void Awake()
	{
		this.variance = global::UnityEngine.Random.Range(0.5f, 1.5f);
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.initialPosition = this.rigidbody.position;
		this.audioSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x000080CC File Offset: 0x000062CC
	private float WorldToAngle(float arcLength)
	{
		return 2f * Mathf.Atan(arcLength / (2f * this.swingLength));
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x000080E7 File Offset: 0x000062E7
	private void Start()
	{
		this.rotation = 2f * (global::UnityEngine.Random.value - 0.5f);
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x00037080 File Offset: 0x00035280
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		if (this.rigidbody == null)
		{
			return;
		}
		float num = -Mathf.Sign(Vector3.Dot(this.rigidbody.rotation * Vector3.forward, velocity)) * this.speedFromHit;
		if (this.hasActor)
		{
			num *= 0.5f;
		}
		this.speed += num;
		if (this.audioSource != null)
		{
			this.audioSource.Play();
		}
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x000370FC File Offset: 0x000352FC
	public void AddForce(Vector3 force)
	{
		if (this.rigidbody == null)
		{
			return;
		}
		Mathf.Sign(Vector3.Dot(this.rigidbody.rotation * Vector3.forward, force));
		this.speed -= Time.deltaTime * Vector3.Dot(this.rigidbody.rotation * Vector3.forward, force);
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x00037168 File Offset: 0x00035368
	private void FixedUpdate()
	{
		float num = this.variance * (this.hasActor ? 0.1f : 0f);
		float num2 = this.variance * ((this.hasActor && !this.hasPlayer) ? 3f : 360f);
		float num3 = this.variance * (this.hasActor ? 0.5f : 0.5f);
		if (Mathf.Abs(this.speed) > num)
		{
			this.speed = Mathf.MoveTowards(this.speed, Mathf.Sign(this.speed) * num, num3 * Time.deltaTime);
		}
		this.AddForce(Physics.gravity);
		this.speed = Mathf.Clamp(this.speed, -num2, num2);
		this.trail1.emitting = (this.trail2.emitting = Mathf.Abs(this.speed) > this.trailSpeed);
		this.rigidbody.velocity = Vector3.zero;
		this.rigidbody.angularVelocity = 1.25f * base.transform.parent.TransformDirection(Vector3.right * this.WorldToAngle(this.speed));
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x00008100 File Offset: 0x00006300
	public float GetSpeed()
	{
		return this.speed;
	}

	private const float minSpeed = 0f;

	private const float maxSpeed = 360f;

	private const float drag = 0.5f;

	private const float actorMinSpeed = 0.1f;

	private const float actorMaxSpeed = 3f;

	private const float actorDrag = 0.5f;

	private Vector3 initialPosition;

	private Rigidbody rigidbody;

	private float rotation;

	private float speed;

	public float swingLength = 2.5f;

	public float speedFromHit = 5f;

	public float trailSpeed = 4f;

	public TrailRenderer trail1;

	public TrailRenderer trail2;

	private AudioSource audioSource;

	public bool hasActor;

	public bool hasPlayer;

	private float variance = 1f;
}
