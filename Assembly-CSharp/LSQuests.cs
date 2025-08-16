using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/LogicState - Quests")]
public class LSQuests : LogicState
{
	// Token: 0x060008EB RID: 2283 RVA: 0x000391B4 File Offset: 0x000373B4
	public override void CheckLogic()
	{
		QuestProfile[] array = this.quests;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].onComplete.AddListener(new UnityAction(this.CheckQuests));
		}
		this.addedListeners = true;
		this.CheckQuests();
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x000391FC File Offset: 0x000373FC
	private void OnDestroy()
	{
		if (this.addedListeners)
		{
			QuestProfile[] array = this.quests;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].onComplete.RemoveListener(new UnityAction(this.CheckQuests));
			}
		}
		this.addedListeners = false;
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00039248 File Offset: 0x00037448
	private void CheckQuests()
	{
		bool flag = true;
		int num = 0;
		QuestProfile[] array = this.quests;
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].IsComplete)
			{
				flag = false;
			}
			else
			{
				num++;
			}
		}
		if ((this.requireAll && flag) || (!this.requireAll && num >= this.requiredCount))
		{
			array = this.quests;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].onComplete.RemoveListener(new UnityAction(this.CheckQuests));
			}
			this.addedListeners = false;
			this.LogicCompleted();
		}
	}

	[Space]
	public QuestProfile[] quests;

	public bool requireAll = true;

	[ConditionalHide("requireAll", true, Inverse = true)]
	public int requiredCount;

	private bool addedListeners;
}
