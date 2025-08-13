using System;
using UnityEngine;

// Token: 0x020002CC RID: 716
public class QuestRewardConfetti : QuestReward
{
	// Token: 0x06000E05 RID: 3589 RVA: 0x0000C84E File Offset: 0x0000AA4E
	public override void GiveReward()
	{
		this.GiveReward(this.amount);
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x0004B54C File Offset: 0x0004974C
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

	// Token: 0x04001237 RID: 4663
	private static readonly int throwConfettiEmote = Animator.StringToHash("E_ThrowConfetti");

	// Token: 0x04001238 RID: 4664
	private static readonly int throwConfettiUpperbodyEmote = Animator.StringToHash("E_ThrowConfettiUpperbody");

	// Token: 0x04001239 RID: 4665
	public int amount;

	// Token: 0x0400123A RID: 4666
	public DialogueActor actor;

	// Token: 0x0400123B RID: 4667
	public bool forceUpperbodyOnly;

	// Token: 0x0400123C RID: 4668
	public Transform source;

	// Token: 0x0400123D RID: 4669
	public GameObject prefab;
}
