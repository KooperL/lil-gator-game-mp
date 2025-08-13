using System;
using UnityEngine;

// Token: 0x0200015E RID: 350
public class TriggerPickup : MonoBehaviour, IOnTimeout
{
	// Token: 0x06000680 RID: 1664 RVA: 0x00006B1E File Offset: 0x00004D1E
	public static void TriggerQuickPickup()
	{
		TriggerPickup.quickPickupTime = Time.time;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x00030FC4 File Offset: 0x0002F1C4
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

	// Token: 0x06000682 RID: 1666 RVA: 0x00006B2A File Offset: 0x00004D2A
	private void Start()
	{
		if (!this.initialized)
		{
			this.Initialize();
		}
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x00031048 File Offset: 0x0002F248
	public void Initialize()
	{
		this.initialized = true;
		this.initialPosition = base.transform.position;
		this.delayTime = Time.time + this.delay + Random.Range(0f, 0.25f);
		this.autoTime = Time.time + Random.Range(3f, 4f);
		this.rigidbody.interpolation = 1;
		this.sphereTrigger.radius *= 1f / base.transform.parent.parent.localScale.x;
		if (!this.allowPlayerCollision)
		{
			this.rigidbody.gameObject.layer = 23;
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x00031104 File Offset: 0x0002F304
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
		this.rigidbody.AddForce((this.isQuick ? 100f : 50f) / magnitude * vector, 5);
		this.rigidbody.angularVelocity += 20f * Vector3.one * Time.deltaTime;
		this.rigidbody.transform.localScale = Vector3.MoveTowards(this.rigidbody.transform.localScale, 0.5f * Vector3.one, 2f * Time.deltaTime);
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x00006B3A File Offset: 0x00004D3A
	private void OnTriggerStay(Collider other)
	{
		if (base.enabled && !this.pickingUp && Time.time > this.delayTime)
		{
			this.StartPickUp();
		}
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x00006B5F File Offset: 0x00004D5F
	public void OnTimeout()
	{
		this.resource.SetAmountSecret(this.resource.Amount + this.resourceValue);
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x000312A8 File Offset: 0x0002F4A8
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

	// Token: 0x06000688 RID: 1672 RVA: 0x00031310 File Offset: 0x0002F510
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

	// Token: 0x040008BC RID: 2236
	public static float quickPickupTime = -1f;

	// Token: 0x040008BD RID: 2237
	public float delay = 0.5f;

	// Token: 0x040008BE RID: 2238
	private float delayTime = -1f;

	// Token: 0x040008BF RID: 2239
	private const float minAuto = 3f;

	// Token: 0x040008C0 RID: 2240
	private const float maxAuto = 4f;

	// Token: 0x040008C1 RID: 2241
	private float autoTime;

	// Token: 0x040008C2 RID: 2242
	public const float maxDistance = 17f;

	// Token: 0x040008C3 RID: 2243
	private bool pickingUp;

	// Token: 0x040008C4 RID: 2244
	private Vector3 initialPosition;

	// Token: 0x040008C5 RID: 2245
	public Rigidbody rigidbody;

	// Token: 0x040008C6 RID: 2246
	public Collider collider;

	// Token: 0x040008C7 RID: 2247
	public SphereCollider sphereTrigger;

	// Token: 0x040008C8 RID: 2248
	public float pickupAcc = 30f;

	// Token: 0x040008C9 RID: 2249
	public float pickupDrag = 0.7f;

	// Token: 0x040008CA RID: 2250
	public AudioSourceVariance audioSourceVariance;

	// Token: 0x040008CB RID: 2251
	public ItemResource resource;

	// Token: 0x040008CC RID: 2252
	public int resourceValue = 1;

	// Token: 0x040008CD RID: 2253
	public WaterPhysics waterPhysics;

	// Token: 0x040008CE RID: 2254
	public bool allowPlayerCollision;

	// Token: 0x040008CF RID: 2255
	private bool initialized;

	// Token: 0x040008D0 RID: 2256
	private bool isQuick;
}
