using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Skipper : MonoBehaviour
{
	// Token: 0x06000A81 RID: 2689 RVA: 0x0003D738 File Offset: 0x0003B938
	private void OnValidate()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponentInParent<Rigidbody>();
		}
		if (this.waterPhysics == null)
		{
			this.waterPhysics = base.GetComponent<WaterPhysics>();
		}
		this.waterLayer = LayerMask.NameToLayer("Water");
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0000A022 File Offset: 0x00008222
	private void Start()
	{
		this.randomSkipFactor = global::UnityEngine.Random.Range(0.75f, 1f);
		this.randomFriction = global::UnityEngine.Random.value;
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0003D78C File Offset: 0x0003B98C
	private void OnTriggerEnter(Collider other)
	{
		this.stayCount = 0;
		if (other.gameObject.layer != this.waterLayer)
		{
			return;
		}
		Vector3 vector = this.rigidbody.velocity;
		if (vector.y < 0f)
		{
			float magnitude = new Vector2(vector.x, vector.z).magnitude;
			float num = magnitude / (Mathf.Abs(vector.y) + 0.0001f);
			float num2 = 57.29578f * Mathf.Atan(Mathf.Abs(vector.y) / magnitude);
			float num3 = Mathf.InverseLerp(30f, 0f, num2);
			num3 = Mathf.Pow(num3, 0.5f);
			if (num3 > 0f && magnitude > this.minSkipSpeed)
			{
				vector.y = Mathf.Abs(vector.y);
				vector *= num3;
				this.rigidbody.velocity = vector;
				Water component = other.GetComponent<Water>();
				Vector3 worldCenterOfMass = this.rigidbody.worldCenterOfMass;
				worldCenterOfMass.y = component.GetWaterPlaneHeight(this.rigidbody.position);
				global::UnityEngine.Object.Instantiate<GameObject>(this.skipEffect, worldCenterOfMass, Quaternion.identity);
				this.skipCount++;
				Skipper.bestSkip = Mathf.Max(Skipper.bestSkip, this.skipCount);
				if (Skipper.isPartOfQuest)
				{
					PlayAudio.p.PlayQuestSting(this.skipCount - 1);
					return;
				}
			}
			else
			{
				Skipper.bestSkip = Mathf.Max(Skipper.bestSkip, this.skipCount);
				this.Sink();
			}
		}
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x0000A044 File Offset: 0x00008244
	private void OnTriggerStay(Collider other)
	{
		this.stayCount++;
		if (this.stayCount > 1 && this.rigidbody.velocity.y < 0f)
		{
			this.Sink();
		}
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0000A07A File Offset: 0x0000827A
	private void OnTriggerExit(Collider other)
	{
		this.stayCount = 0;
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0000A083 File Offset: 0x00008283
	private void Sink()
	{
		this.waterPhysics.activateAutomatically = true;
		this.onSink.Invoke();
		global::UnityEngine.Object.Destroy(this);
	}

	public static int bestSkip;

	public static bool isPartOfQuest;

	[ReadOnly]
	public int waterLayer;

	public Rigidbody rigidbody;

	public WaterPhysics waterPhysics;

	public float spin = 10f;

	private const float maxBounceSpeed = 6f;

	public float minSkipSpeed = 3f;

	public float minSkipFriction = 0.7f;

	public float maxSkipFriction = 0.95f;

	public float minBounceFriction = 0.7f;

	public float maxBounceFriction = 1f;

	public GameObject skipEffect;

	public UnityEvent onSink;

	private int skipCount;

	private float randomSkipFactor = 1f;

	private float randomFriction = 1f;

	private int stayCount;
}
