using System;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyReset : MonoBehaviour, IManagedUpdate
{
	// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00028C2B File Offset: 0x00026E2B
	private bool JustReset
	{
		get
		{
			return Time.time - this.resetTime < 8f * Time.fixedDeltaTime;
		}
	}

	// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00028C46 File Offset: 0x00026E46
	// (set) Token: 0x060008A2 RID: 2210 RVA: 0x00028C4E File Offset: 0x00026E4E
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

	// Token: 0x060008A3 RID: 2211 RVA: 0x00028C74 File Offset: 0x00026E74
	public virtual void OnAwakeChange()
	{
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x00028C76 File Offset: 0x00026E76
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

	// Token: 0x060008A5 RID: 2213 RVA: 0x00028CAC File Offset: 0x00026EAC
	private void Awake()
	{
		this.initialPosition = this.rigidbody.position;
		this.initialRotation = this.rigidbody.rotation;
		this.rigidbody.Sleep();
		this.IsAwake = false;
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x00028CE2 File Offset: 0x00026EE2
	private void Start()
	{
		this.mainCamera = MainCamera.t;
		this.FindAdjacentResets();
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x00028CF5 File Offset: 0x00026EF5
	private void OnTriggerEnter()
	{
		if (!this.JustReset && !this.rigidbody.IsSleeping())
		{
			this.IsAwake = true;
		}
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x00028D13 File Offset: 0x00026F13
	private void OnTriggerExit()
	{
		if (!this.JustReset && !this.rigidbody.IsSleeping())
		{
			this.IsAwake = true;
		}
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x00028D31 File Offset: 0x00026F31
	private void OnCollisionEnter()
	{
		if (!this.JustReset && !this.rigidbody.IsSleeping())
		{
			this.IsAwake = true;
		}
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x00028D4F File Offset: 0x00026F4F
	private void OnCollisionExit()
	{
		if (!this.JustReset && !this.rigidbody.IsSleeping())
		{
			this.IsAwake = true;
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x00028D6D File Offset: 0x00026F6D
	public void OnEnable()
	{
		FastUpdateManager.fixedUpdate16.Add(this);
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x00028D7A File Offset: 0x00026F7A
	public void OnDisable()
	{
		FastUpdateManager.fixedUpdate16.Remove(this);
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x00028D88 File Offset: 0x00026F88
	private bool IsPositionClear(Vector3 position, Vector3 referencePosition, Vector3 referenceForward)
	{
		Vector3 vector = position - referencePosition;
		if (Vector3.Dot(vector, referenceForward) < 0f)
		{
			vector *= 3f;
		}
		return Vector3.SqrMagnitude(vector) > 2025f;
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x00028DC4 File Offset: 0x00026FC4
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

	// Token: 0x060008AF RID: 2223 RVA: 0x00028E28 File Offset: 0x00027028
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

	// Token: 0x060008B0 RID: 2224 RVA: 0x00028EE0 File Offset: 0x000270E0
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

	// Token: 0x060008B1 RID: 2225 RVA: 0x00028F6C File Offset: 0x0002716C
	public void ResetToInitial()
	{
		this.resetTime = Time.time;
		this.ResetAdjacentResets();
		this.rigidbody.velocity = Vector3.zero;
		this.rigidbody.angularVelocity = Vector3.zero;
		this.rigidbody.position = this.initialPosition;
		this.rigidbody.rotation = this.initialRotation;
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x00028FCC File Offset: 0x000271CC
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
