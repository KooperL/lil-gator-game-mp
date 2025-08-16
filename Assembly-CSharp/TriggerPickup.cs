using System;
using UnityEngine;

public class TriggerPickup : MonoBehaviour, IOnTimeout
{
	// Token: 0x060006BA RID: 1722 RVA: 0x00006DE4 File Offset: 0x00004FE4
	public static void TriggerQuickPickup()
	{
		TriggerPickup.quickPickupTime = Time.time;
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x00032544 File Offset: 0x00030744
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

	// Token: 0x060006BC RID: 1724 RVA: 0x00006DF0 File Offset: 0x00004FF0
	private void Start()
	{
		if (!this.initialized)
		{
			this.Initialize();
		}
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x000325C8 File Offset: 0x000307C8
	public void Initialize()
	{
		this.initialized = true;
		this.initialPosition = base.transform.position;
		this.delayTime = Time.time + this.delay + global::UnityEngine.Random.Range(0f, 0.25f);
		this.autoTime = Time.time + global::UnityEngine.Random.Range(3f, 4f);
		this.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		this.sphereTrigger.radius *= 1f / base.transform.parent.parent.localScale.x;
		if (!this.allowPlayerCollision)
		{
			this.rigidbody.gameObject.layer = 23;
		}
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x00032684 File Offset: 0x00030884
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
			global::UnityEngine.Object.Destroy(this.rigidbody.gameObject);
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
			global::UnityEngine.Object.Destroy(this.rigidbody.gameObject);
			return;
		}
		this.rigidbody.AddForce((this.isQuick ? 100f : 50f) / magnitude * vector, ForceMode.Acceleration);
		this.rigidbody.angularVelocity += 20f * Vector3.one * Time.deltaTime;
		this.rigidbody.transform.localScale = Vector3.MoveTowards(this.rigidbody.transform.localScale, 0.5f * Vector3.one, 2f * Time.deltaTime);
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00006E00 File Offset: 0x00005000
	private void OnTriggerStay(Collider other)
	{
		if (base.enabled && !this.pickingUp && Time.time > this.delayTime)
		{
			this.StartPickUp();
		}
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00006E25 File Offset: 0x00005025
	public void OnTimeout()
	{
		this.resource.SetAmountSecret(this.resource.Amount + this.resourceValue);
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00032828 File Offset: 0x00030A28
	private void StartPickUp()
	{
		if (this.waterPhysics != null)
		{
			global::UnityEngine.Object.Destroy(this.waterPhysics.gameObject);
		}
		this.rigidbody.drag = 3f;
		this.rigidbody.useGravity = false;
		this.pickingUp = true;
		if (this.collider != null)
		{
			this.collider.enabled = false;
		}
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x00032890 File Offset: 0x00030A90
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
