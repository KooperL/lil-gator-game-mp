using System;
using UnityEngine;

public class HitSpring : MonoBehaviour, IHit
{
	// Token: 0x0600069D RID: 1693 RVA: 0x00021A02 File Offset: 0x0001FC02
	private void Awake()
	{
		if (this.springTransform == null)
		{
			this.springTransform = base.transform;
		}
		this.initialRotation = this.springTransform.localRotation;
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x00021A30 File Offset: 0x0001FC30
	private void OnEnable()
	{
		if (this.surface == null)
		{
			this.surface = base.GetComponent<ISurface>();
		}
		if (this.audioSource == null)
		{
			this.audioSource = base.GetComponent<AudioSourceVariance>();
		}
		if (this.dynamicRenderer != null)
		{
			this.dynamicRenderer.enabled = true;
		}
		if (this.staticRenderer != null)
		{
			this.staticRenderer.enabled = false;
		}
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x00021AA0 File Offset: 0x0001FCA0
	private void OnDisable()
	{
		if (this.dynamicRenderer != null)
		{
			this.dynamicRenderer.enabled = false;
		}
		if (this.staticRenderer != null)
		{
			this.staticRenderer.enabled = true;
		}
		this.prevVelocity = (this.velocity = Vector2.zero);
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x00021AF8 File Offset: 0x0001FCF8
	private void Start()
	{
		this.rotation = this.springTransform.localRotation.eulerAngles.y;
		base.enabled = false;
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x00021B2A File Offset: 0x0001FD2A
	private void TrySqueakEffect()
	{
		if (this.squeakSound != null && Time.time - this.lastSqueakSoundTime > 0.25f)
		{
			this.lastSqueakSoundTime = Time.time;
			this.squeakSound.Play();
		}
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x00021B64 File Offset: 0x0001FD64
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		this.AddImpulse(100f * velocity);
		if (this.audioSource != null)
		{
			this.audioSource.Play();
		}
		if (this.surface != null)
		{
			SurfaceMaterial surfaceMaterial = this.surface.GetSurfaceMaterial(base.transform.position);
			if (surfaceMaterial != null)
			{
				surfaceMaterial.PlayImpact(base.transform.position, 0.5f, 1f);
			}
		}
		if (this.collider != null)
		{
			this.collider.enabled = false;
			this.colliderEnableTime = Time.time + this.colliderCooldown;
		}
		base.enabled = true;
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x00021C11 File Offset: 0x0001FE11
	public void AddImpulse(Vector3 impulse)
	{
		impulse = this.springTransform.InverseTransformDirection(impulse);
		this.velocity.x = impulse.z;
		this.velocity.y = -impulse.x;
		base.enabled = true;
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x00021C4C File Offset: 0x0001FE4C
	public void AddForce(Vector3 force)
	{
		force = this.springTransform.InverseTransformDirection(force);
		this.velocity.x = this.velocity.x + force.z * Time.deltaTime;
		this.velocity.y = this.velocity.y + -force.x * Time.deltaTime;
		base.enabled = true;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x00021CA8 File Offset: 0x0001FEA8
	private void Update()
	{
		if (this.collider != null && !this.collider.enabled && Time.time > this.colliderEnableTime)
		{
			this.collider.enabled = true;
		}
		this.velocity = Vector2.ClampMagnitude(this.velocity, 400f);
		if (this.position.magnitude > 45f)
		{
			this.position = Vector2.ClampMagnitude(this.position, 45f);
			this.velocity *= -0.5f;
		}
		this.velocity = Vector2.MoveTowards(this.velocity, Vector2.zero, Mathf.Max(this.velocity.magnitude, 50f) * 2f * Time.deltaTime);
		if (this.velocity.sqrMagnitude > 1f || this.position.sqrMagnitude > 1f)
		{
			Vector2 vector = this.position * -150f;
			this.velocity += vector * Time.deltaTime;
			this.position += this.velocity * Time.deltaTime;
		}
		else
		{
			this.position = Vector2.zero;
			this.velocity = Vector2.zero;
		}
		this.springTransform.localRotation = this.initialRotation * Quaternion.Euler(this.position.x, this.rotation, this.position.y);
		if (this.velocity.magnitude > 15f && this.prevVelocity.magnitude <= 15f)
		{
			this.TrySqueakEffect();
		}
		this.prevVelocity = this.velocity;
		if (this.velocity == Vector2.zero && this.collider != null)
		{
			this.collider.enabled = true;
			base.enabled = false;
		}
	}

	private Quaternion initialRotation;

	public Renderer dynamicRenderer;

	public Renderer staticRenderer;

	public Collider collider;

	public float colliderCooldown = 0.5f;

	private float colliderEnableTime = -1f;

	private ISurface surface;

	public Transform springTransform;

	private Vector2 position = Vector2.zero;

	private Vector2 velocity = Vector2.zero;

	private Vector2 prevVelocity;

	private const float springFactor = 150f;

	private const float drag = 2f;

	private const float hitSpeed = 100f;

	private const float maxAngle = 45f;

	public AudioSourceVariance audioSource;

	public AudioSourceVariance squeakSound;

	private float lastSqueakSoundTime = -1f;

	private float rotation;
}
