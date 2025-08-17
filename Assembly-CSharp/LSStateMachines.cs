using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/LogicState - State Machines")]
public class LSStateMachines : LogicState
{
	// Token: 0x060008EF RID: 2287 RVA: 0x000394B8 File Offset: 0x000376B8
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

	// Token: 0x060008F0 RID: 2288 RVA: 0x00039524 File Offset: 0x00037724
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
