using System;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyReset : MonoBehaviour, IManagedUpdate
{
	// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00009F00 File Offset: 0x00008100
	private bool JustReset
	{
		get
		{
			return Time.time - this.resetTime < 8f * Time.fixedDeltaTime;
		}
	}

	// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00009F1B File Offset: 0x0000811B
	// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00009F23 File Offset: 0x00008123
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

	// Token: 0x06000A6E RID: 2670 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void OnAwakeChange()
	{
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x00009F49 File Offset: 0x00008149
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

	// Token: 0x06000A70 RID: 2672 RVA: 0x00009F7F File Offset: 0x0000817F
	private void Awake()
	{
		this.initialPosition = this.rigidbody.position;
		this.initialRotation = this.rigidbody.rotation;
		this.rigidbody.Sleep();
		this.IsAwake = false;
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x00009FB5 File Offset: 0x000081B5
	private void Start()
	{
		this.mainCamera = MainCamera.t;
		this.FindAdjacentResets();
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x00009FC8 File Offset: 0x000081C8
	private void OnTriggerEnter()
	{
		if (!this.JustReset && !this.rigidbody.IsSleeping())
		{
			this.IsAwake = true;
		}
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x00009FC8 File Offset: 0x000081C8
	private void OnTriggerExit()
	{
		if (!this.JustReset && !this.rigidbody.IsSleeping())
		{
			this.IsAwake = true;
		}
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x00009FC8 File Offset: 0x000081C8
	private void OnCollisionEnter()
	{
		if (!this.JustReset && !this.rigidbody.IsSleeping())
		{
			this.IsAwake = true;
		}
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x00009FC8 File Offset: 0x000081C8
	private void OnCollisionExit()
	{
		if (!this.JustReset && !this.rigidbody.IsSleeping())
		{
			this.IsAwake = true;
		}
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x00009FE6 File Offset: 0x000081E6
	public void OnEnable()
	{
		FastUpdateManager.fixedUpdate16.Add(this);
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x00009FF3 File Offset: 0x000081F3
	public void OnDisable()
	{
		FastUpdateManager.fixedUpdate16.Remove(this);
	}

	// Token: 0x06000A78 RID: 2680 RVA: 0x0003D1DC File Offset: 0x0003B3DC
	private bool IsPositionClear(Vector3 position, Vector3 referencePosition, Vector3 referenceForward)
	{
		Vector3 vector = position - referencePosition;
		if (Vector3.Dot(vector, referenceForward) < 0f)
		{
			vector *= 3f;
		}
		return Vector3.SqrMagnitude(vector) > 2025f;
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0003D218 File Offset: 0x0003B418
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

	// Token: 0x06000A7A RID: 2682 RVA: 0x0003D27C File Offset: 0x0003B47C
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

	// Token: 0x06000A7B RID: 2683 RVA: 0x0003D334 File Offset: 0x0003B534
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

	// Token: 0x06000A7C RID: 2684 RVA: 0x0003D3C0 File Offset: 0x0003B5C0
	public void ResetToInitial()
	{
		this.resetTime = Time.time;
		this.ResetAdjacentResets();
		this.rigidbody.velocity = Vector3.zero;
		this.rigidbody.angularVelocity = Vector3.zero;
		this.rigidbody.position = this.initialPosition;
		this.rigidbody.rotation = this.initialRotation;
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0003D420 File Offset: 0x0003B620
	private void ResetAdjacentResets()
	{
		RigidbodyReset[] array = this.adjacentResets;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].TryToReset();
		}
	}

	private static Collider[] results = new Collider[10];

	private const float resetDistance = 45f;

	private const float sqrResetDistance = 2025f;

	public Rigidbody rigidbody;

	public WaterPhysics waterPhysics;

	private Transform mainCamera;

	protected Vector3 initialPosition;

	private Quaternion initialRotation;

	private float resetTime = -1f;

	private RigidbodyReset[] adjacentResets;

	[ReadOnly]
	public bool isAwake;
}
