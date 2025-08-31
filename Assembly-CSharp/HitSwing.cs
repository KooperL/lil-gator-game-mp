using System;
using UnityEngine;

public class HitSwing : MonoBehaviour, SpeedInterface, IHit
{
	// Token: 0x060006AA RID: 1706 RVA: 0x00021F2D File Offset: 0x0002012D
	private void Awake()
	{
		this.variance = Random.Range(0.5f, 1.5f);
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.initialPosition = this.rigidbody.position;
		this.audioSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x00021F6D File Offset: 0x0002016D
	private float WorldToAngle(float arcLength)
	{
		return 2f * Mathf.Atan(arcLength / (2f * this.swingLength));
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x00021F88 File Offset: 0x00020188
	private void Start()
	{
		this.rotation = 2f * (Random.value - 0.5f);
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x00021FA4 File Offset: 0x000201A4
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

	// Token: 0x060006AE RID: 1710 RVA: 0x00022020 File Offset: 0x00020220
	public void AddForce(Vector3 force)
	{
		if (this.rigidbody == null)
		{
			return;
		}
		Mathf.Sign(Vector3.Dot(this.rigidbody.rotation * Vector3.forward, force));
		this.speed -= Time.deltaTime * Vector3.Dot(this.rigidbody.rotation * Vector3.forward, force);
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0002208C File Offset: 0x0002028C
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

	// Token: 0x060006B0 RID: 1712 RVA: 0x000221BE File Offset: 0x000203BE
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
