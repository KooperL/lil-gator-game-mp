using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000062 RID: 98
public class FlyingBugTrigger : MonoBehaviour
{
	// Token: 0x06000148 RID: 328 RVA: 0x000031C3 File Offset: 0x000013C3
	private void OnValidate()
	{
		if (this.cart == null)
		{
			this.cart = base.GetComponentInParent<CinemachineDollyCart>();
		}
	}

	// Token: 0x06000149 RID: 329 RVA: 0x000031DF File Offset: 0x000013DF
	private void Awake()
	{
		this.maxPos = this.cart.m_Path.MaxUnit(this.cart.m_PositionUnits);
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00003202 File Offset: 0x00001402
	private void OnTriggerStay(Collider other)
	{
		if (base.enabled)
		{
			this.stepsSinceTriggered = 0;
		}
	}

	// Token: 0x0600014B RID: 331 RVA: 0x0001B89C File Offset: 0x00019A9C
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

	// Token: 0x040001F7 RID: 503
	public CinemachineDollyCart cart;

	// Token: 0x040001F8 RID: 504
	public float acceleration;

	// Token: 0x040001F9 RID: 505
	public float deceleration;

	// Token: 0x040001FA RID: 506
	public float speed = 5f;

	// Token: 0x040001FB RID: 507
	private int stepsSinceTriggered;

	// Token: 0x040001FC RID: 508
	private const float timeUntilReverse = 10f;

	// Token: 0x040001FD RID: 509
	private float maxPos;

	// Token: 0x040001FE RID: 510
	public UnityEvent onReachEnd;

	// Token: 0x040001FF RID: 511
	public MultilingualTextDocument document;

	// Token: 0x04000200 RID: 512
	public FlyingBugTrigger.ChaseShout[] chaseShouts;

	// Token: 0x02000063 RID: 99
	[Serializable]
	public struct ChaseShout
	{
		// Token: 0x04000201 RID: 513
		[ChunkLookup("document")]
		public string shoutID;

		// Token: 0x04000202 RID: 514
		public float distanceTrigger;

		// Token: 0x04000203 RID: 515
		public bool triggered;
	}
}
