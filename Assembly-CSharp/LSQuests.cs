using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001D3 RID: 467
[AddComponentMenu("Logic/LogicState - Quests")]
public class LSQuests : LogicState
{
	// Token: 0x060008AB RID: 2219 RVA: 0x00037A24 File Offset: 0x00035C24
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

	// Token: 0x060008AC RID: 2220 RVA: 0x00037A6C File Offset: 0x00035C6C
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

	// Token: 0x060008AD RID: 2221 RVA: 0x00037AB8 File Offset: 0x00035CB8
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

	// Token: 0x04000B43 RID: 2883
	[Space]
	public QuestProfile[] quests;

	// Token: 0x04000B44 RID: 2884
	public bool requireAll = true;

	// Token: 0x04000B45 RID: 2885
	[ConditionalHide("requireAll", true, Inverse = true)]
	public int requiredCount;

	// Token: 0x04000B46 RID: 2886
	private bool addedListeners;
}
