using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001A7 RID: 423
[RequireComponent(typeof(Collider))]
public class Skipper : MonoBehaviour
{
	// Token: 0x060008B5 RID: 2229 RVA: 0x00029018 File Offset: 0x00027218
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

	// Token: 0x060008B6 RID: 2230 RVA: 0x00029069 File Offset: 0x00027269
	private void Start()
	{
		this.randomSkipFactor = Random.Range(0.75f, 1f);
		this.randomFriction = Random.value;
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0002908C File Offset: 0x0002728C
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
				Object.Instantiate<GameObject>(this.skipEffect, worldCenterOfMass, Quaternion.identity);
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

	// Token: 0x060008B8 RID: 2232 RVA: 0x0002920B File Offset: 0x0002740B
	private void OnTriggerStay(Collider other)
	{
		this.stayCount++;
		if (this.stayCount > 1 && this.rigidbody.velocity.y < 0f)
		{
			this.Sink();
		}
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00029241 File Offset: 0x00027441
	private void OnTriggerExit(Collider other)
	{
		this.stayCount = 0;
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x0002924A File Offset: 0x0002744A
	private void Sink()
	{
		this.waterPhysics.activateAutomatically = true;
		this.onSink.Invoke();
		Object.Destroy(this);
	}

	// Token: 0x04000AAE RID: 2734
	public static int bestSkip;

	// Token: 0x04000AAF RID: 2735
	public static bool isPartOfQuest;

	// Token: 0x04000AB0 RID: 2736
	[ReadOnly]
	public int waterLayer;

	// Token: 0x04000AB1 RID: 2737
	public Rigidbody rigidbody;

	// Token: 0x04000AB2 RID: 2738
	public WaterPhysics waterPhysics;

	// Token: 0x04000AB3 RID: 2739
	public float spin = 10f;

	// Token: 0x04000AB4 RID: 2740
	private const float maxBounceSpeed = 6f;

	// Token: 0x04000AB5 RID: 2741
	public float minSkipSpeed = 3f;

	// Token: 0x04000AB6 RID: 2742
	public float minSkipFriction = 0.7f;

	// Token: 0x04000AB7 RID: 2743
	public float maxSkipFriction = 0.95f;

	// Token: 0x04000AB8 RID: 2744
	public float minBounceFriction = 0.7f;

	// Token: 0x04000AB9 RID: 2745
	public float maxBounceFriction = 1f;

	// Token: 0x04000ABA RID: 2746
	public GameObject skipEffect;

	// Token: 0x04000ABB RID: 2747
	public UnityEvent onSink;

	// Token: 0x04000ABC RID: 2748
	private int skipCount;

	// Token: 0x04000ABD RID: 2749
	private float randomSkipFactor = 1f;

	// Token: 0x04000ABE RID: 2750
	private float randomFriction = 1f;

	// Token: 0x04000ABF RID: 2751
	private int stayCount;
}
