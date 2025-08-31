using System;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
	// Token: 0x060008BC RID: 2236 RVA: 0x000292D7 File Offset: 0x000274D7
	private void Awake()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponentInParent<Rigidbody>();
		}
		this.initialDrag = this.rigidbody.drag;
		this.initialAngularDrag = this.rigidbody.angularDrag;
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00029315 File Offset: 0x00027515
	private void Start()
	{
		if (this.accurateCollider == null || !this.accurateCollider.enabled)
		{
			this.accurateCollider = this.collider;
		}
		base.enabled = false;
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00029345 File Offset: 0x00027545
	private void OnDisable()
	{
		this.waterPlaneHeight = -100f;
		this.rigidbody.drag = this.initialDrag;
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00029363 File Offset: 0x00027563
	private void OnTriggerEnter(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0002936C File Offset: 0x0002756C
	private void OnTriggerStay(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x00029378 File Offset: 0x00027578
	private void WaterTrigger(Collider collider)
	{
		if (collider.gameObject.layer != 4)
		{
			return;
		}
		if (!this.activateAutomatically && !base.enabled)
		{
			return;
		}
		Water component = collider.GetComponent<Water>();
		if (component == null)
		{
			return;
		}
		float num = component.GetWaterPlaneHeight(this.rigidbody.position);
		if (this.stepsSinceTriggered < 1 && this.water != null && num < this.waterPlaneHeight)
		{
			return;
		}
		this.water = component;
		this.waterPlaneHeight = num;
		this.stepsSinceTriggered = 0;
		base.enabled = true;
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x00029408 File Offset: 0x00027608
	private void FixedUpdate()
	{
		this.stepsSinceTriggered++;
		if (this.stepsSinceTriggered > 5)
		{
			base.enabled = false;
		}
		if (this.water != null)
		{
			this.waterPlaneHeight = this.water.GetWaterPlaneHeight(this.rigidbody.worldCenterOfMass);
		}
		Bounds bounds = ((this.accurateCollider != null) ? this.accurateCollider.bounds : this.collider.bounds);
		float num = Mathf.InverseLerp(bounds.min.y, bounds.max.y, this.waterPlaneHeight);
		if (num > 0.2f && Time.time - this.splashTime > 1f && this.rigidbody.velocity.sqrMagnitude > 4f)
		{
			this.splashTime = Time.time;
			Vector3 worldCenterOfMass = this.rigidbody.worldCenterOfMass;
			worldCenterOfMass.y = this.waterPlaneHeight;
			EffectsManager.e.Splash(worldCenterOfMass, 0.5f);
		}
		this.rigidbody.drag = Mathf.Lerp(this.initialDrag, this.drag, num);
		this.rigidbody.angularDrag = Mathf.Lerp(this.initialAngularDrag, this.angularDrag, num);
		float num2 = this.rippleRate * ((num >= 1f) ? 0f : num);
		this.rippleCount += Time.deltaTime * num2;
		if (this.rippleCount > 1f)
		{
			Vector3 worldCenterOfMass2 = this.rigidbody.worldCenterOfMass;
			worldCenterOfMass2.y = this.waterPlaneHeight;
			EffectsManager.e.Ripple(worldCenterOfMass2, Mathf.FloorToInt(this.rippleCount));
			this.rippleCount -= Mathf.Floor(this.rippleCount);
		}
		if (num == 0f)
		{
			this.stepsSinceSubmerged++;
			if (this.stepsSinceSubmerged > 5 && this.stepsSinceTriggered > 5)
			{
				base.enabled = false;
				return;
			}
		}
		else if (!this.rigidbody.IsSleeping())
		{
			this.rigidbody.AddForceAtPosition(this.buoyancy * num * -Physics.gravity, base.transform.TransformPoint(this.centerOfBuoyancy), ForceMode.Acceleration);
		}
	}

	public const int waterLayer = 4;

	public Rigidbody rigidbody;

	public Collider collider;

	public Collider accurateCollider;

	public bool activateAutomatically = true;

	[Range(0f, 2f)]
	public float buoyancy = 1.1f;

	public Vector3 buoyancyOffset = Vector3.up;

	[ReadOnly]
	public Vector3 centerOfBuoyancy;

	private float initialDrag;

	[Range(0f, 5f)]
	public float drag = 2f;

	private float initialAngularDrag;

	[Range(0f, 5f)]
	public float angularDrag = 1f;

	private Water water;

	private float waterPlaneHeight;

	public float rippleRate = 2f;

	private int stepsSinceTriggered;

	private int stepsSinceSubmerged;

	private float rippleCount;

	private float splashTime = -1f;
}
