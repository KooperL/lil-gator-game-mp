using System;
using UnityEngine;

public class QuestRewardConfetti : QuestReward
{
	// Token: 0x06000BB0 RID: 2992 RVA: 0x00038C27 File Offset: 0x00036E27
	public override void GiveReward()
	{
		this.GiveReward(this.amount);
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x00038C38 File Offset: 0x00036E38
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
		ParticleSystem component = Object.Instantiate<GameObject>(this.prefab, vector, Quaternion.identity).GetComponent<ParticleSystem>();
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
