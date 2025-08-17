using System;
using UnityEngine.Events;

public class TimedBreakables : TimedChallenge
{
	// Token: 0x06000862 RID: 2146 RVA: 0x00037CE8 File Offset: 0x00035EE8
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

	// Token: 0x06000863 RID: 2147 RVA: 0x00037D48 File Offset: 0x00035F48
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

	public int unbrokenAllowance;

	public BreakableObject[] requiredBreakables;
}
