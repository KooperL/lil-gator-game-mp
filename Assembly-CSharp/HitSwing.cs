using System;
using UnityEngine;

// Token: 0x02000146 RID: 326
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

	// Token: 0x040008FA RID: 2298
	private const float minSpeed = 0f;

	// Token: 0x040008FB RID: 2299
	private const float maxSpeed = 360f;

	// Token: 0x040008FC RID: 2300
	private const float drag = 0.5f;

	// Token: 0x040008FD RID: 2301
	private const float actorMinSpeed = 0.1f;

	// Token: 0x040008FE RID: 2302
	private const float actorMaxSpeed = 3f;

	// Token: 0x040008FF RID: 2303
	private const float actorDrag = 0.5f;

	// Token: 0x04000900 RID: 2304
	private Vector3 initialPosition;

	// Token: 0x04000901 RID: 2305
	private Rigidbody rigidbody;

	// Token: 0x04000902 RID: 2306
	private float rotation;

	// Token: 0x04000903 RID: 2307
	private float speed;

	// Token: 0x04000904 RID: 2308
	public float swingLength = 2.5f;

	// Token: 0x04000905 RID: 2309
	public float speedFromHit = 5f;

	// Token: 0x04000906 RID: 2310
	public float trailSpeed = 4f;

	// Token: 0x04000907 RID: 2311
	public TrailRenderer trail1;

	// Token: 0x04000908 RID: 2312
	public TrailRenderer trail2;

	// Token: 0x04000909 RID: 2313
	private AudioSource audioSource;

	// Token: 0x0400090A RID: 2314
	public bool hasActor;

	// Token: 0x0400090B RID: 2315
	public bool hasPlayer;

	// Token: 0x0400090C RID: 2316
	private float variance = 1f;
}
