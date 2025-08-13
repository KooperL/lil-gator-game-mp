using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000220 RID: 544
public class RigidbodyReset : MonoBehaviour, IManagedUpdate
{
	// Token: 0x170000FA RID: 250
	// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00009BC2 File Offset: 0x00007DC2
	private bool JustReset
	{
		get
		{
			return Time.time - this.resetTime < 8f * Time.fixedDeltaTime;
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00009BDD File Offset: 0x00007DDD
	// (set) Token: 0x06000A23 RID: 2595 RVA: 0x00009BE5 File Offset: 0x00007DE5
	private bool IsAwake
	{
		get
		{
			return this.isAwake;
		}
		set
		{
			if (this.isAwake != value)
			{
				if (value)
				{
					this.resetTime = -10f;
				}
				this.isAwake = value;
				this.OnAwakeChange();
			}
		}
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void OnAwakeChange()
	{
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x00009C0B File Offset: 0x00007E0B
	public virtual void OnValidate()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
		}
		if (this.waterPhysics == null)
		{
			this.waterPhysics = base.GetComponentInChildren<WaterPhysics>();
		}
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x00009C41 File Offset: 0x00007E41
	private void Awake()
	{
		this.initialPosition = this.rigidbody.position;
		this.initialRotation = this.rigidbody.rotation;
		this.rigidbody.Sleep();
		this.IsAwake = false;
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x00009C77 File Offset: 0x00007E77
	private void Start()
	{
		this.mainCamera = MainCamera.t;
		this.FindAdjacentResets();
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x00009C8A File Offset: 0x00007E8A
	private void OnTriggerEnter()
	{
		if (!this.JustReset && !this.rigidbody.IsSleeping())
		{
			this.IsAwake = true;
		}
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x00009C8A File Offset: 0x00007E8A
	private void OnTriggerExit()
	{
		if (!this.JustReset && !this.rigidbody.IsSleeping())
		{
			this.IsAwake = true;
		}
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x00009C8A File Offset: 0x00007E8A
	private void OnCollisionEnter()
	{
		if (!this.JustReset && !this.rigidbody.IsSleeping())
		{
			this.IsAwake = true;
		}
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x00009C8A File Offset: 0x00007E8A
	private void OnCollisionExit()
	{
		if (!this.JustReset && !this.rigidbody.IsSleeping())
		{
			this.IsAwake = true;
		}
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x00009CA8 File Offset: 0x00007EA8
	public void OnEnable()
	{
		FastUpdateManager.fixedUpdate16.Add(this);
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00009CB5 File Offset: 0x00007EB5
	public void OnDisable()
	{
		FastUpdateManager.fixedUpdate16.Remove(this);
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x0003B754 File Offset: 0x00039954
	private bool IsPositionClear(Vector3 position, Vector3 referencePosition, Vector3 referenceForward)
	{
		Vector3 vector = position - referencePosition;
		if (Vector3.Dot(vector, referenceForward) < 0f)
		{
			vector *= 3f;
		}
		return Vector3.SqrMagnitude(vector) > 2025f;
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x0003B790 File Offset: 0x00039990
	public void ManagedUpdate()
	{
		if (Time.time - this.resetTime < 4f)
		{
			bool flag = this.rigidbody.IsSleeping();
			if (this.IsAwake == flag)
			{
				this.IsAwake = !flag;
				return;
			}
		}
		else
		{
			if (this.IsAwake)
			{
				this.TryToReset();
				return;
			}
			if (!this.rigidbody.IsSleeping())
			{
				this.IsAwake = true;
			}
		}
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x0003B7F4 File Offset: 0x000399F4
	private void FindAdjacentResets()
	{
		Collider component = base.GetComponent<Collider>();
		Bounds bounds = component.bounds;
		if (!component.enabled)
		{
			component.enabled = true;
			bounds = component.bounds;
			component.enabled = false;
		}
		int num = Physics.OverlapBoxNonAlloc(bounds.center, 1.2f * bounds.extents, RigidbodyReset.results);
		List<RigidbodyReset> list = new List<RigidbodyReset>();
		for (int i = 0; i < num; i++)
		{
			Collider collider = RigidbodyReset.results[i];
			if (!(collider.gameObject == base.gameObject))
			{
				RigidbodyReset component2 = collider.GetComponent<RigidbodyReset>();
				if (component2 != null)
				{
					list.Add(component2);
				}
			}
		}
		this.adjacentResets = list.ToArray();
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x0003B8AC File Offset: 0x00039AAC
	public void TryToReset()
	{
		if (this.JustReset)
		{
			return;
		}
		if (this.mainCamera == null)
		{
			this.mainCamera = MainCamera.t;
		}
		Vector3 position = this.mainCamera.position;
		Vector3 forward = this.mainCamera.forward;
		if (this.IsPositionClear(this.rigidbody.position, position, forward) && this.IsPositionClear(this.initialPosition, position, forward))
		{
			if (this.waterPhysics != null)
			{
				this.waterPhysics.enabled = false;
			}
			this.ResetToInitial();
		}
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0003B938 File Offset: 0x00039B38
	public void ResetToInitial()
	{
		this.resetTime = Time.time;
		this.ResetAdjacentResets();
		this.rigidbody.velocity = Vector3.zero;
		this.rigidbody.angularVelocity = Vector3.zero;
		this.rigidbody.position = this.initialPosition;
		this.rigidbody.rotation = this.initialRotation;
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x0003B998 File Offset: 0x00039B98
	private void ResetAdjacentResets()
	{
		RigidbodyReset[] array = this.adjacentResets;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].TryToReset();
		}
	}

	// Token: 0x04000CA3 RID: 3235
	private static Collider[] results = new Collider[10];

	// Token: 0x04000CA4 RID: 3236
	private const float resetDistance = 45f;

	// Token: 0x04000CA5 RID: 3237
	private const float sqrResetDistance = 2025f;

	// Token: 0x04000CA6 RID: 3238
	public Rigidbody rigidbody;

	// Token: 0x04000CA7 RID: 3239
	public WaterPhysics waterPhysics;

	// Token: 0x04000CA8 RID: 3240
	private Transform mainCamera;

	// Token: 0x04000CA9 RID: 3241
	protected Vector3 initialPosition;

	// Token: 0x04000CAA RID: 3242
	private Quaternion initialRotation;

	// Token: 0x04000CAB RID: 3243
	private float resetTime = -1f;

	// Token: 0x04000CAC RID: 3244
	private RigidbodyReset[] adjacentResets;

	// Token: 0x04000CAD RID: 3245
	[ReadOnly]
	public bool isAwake;
}
