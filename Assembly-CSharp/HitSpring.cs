using System;
using UnityEngine;

// Token: 0x020001A7 RID: 423
public class HitSpring : MonoBehaviour, IHit
{
	// Token: 0x060007D5 RID: 2005 RVA: 0x00007C4C File Offset: 0x00005E4C
	private void Awake()
	{
		if (this.springTransform == null)
		{
			this.springTransform = base.transform;
		}
		this.initialRotation = this.springTransform.localRotation;
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x00035410 File Offset: 0x00033610
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

	// Token: 0x060007D7 RID: 2007 RVA: 0x00035480 File Offset: 0x00033680
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

	// Token: 0x060007D8 RID: 2008 RVA: 0x000354D8 File Offset: 0x000336D8
	private void Start()
	{
		this.rotation = this.springTransform.localRotation.eulerAngles.y;
		base.enabled = false;
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x00007C79 File Offset: 0x00005E79
	private void TrySqueakEffect()
	{
		if (this.squeakSound != null && Time.time - this.lastSqueakSoundTime > 0.25f)
		{
			this.lastSqueakSoundTime = Time.time;
			this.squeakSound.Play();
		}
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x0003550C File Offset: 0x0003370C
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

	// Token: 0x060007DB RID: 2011 RVA: 0x00007CB2 File Offset: 0x00005EB2
	public void AddImpulse(Vector3 impulse)
	{
		impulse = this.springTransform.InverseTransformDirection(impulse);
		this.velocity.x = impulse.z;
		this.velocity.y = -impulse.x;
		base.enabled = true;
	}

	// Token: 0x060007DC RID: 2012 RVA: 0x000355BC File Offset: 0x000337BC
	public void AddForce(Vector3 force)
	{
		force = this.springTransform.InverseTransformDirection(force);
		this.velocity.x = this.velocity.x + force.z * Time.deltaTime;
		this.velocity.y = this.velocity.y + -force.x * Time.deltaTime;
		base.enabled = true;
	}

	// Token: 0x060007DD RID: 2013 RVA: 0x00035618 File Offset: 0x00033818
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

	// Token: 0x04000A68 RID: 2664
	private Quaternion initialRotation;

	// Token: 0x04000A69 RID: 2665
	public Renderer dynamicRenderer;

	// Token: 0x04000A6A RID: 2666
	public Renderer staticRenderer;

	// Token: 0x04000A6B RID: 2667
	public Collider collider;

	// Token: 0x04000A6C RID: 2668
	public float colliderCooldown = 0.5f;

	// Token: 0x04000A6D RID: 2669
	private float colliderEnableTime = -1f;

	// Token: 0x04000A6E RID: 2670
	private ISurface surface;

	// Token: 0x04000A6F RID: 2671
	public Transform springTransform;

	// Token: 0x04000A70 RID: 2672
	private Vector2 position = Vector2.zero;

	// Token: 0x04000A71 RID: 2673
	private Vector2 velocity = Vector2.zero;

	// Token: 0x04000A72 RID: 2674
	private Vector2 prevVelocity;

	// Token: 0x04000A73 RID: 2675
	private const float springFactor = 150f;

	// Token: 0x04000A74 RID: 2676
	private const float drag = 2f;

	// Token: 0x04000A75 RID: 2677
	private const float hitSpeed = 100f;

	// Token: 0x04000A76 RID: 2678
	private const float maxAngle = 45f;

	// Token: 0x04000A77 RID: 2679
	public AudioSourceVariance audioSource;

	// Token: 0x04000A78 RID: 2680
	public AudioSourceVariance squeakSound;

	// Token: 0x04000A79 RID: 2681
	private float lastSqueakSoundTime = -1f;

	// Token: 0x04000A7A RID: 2682
	private float rotation;
}
