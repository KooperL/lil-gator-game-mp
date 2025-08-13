using System;
using UnityEngine;

// Token: 0x020001AA RID: 426
public class HitSwing : MonoBehaviour, SpeedInterface, IHit
{
	// Token: 0x060007E8 RID: 2024 RVA: 0x00007D92 File Offset: 0x00005F92
	private void Awake()
	{
		this.variance = Random.Range(0.5f, 1.5f);
		this.rigidbody = base.GetComponent<Rigidbody>();
		this.initialPosition = this.rigidbody.position;
		this.audioSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x00007DD2 File Offset: 0x00005FD2
	private float WorldToAngle(float arcLength)
	{
		return 2f * Mathf.Atan(arcLength / (2f * this.swingLength));
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x00007DED File Offset: 0x00005FED
	private void Start()
	{
		this.rotation = 2f * (Random.value - 0.5f);
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x000358F8 File Offset: 0x00033AF8
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

	// Token: 0x060007EC RID: 2028 RVA: 0x00035974 File Offset: 0x00033B74
	public void AddForce(Vector3 force)
	{
		if (this.rigidbody == null)
		{
			return;
		}
		Mathf.Sign(Vector3.Dot(this.rigidbody.rotation * Vector3.forward, force));
		this.speed -= Time.deltaTime * Vector3.Dot(this.rigidbody.rotation * Vector3.forward, force);
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x000359E0 File Offset: 0x00033BE0
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

	// Token: 0x060007EE RID: 2030 RVA: 0x00007E06 File Offset: 0x00006006
	public float GetSpeed()
	{
		return this.speed;
	}

	// Token: 0x04000A82 RID: 2690
	private const float minSpeed = 0f;

	// Token: 0x04000A83 RID: 2691
	private const float maxSpeed = 360f;

	// Token: 0x04000A84 RID: 2692
	private const float drag = 0.5f;

	// Token: 0x04000A85 RID: 2693
	private const float actorMinSpeed = 0.1f;

	// Token: 0x04000A86 RID: 2694
	private const float actorMaxSpeed = 3f;

	// Token: 0x04000A87 RID: 2695
	private const float actorDrag = 0.5f;

	// Token: 0x04000A88 RID: 2696
	private Vector3 initialPosition;

	// Token: 0x04000A89 RID: 2697
	private Rigidbody rigidbody;

	// Token: 0x04000A8A RID: 2698
	private float rotation;

	// Token: 0x04000A8B RID: 2699
	private float speed;

	// Token: 0x04000A8C RID: 2700
	public float swingLength = 2.5f;

	// Token: 0x04000A8D RID: 2701
	public float speedFromHit = 5f;

	// Token: 0x04000A8E RID: 2702
	public float trailSpeed = 4f;

	// Token: 0x04000A8F RID: 2703
	public TrailRenderer trail1;

	// Token: 0x04000A90 RID: 2704
	public TrailRenderer trail2;

	// Token: 0x04000A91 RID: 2705
	private AudioSource audioSource;

	// Token: 0x04000A92 RID: 2706
	public bool hasActor;

	// Token: 0x04000A93 RID: 2707
	public bool hasPlayer;

	// Token: 0x04000A94 RID: 2708
	private float variance = 1f;
}
