using System;
using UnityEngine;

public class QuestRewardConfetti : QuestReward
{
	// Token: 0x06000E51 RID: 3665 RVA: 0x0000CB60 File Offset: 0x0000AD60
	public override void GiveReward()
	{
		this.GiveReward(this.amount);
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x0004D0B0 File Offset: 0x0004B2B0
	public void GiveReward(int amount)
	{
		Vector3 vector = base.transform.position;
		if (this.actor != null)
		{
			if (this.actor.Position == 0 && !this.forceUpperbodyOnly)
			{
				this.actor.SetEmote(QuestRewardConfetti.throwConfettiEmote, false);
			}
			else
			{
				this.actor.SetEmote(QuestRewardConfetti.throwConfettiUpperbodyEmote, false);
			}
			vector = this.actor.head.position;
		}
		if (this.source != null)
		{
			vector = this.source.position;
		}
		ParticleSystem component = global::UnityEngine.Object.Instantiate<GameObject>(this.prefab, vector, Quaternion.identity).GetComponent<ParticleSystem>();
		component.main.maxParticles = Mathf.RoundToInt((float)amount / component.GetComponent<ParticlePickup>().rewardPerPickup);
	}

	private static readonly int throwConfettiEmote = Animator.StringToHash("E_ThrowConfetti");

	private static readonly int throwConfettiUpperbodyEmote = Animator.StringToHash("E_ThrowConfettiUpperbody");

	public int amount;

	public DialogueActor actor;

	public bool forceUpperbodyOnly;

	public Transform source;

	public GameObject prefab;
}
