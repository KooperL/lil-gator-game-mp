using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/LogicState - State Machines")]
public class LSStateMachines : LogicState
{
	// Token: 0x0600075D RID: 1885 RVA: 0x00024974 File Offset: 0x00022B74
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

	// Token: 0x0600075E RID: 1886 RVA: 0x000249E0 File Offset: 0x00022BE0
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

	[Space]
	public QuestStates[] stateMachines;

	public int endAllowance = 1;
}
