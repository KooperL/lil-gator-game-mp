using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Logic/LogicState - Quests")]
public class LSQuests : LogicState
{
	// Token: 0x06000759 RID: 1881 RVA: 0x00024840 File Offset: 0x00022A40
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

	// Token: 0x0600075A RID: 1882 RVA: 0x00024888 File Offset: 0x00022A88
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

	// Token: 0x0600075B RID: 1883 RVA: 0x000248D4 File Offset: 0x00022AD4
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
