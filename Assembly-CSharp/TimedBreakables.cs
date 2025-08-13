using System;
using UnityEngine.Events;

// Token: 0x02000150 RID: 336
public class TimedBreakables : TimedChallenge
{
	// Token: 0x060006E4 RID: 1764 RVA: 0x00022D60 File Offset: 0x00020F60
	protected override void Start()
	{
		base.Start();
		if (this.requiredBreakables == null || this.requiredBreakables.Length == 0)
		{
			this.requiredBreakables = this.timedBreakables;
		}
		BreakableObject[] array = this.requiredBreakables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].onBreak.AddListener(new UnityAction(this.OnBreakableBroken));
		}
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00022DC0 File Offset: 0x00020FC0
	private void OnBreakableBroken()
	{
		int num = 0;
		int num2 = 0;
		BreakableObject[] array = this.requiredBreakables;
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].isBroken)
			{
				num++;
			}
			else
			{
				num2++;
			}
		}
		if (num <= this.unbrokenAllowance)
		{
			PlayAudio.p.PlayQuestEndSting();
			base.FinishRace();
			return;
		}
		PlayAudio.p.PlayQuestSting(num2);
	}

	// Token: 0x04000947 RID: 2375
	public int unbrokenAllowance;

	// Token: 0x04000948 RID: 2376
	public BreakableObject[] requiredBreakables;
}
