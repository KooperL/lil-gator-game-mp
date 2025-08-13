using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200004B RID: 75
public class FlyingBugTrigger : MonoBehaviour
{
	// Token: 0x06000123 RID: 291 RVA: 0x00007241 File Offset: 0x00005441
	private void OnValidate()
	{
		if (this.cart == null)
		{
			this.cart = base.GetComponentInParent<CinemachineDollyCart>();
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000725D File Offset: 0x0000545D
	private void Awake()
	{
		this.maxPos = this.cart.m_Path.MaxUnit(this.cart.m_PositionUnits);
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00007280 File Offset: 0x00005480
	private void OnTriggerStay(Collider other)
	{
		if (base.enabled)
		{
			this.stepsSinceTriggered = 0;
		}
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00007294 File Offset: 0x00005494
	private void FixedUpdate()
	{
		bool flag = this.stepsSinceTriggered < 2;
		bool flag2 = (float)this.stepsSinceTriggered * Time.fixedDeltaTime > 10f;
		float num = (flag ? this.speed : 0f);
		float num2 = (flag ? this.acceleration : this.deceleration);
		if (flag2)
		{
			num = -0.5f * this.speed;
			num2 = 0.5f * this.acceleration;
		}
		float pathLength = this.cart.m_Path.PathLength;
		for (int i = 0; i < this.chaseShouts.Length; i++)
		{
			if (flag2)
			{
				if (this.chaseShouts[i].triggered && this.cart.m_Position / pathLength < this.chaseShouts[i].distanceTrigger)
				{
					this.chaseShouts[i].triggered = false;
				}
			}
			else if (!this.chaseShouts[i].triggered && this.cart.m_Position / pathLength > this.chaseShouts[i].distanceTrigger && !DialogueManager.d.isInBubbleDialogue)
			{
				this.chaseShouts[i].triggered = true;
				DialogueManager.d.Bubble(this.document.FetchChunk(this.chaseShouts[i].shoutID), null, 0f, false, true, true);
			}
		}
		this.cart.m_Speed = Mathf.MoveTowards(this.cart.m_Speed, num, num2 * Time.deltaTime);
		this.stepsSinceTriggered++;
		if (this.cart.m_Position >= this.maxPos)
		{
			this.onReachEnd.Invoke();
			base.enabled = false;
		}
	}

	// Token: 0x04000199 RID: 409
	public CinemachineDollyCart cart;

	// Token: 0x0400019A RID: 410
	public float acceleration;

	// Token: 0x0400019B RID: 411
	public float deceleration;

	// Token: 0x0400019C RID: 412
	public float speed = 5f;

	// Token: 0x0400019D RID: 413
	private int stepsSinceTriggered;

	// Token: 0x0400019E RID: 414
	private const float timeUntilReverse = 10f;

	// Token: 0x0400019F RID: 415
	private float maxPos;

	// Token: 0x040001A0 RID: 416
	public UnityEvent onReachEnd;

	// Token: 0x040001A1 RID: 417
	public MultilingualTextDocument document;

	// Token: 0x040001A2 RID: 418
	public FlyingBugTrigger.ChaseShout[] chaseShouts;

	// Token: 0x02000366 RID: 870
	[Serializable]
	public struct ChaseShout
	{
		// Token: 0x04001A30 RID: 6704
		[ChunkLookup("document")]
		public string shoutID;

		// Token: 0x04001A31 RID: 6705
		public float distanceTrigger;

		// Token: 0x04001A32 RID: 6706
		public bool triggered;
	}
}
