using System;
using UnityEngine;

public class TriggerPickup : MonoBehaviour, IOnTimeout
{
	// Token: 0x06000568 RID: 1384 RVA: 0x0001C92F File Offset: 0x0001AB2F
	public static void TriggerQuickPickup()
	{
		TriggerPickup.quickPickupTime = Time.time;
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0001C93C File Offset: 0x0001AB3C
	private void OnValidate()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponentInParent<Rigidbody>();
		}
		if (base.transform.parent != null && this.collider == null)
		{
			this.collider = base.transform.parent.GetComponent<Collider>();
		}
		this.sphereTrigger = base.GetComponent<SphereCollider>();
		if (this.audioSourceVariance == null)
		{
			this.audioSourceVariance = base.GetComponent<AudioSourceVariance>();
		}
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0001C9C0 File Offset: 0x0001ABC0
	private void Start()
	{
		if (!this.initialized)
		{
			this.Initialize();
		}
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x0001C9D0 File Offset: 0x0001ABD0
	public void Initialize()
	{
		this.initialized = true;
		this.initialPosition = base.transform.position;
		this.delayTime = Time.time + this.delay + Random.Range(0f, 0.25f);
		this.autoTime = Time.time + Random.Range(3f, 4f);
		this.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		this.sphereTrigger.radius *= 1f / base.transform.parent.parent.localScale.x;
		if (!this.allowPlayerCollision)
		{
			this.rigidbody.gameObject.layer = 23;
		}
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0001CA8C File Offset: 0x0001AC8C
	private void FixedUpdate()
	{
		if (!this.isQuick && Time.time - TriggerPickup.quickPickupTime < 0.1f)
		{
			this.delayTime -= 0.5f * this.delay;
			this.autoTime = this.delayTime;
			this.isQuick = true;
		}
		if (Time.time < this.delayTime)
		{
			return;
		}
		Vector3 position = this.rigidbody.position;
		Vector3 position2 = Player.Position;
		Vector3 vector = position2 - position;
		float magnitude = vector.magnitude;
		if (position.y < -20f || magnitude > 17f)
		{
			this.GiveReward();
			Object.Destroy(this.rigidbody.gameObject);
		}
		if (position.y - Mathf.Min(this.initialPosition.y, position2.y) < -5f || Time.time > this.autoTime)
		{
			this.StartPickUp();
		}
		if (!this.pickingUp)
		{
			return;
		}
		if (magnitude < 0.5f)
		{
			this.GiveReward();
			Object.Destroy(this.rigidbody.gameObject);
			return;
		}
		this.rigidbody.AddForce((this.isQuick ? 100f : 50f) / magnitude * vector, ForceMode.Acceleration);
		this.rigidbody.angularVelocity += 20f * Vector3.one * Time.deltaTime;
		this.rigidbody.transform.localScale = Vector3.MoveTowards(this.rigidbody.transform.localScale, 0.5f * Vector3.one, 2f * Time.deltaTime);
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0001CC2F File Offset: 0x0001AE2F
	private void OnTriggerStay(Collider other)
	{
		if (base.enabled && !this.pickingUp && Time.time > this.delayTime)
		{
			this.StartPickUp();
		}
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x0001CC54 File Offset: 0x0001AE54
	public void OnTimeout()
	{
		this.resource.SetAmountSecret(this.resource.Amount + this.resourceValue);
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0001CC74 File Offset: 0x0001AE74
	private void StartPickUp()
	{
		if (this.waterPhysics != null)
		{
			Object.Destroy(this.waterPhysics.gameObject);
		}
		this.rigidbody.drag = 3f;
		this.rigidbody.useGravity = false;
		this.pickingUp = true;
		if (this.collider != null)
		{
			this.collider.enabled = false;
		}
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0001CCDC File Offset: 0x0001AEDC
	private void GiveReward()
	{
		if (this.pickingUp && this.audioSourceVariance != null)
		{
			this.audioSourceVariance.Play();
		}
		if (this.resource != null)
		{
			this.resource.Amount += this.resourceValue;
		}
	}

	public static float quickPickupTime = -1f;

	public float delay = 0.5f;

	private float delayTime = -1f;

	private const float minAuto = 3f;

	private const float maxAuto = 4f;

	private float autoTime;

	public const float maxDistance = 17f;

	private bool pickingUp;

	private Vector3 initialPosition;

	public Rigidbody rigidbody;

	public Collider collider;

	public SphereCollider sphereTrigger;

	public float pickupAcc = 30f;

	public float pickupDrag = 0.7f;

	public AudioSourceVariance audioSourceVariance;

	public ItemResource resource;

	public int resourceValue = 1;

	public WaterPhysics waterPhysics;

	public bool allowPlayerCollision;

	private bool initialized;

	private bool isQuick;
}
