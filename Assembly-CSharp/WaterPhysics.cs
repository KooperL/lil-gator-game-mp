using System;
using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
	// Token: 0x06000A88 RID: 2696 RVA: 0x0000A0A2 File Offset: 0x000082A2
	private void Awake()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponentInParent<Rigidbody>();
		}
		this.initialDrag = this.rigidbody.drag;
		this.initialAngularDrag = this.rigidbody.angularDrag;
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0000A0E0 File Offset: 0x000082E0
	private void Start()
	{
		if (this.accurateCollider == null || !this.accurateCollider.enabled)
		{
			this.accurateCollider = this.collider;
		}
		base.enabled = false;
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0000A110 File Offset: 0x00008310
	private void OnDisable()
	{
		this.waterPlaneHeight = -100f;
		this.rigidbody.drag = this.initialDrag;
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0000A12E File Offset: 0x0000832E
	private void OnTriggerEnter(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x0000A12E File Offset: 0x0000832E
	private void OnTriggerStay(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x0003D978 File Offset: 0x0003BB78
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

	// Token: 0x06000A8E RID: 2702 RVA: 0x0003DA08 File Offset: 0x0003BC08
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
