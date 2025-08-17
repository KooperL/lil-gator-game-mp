using System;
using UnityEngine;
using UnityEngine.Events;

public class Bird_Standing : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000004 RID: 4 RVA: 0x0000209B File Offset: 0x0000029B
	private void Start()
	{
		this.proximityTrigger.radius *= global::UnityEngine.Random.Range(1f - this.proximityTriggerVariance, 1f + this.proximityTriggerVariance);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000177B4 File Offset: 0x000159B4
	private void OnEnable()
	{
		if (this.attachedTightrope != null)
		{
			this.attachedTightrope.onEnable.AddListener(new UnityAction(this.BeginFlying));
		}
		if (this.attachedBeam != null)
		{
			this.attachedBeam.onEnable.AddListener(new UnityAction(this.BeginFlying));
		}
		if (this.attachedPole != null)
		{
			this.attachedPole.onEnable.AddListener(new UnityAction(this.BeginFlying));
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00017840 File Offset: 0x00015A40
	private void OnDisable()
	{
		if (this.attachedTightrope != null)
		{
			this.attachedTightrope.onEnable.RemoveListener(new UnityAction(this.BeginFlying));
		}
		if (this.attachedBeam != null)
		{
			this.attachedBeam.onEnable.RemoveListener(new UnityAction(this.BeginFlying));
		}
		if (this.attachedPole != null)
		{
			this.attachedPole.onEnable.RemoveListener(new UnityAction(this.BeginFlying));
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000020CC File Offset: 0x000002CC
	private void OnDestroy()
	{
		if (this.respawn && this.isFlying && FastUpdateManager.updateEvery8.Contains(this))
		{
			FastUpdateManager.updateEvery8.Remove(this);
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000020F7 File Offset: 0x000002F7
	public void OnTriggerEnter(Collider other)
	{
		this.BeginFlying();
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000178CC File Offset: 0x00015ACC
	public void BeginFlying()
	{
		if (this.isFlying)
		{
			return;
		}
		this.isFlying = true;
		this.onBeginFlying.Invoke();
		global::UnityEngine.Object.Instantiate<GameObject>(this.flyingPrefab, base.transform.TransformPoint(this.flyingPositionOffset), base.transform.rotation).transform.localScale = base.transform.localScale;
		this.onFly.Invoke();
		if (this.respawn)
		{
			FastUpdateManager.updateEvery8.Add(this);
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0001795C File Offset: 0x00015B5C
	public void ManagedUpdate()
	{
		if (this.attachedTightrope != null && this.attachedTightrope.enabled)
		{
			return;
		}
		if (this.attachedBeam != null && this.attachedBeam.enabled)
		{
			return;
		}
		if (this.attachedPole != null && this.attachedPole.enabled)
		{
			return;
		}
		if (Vector3.Distance(MainCamera.t.position, base.transform.position) < 60f)
		{
			return;
		}
		FastUpdateManager.updateEvery8.Remove(this);
		this.isFlying = false;
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00017A00 File Offset: 0x00015C00
	[ContextMenu("Snap to...")]
	public void SnapTo()
	{
		if (this.attachedTightrope != null)
		{
			base.transform.position = this.attachedTightrope.ClosestPointOnLine(base.transform.position);
		}
		if (this.attachedBeam != null)
		{
			base.transform.position = this.attachedBeam.ClosestPointOnPath(base.transform.position);
		}
		if (this.attachedPole != null)
		{
			base.transform.position = this.attachedPole.climbingPole.GetPosition((float)(this.attachedPole.climbingPole.positions.Length - 1));
		}
	}

	public bool respawn = true;

	public GameObject flyingPrefab;

	public Tightrope attachedTightrope;

	public BendyPole attachedBeam;

	public BendyClimbingPole attachedPole;

	public UnityEvent onBeginFlying;

	public Vector3 flyingPositionOffset;

	public SphereCollider proximityTrigger;

	public float proximityTriggerVariance = 0.25f;

	public UnityEvent onFly;

	private bool isFlying;
}
