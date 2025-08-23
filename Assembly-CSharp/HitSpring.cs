using System;
using UnityEngine;

public class HitSpring : MonoBehaviour, IHit
{
	// Token: 0x06000816 RID: 2070 RVA: 0x00007F5B File Offset: 0x0000615B
	private void Awake()
	{
		if (this.springTransform == null)
		{
			this.springTransform = base.transform;
		}
		this.initialRotation = this.springTransform.localRotation;
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00037040 File Offset: 0x00035240
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

	// Token: 0x06000818 RID: 2072 RVA: 0x000370B0 File Offset: 0x000352B0
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

	// Token: 0x06000819 RID: 2073 RVA: 0x00037108 File Offset: 0x00035308
	private void Start()
	{
		this.rotation = this.springTransform.localRotation.eulerAngles.y;
		base.enabled = false;
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x00007F88 File Offset: 0x00006188
	private void TrySqueakEffect()
	{
		if (this.squeakSound != null && Time.time - this.lastSqueakSoundTime > 0.25f)
		{
			this.lastSqueakSoundTime = Time.time;
			this.squeakSound.Play();
		}
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x0003713C File Offset: 0x0003533C
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

	// Token: 0x0600081C RID: 2076 RVA: 0x00007FC1 File Offset: 0x000061C1
	public void AddImpulse(Vector3 impulse)
	{
		impulse = this.springTransform.InverseTransformDirection(impulse);
		this.velocity.x = impulse.z;
		this.velocity.y = -impulse.x;
		base.enabled = true;
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x000371EC File Offset: 0x000353EC
	public void AddForce(Vector3 force)
	{
		force = this.springTransform.InverseTransformDirection(force);
		this.velocity.x = this.velocity.x + force.z * Time.deltaTime;
		this.velocity.y = this.velocity.y + -force.x * Time.deltaTime;
		base.enabled = true;
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00037248 File Offset: 0x00035448
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
