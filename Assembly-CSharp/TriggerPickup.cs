using System;
using UnityEngine;

// Token: 0x02000109 RID: 265
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

	// Token: 0x04000768 RID: 1896
	public static float quickPickupTime = -1f;

	// Token: 0x04000769 RID: 1897
	public float delay = 0.5f;

	// Token: 0x0400076A RID: 1898
	private float delayTime = -1f;

	// Token: 0x0400076B RID: 1899
	private const float minAuto = 3f;

	// Token: 0x0400076C RID: 1900
	private const float maxAuto = 4f;

	// Token: 0x0400076D RID: 1901
	private float autoTime;

	// Token: 0x0400076E RID: 1902
	public const float maxDistance = 17f;

	// Token: 0x0400076F RID: 1903
	private bool pickingUp;

	// Token: 0x04000770 RID: 1904
	private Vector3 initialPosition;

	// Token: 0x04000771 RID: 1905
	public Rigidbody rigidbody;

	// Token: 0x04000772 RID: 1906
	public Collider collider;

	// Token: 0x04000773 RID: 1907
	public SphereCollider sphereTrigger;

	// Token: 0x04000774 RID: 1908
	public float pickupAcc = 30f;

	// Token: 0x04000775 RID: 1909
	public float pickupDrag = 0.7f;

	// Token: 0x04000776 RID: 1910
	public AudioSourceVariance audioSourceVariance;

	// Token: 0x04000777 RID: 1911
	public ItemResource resource;

	// Token: 0x04000778 RID: 1912
	public int resourceValue = 1;

	// Token: 0x04000779 RID: 1913
	public WaterPhysics waterPhysics;

	// Token: 0x0400077A RID: 1914
	public bool allowPlayerCollision;

	// Token: 0x0400077B RID: 1915
	private bool initialized;

	// Token: 0x0400077C RID: 1916
	private bool isQuick;
}
