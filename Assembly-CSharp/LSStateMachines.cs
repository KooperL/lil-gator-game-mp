using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001D4 RID: 468
[AddComponentMenu("Logic/LogicState - State Machines")]
public class LSStateMachines : LogicState
{
	// Token: 0x060008AF RID: 2223 RVA: 0x00037B48 File Offset: 0x00035D48
	public override void Start()
	{
		base.Start();
		foreach (QuestStates questStates in this.stateMachines)
		{
			for (int j = 1; j <= this.endAllowance; j++)
			{
				questStates.states[questStates.states.Length - j].onProgress.AddListener(new UnityAction(this.CheckLogic));
			}
		}
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x00037BB4 File Offset: 0x00035DB4
	public override void CheckLogic()
	{
		int num = 0;
		foreach (QuestStates questStates in this.stateMachines)
		{
			if (questStates.StateID >= questStates.states.Length - this.endAllowance)
			{
				num++;
			}
		}
		if (num == this.stateMachines.Length)
		{
			this.LogicCompleted();
		}
	}

	// Token: 0x04000B47 RID: 2887
	[Space]
	public QuestStates[] stateMachines;

	// Token: 0x04000B48 RID: 2888
	public int endAllowance = 1;
}
