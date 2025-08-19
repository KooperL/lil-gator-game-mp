using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class FlyingBugTrigger : MonoBehaviour
{
	// Token: 0x06000150 RID: 336 RVA: 0x00003266 File Offset: 0x00001466
	private void OnValidate()
	{
		if (this.cart == null)
		{
			this.cart = base.GetComponentInParent<CinemachineDollyCart>();
		}
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00003282 File Offset: 0x00001482
	private void Awake()
	{
		this.maxPos = this.cart.m_Path.MaxUnit(this.cart.m_PositionUnits);
	}

	// Token: 0x06000152 RID: 338 RVA: 0x000032A5 File Offset: 0x000014A5
	private void OnTriggerStay(Collider other)
	{
		if (base.enabled)
		{
			this.stepsSinceTriggered = 0;
		}
	}

	// Token: 0x06000153 RID: 339 RVA: 0x0001C08C File Offset: 0x0001A28C
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
