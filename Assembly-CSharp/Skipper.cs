using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000221 RID: 545
[RequireComponent(typeof(Collider))]
public class Skipper : MonoBehaviour
{
	// Token: 0x06000A36 RID: 2614 RVA: 0x0003B9C4 File Offset: 0x00039BC4
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

	// Token: 0x06000A37 RID: 2615 RVA: 0x00009CE4 File Offset: 0x00007EE4
	private void Start()
	{
		this.randomSkipFactor = Random.Range(0.75f, 1f);
		this.randomFriction = Random.value;
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x0003BA18 File Offset: 0x00039C18
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

	// Token: 0x06000A39 RID: 2617 RVA: 0x00009D06 File Offset: 0x00007F06
	private void OnTriggerStay(Collider other)
	{
		this.stayCount++;
		if (this.stayCount > 1 && this.rigidbody.velocity.y < 0f)
		{
			this.Sink();
		}
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00009D3C File Offset: 0x00007F3C
	private void OnTriggerExit(Collider other)
	{
		this.stayCount = 0;
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x00009D45 File Offset: 0x00007F45
	private void Sink()
	{
		this.waterPhysics.activateAutomatically = true;
		this.onSink.Invoke();
		Object.Destroy(this);
	}

	// Token: 0x04000CAE RID: 3246
	public static int bestSkip;

	// Token: 0x04000CAF RID: 3247
	public static bool isPartOfQuest;

	// Token: 0x04000CB0 RID: 3248
	[ReadOnly]
	public int waterLayer;

	// Token: 0x04000CB1 RID: 3249
	public Rigidbody rigidbody;

	// Token: 0x04000CB2 RID: 3250
	public WaterPhysics waterPhysics;

	// Token: 0x04000CB3 RID: 3251
	public float spin = 10f;

	// Token: 0x04000CB4 RID: 3252
	private const float maxBounceSpeed = 6f;

	// Token: 0x04000CB5 RID: 3253
	public float minSkipSpeed = 3f;

	// Token: 0x04000CB6 RID: 3254
	public float minSkipFriction = 0.7f;

	// Token: 0x04000CB7 RID: 3255
	public float maxSkipFriction = 0.95f;

	// Token: 0x04000CB8 RID: 3256
	public float minBounceFriction = 0.7f;

	// Token: 0x04000CB9 RID: 3257
	public float maxBounceFriction = 1f;

	// Token: 0x04000CBA RID: 3258
	public GameObject skipEffect;

	// Token: 0x04000CBB RID: 3259
	public UnityEvent onSink;

	// Token: 0x04000CBC RID: 3260
	private int skipCount;

	// Token: 0x04000CBD RID: 3261
	private float randomSkipFactor = 1f;

	// Token: 0x04000CBE RID: 3262
	private float randomFriction = 1f;

	// Token: 0x04000CBF RID: 3263
	private int stayCount;
}
