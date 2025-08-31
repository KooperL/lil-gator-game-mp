using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

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

	public CinemachineDollyCart cart;

	public float acceleration;

	public float deceleration;

	public float speed = 5f;

	private int stepsSinceTriggered;

	private const float timeUntilReverse = 10f;

	private float maxPos;

	public UnityEvent onReachEnd;

	public MultilingualTextDocument document;

	public FlyingBugTrigger.ChaseShout[] chaseShouts;

	[Serializable]
	public struct ChaseShout
	{
		[ChunkLookup("document")]
		public string shoutID;

		public float distanceTrigger;

		public bool triggered;
	}
}
