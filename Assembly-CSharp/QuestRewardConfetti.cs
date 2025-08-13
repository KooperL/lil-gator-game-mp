using System;
using UnityEngine;

// Token: 0x0200021D RID: 541
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

	// Token: 0x04000F74 RID: 3956
	private static readonly int throwConfettiEmote = Animator.StringToHash("E_ThrowConfetti");

	// Token: 0x04000F75 RID: 3957
	private static readonly int throwConfettiUpperbodyEmote = Animator.StringToHash("E_ThrowConfettiUpperbody");

	// Token: 0x04000F76 RID: 3958
	public int amount;

	// Token: 0x04000F77 RID: 3959
	public DialogueActor actor;

	// Token: 0x04000F78 RID: 3960
	public bool forceUpperbodyOnly;

	// Token: 0x04000F79 RID: 3961
	public Transform source;

	// Token: 0x04000F7A RID: 3962
	public GameObject prefab;
}
