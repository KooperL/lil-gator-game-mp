using System;
using UnityEngine.Events;

// Token: 0x020001B4 RID: 436
public class TimedBreakables : TimedChallenge
{
	// Token: 0x06000822 RID: 2082 RVA: 0x00036380 File Offset: 0x00034580
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

	// Token: 0x06000823 RID: 2083 RVA: 0x000363E0 File Offset: 0x000345E0
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

	// Token: 0x04000ACF RID: 2767
	public int unbrokenAllowance;

	// Token: 0x04000AD0 RID: 2768
	public BreakableObject[] requiredBreakables;
}
