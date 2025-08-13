using System;
using UnityEngine;

// Token: 0x02000222 RID: 546
public class WaterPhysics : MonoBehaviour
{
	// Token: 0x06000A3D RID: 2621 RVA: 0x00009D64 File Offset: 0x00007F64
	private void Awake()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponentInParent<Rigidbody>();
		}
		this.initialDrag = this.rigidbody.drag;
		this.initialAngularDrag = this.rigidbody.angularDrag;
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x00009DA2 File Offset: 0x00007FA2
	private void Start()
	{
		if (this.accurateCollider == null || !this.accurateCollider.enabled)
		{
			this.accurateCollider = this.collider;
		}
		base.enabled = false;
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x00009DD2 File Offset: 0x00007FD2
	private void OnDisable()
	{
		this.waterPlaneHeight = -100f;
		this.rigidbody.drag = this.initialDrag;
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x00009DF0 File Offset: 0x00007FF0
	private void OnTriggerEnter(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x00009DF0 File Offset: 0x00007FF0
	private void OnTriggerStay(Collider other)
	{
		this.WaterTrigger(other);
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0003BC04 File Offset: 0x00039E04
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

	// Token: 0x06000A43 RID: 2627 RVA: 0x0003BC94 File Offset: 0x00039E94
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
			this.rigidbody.AddForceAtPosition(this.buoyancy * num * -Physics.gravity, base.transform.TransformPoint(this.centerOfBuoyancy), 5);
		}
	}

	// Token: 0x04000CC0 RID: 3264
	public const int waterLayer = 4;

	// Token: 0x04000CC1 RID: 3265
	public Rigidbody rigidbody;

	// Token: 0x04000CC2 RID: 3266
	public Collider collider;

	// Token: 0x04000CC3 RID: 3267
	public Collider accurateCollider;

	// Token: 0x04000CC4 RID: 3268
	public bool activateAutomatically = true;

	// Token: 0x04000CC5 RID: 3269
	[Range(0f, 2f)]
	public float buoyancy = 1.1f;

	// Token: 0x04000CC6 RID: 3270
	public Vector3 buoyancyOffset = Vector3.up;

	// Token: 0x04000CC7 RID: 3271
	[ReadOnly]
	public Vector3 centerOfBuoyancy;

	// Token: 0x04000CC8 RID: 3272
	private float initialDrag;

	// Token: 0x04000CC9 RID: 3273
	[Range(0f, 5f)]
	public float drag = 2f;

	// Token: 0x04000CCA RID: 3274
	private float initialAngularDrag;

	// Token: 0x04000CCB RID: 3275
	[Range(0f, 5f)]
	public float angularDrag = 1f;

	// Token: 0x04000CCC RID: 3276
	private Water water;

	// Token: 0x04000CCD RID: 3277
	private float waterPlaneHeight;

	// Token: 0x04000CCE RID: 3278
	public float rippleRate = 2f;

	// Token: 0x04000CCF RID: 3279
	private int stepsSinceTriggered;

	// Token: 0x04000CD0 RID: 3280
	private int stepsSinceSubmerged;

	// Token: 0x04000CD1 RID: 3281
	private float rippleCount;

	// Token: 0x04000CD2 RID: 3282
	private float splashTime = -1f;
}
