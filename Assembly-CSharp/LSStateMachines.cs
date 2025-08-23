using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/LogicState - State Machines")]
public class LSStateMachines : LogicState
{
	// Token: 0x060008F0 RID: 2288 RVA: 0x00039780 File Offset: 0x00037980
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

	// Token: 0x060008F1 RID: 2289 RVA: 0x000397EC File Offset: 0x000379EC
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
