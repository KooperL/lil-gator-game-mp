using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000088 RID: 136
public class SyncQuestStates : MonoBehaviour
{
	// Token: 0x060001CE RID: 462 RVA: 0x0001D580 File Offset: 0x0001B780
	private void OnValidate()
	{
		if (this.stateMachine == null)
		{
			this.stateMachine = base.GetComponent<QuestStates>();
		}
		if (this.statePairs != null)
		{
			for (int i = 0; i < this.statePairs.Length; i++)
			{
				this.statePairs[i].name = string.Format("{0} / {1} / {2}", this.statePairs[i].stateIndex, this.statePairs[i].visibleTask, this.statePairs[i].completedTask);
			}
		}
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00003759 File Offset: 0x00001959
	public virtual void OnEnable()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.UpdateState));
		if (GameData.g != null)
		{
			this.UpdateState(this.stateMachine.StateID);
		}
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00003795 File Offset: 0x00001995
	private void OnDisable()
	{
		if (this.stateMachine != null)
		{
			this.stateMachine.onStateChange.RemoveListener(new UnityAction<int>(this.UpdateState));
		}
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0001D610 File Offset: 0x0001B810
	[ContextMenu("Auto Populate")]
	public void AutoPopulate()
	{
		int num = this.statePairs.Length - 1;
		int num2;
		int num3;
		int num4;
		if (num == -1)
		{
			num2 = 0;
			num3 = 0;
			num4 = -1;
		}
		else
		{
			SyncQuestStates.QuestStateTask questStateTask = this.statePairs[num];
			num2 = questStateTask.stateIndex + 1;
			num3 = questStateTask.visibleTask + 1;
			num4 = questStateTask.completedTask + 1;
		}
		List<SyncQuestStates.QuestStateTask> list = new List<SyncQuestStates.QuestStateTask>(this.statePairs);
		while (num2 < this.stateMachine.states.Length && num4 < this.questProfile.tasks.Length)
		{
			list.Add(new SyncQuestStates.QuestStateTask
			{
				stateIndex = num2,
				visibleTask = ((num3 < this.questProfile.tasks.Length) ? num3 : (-1)),
				completedTask = ((num4 < this.questProfile.tasks.Length && num4 >= 0) ? num4 : (-1))
			});
			num2++;
			num3++;
			num4++;
		}
		this.statePairs = list.ToArray();
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0001D6F0 File Offset: 0x0001B8F0
	public void UpdateState(int newStateIndex)
	{
		bool flag = false;
		foreach (SyncQuestStates.QuestStateTask questStateTask in this.statePairs)
		{
			if (newStateIndex == questStateTask.stateIndex)
			{
				flag = true;
				if (questStateTask.visibleTask != -1)
				{
					this.questProfile.MarkTaskVisible(questStateTask.visibleTask, false);
				}
				if (questStateTask.completedTask != -1)
				{
					this.questProfile.MarkTaskComplete(questStateTask.completedTask, false);
				}
				if (questStateTask.completePrevious)
				{
					if (questStateTask.visibleTask != -1)
					{
						for (int j = 0; j < questStateTask.visibleTask; j++)
						{
							this.questProfile.MarkTaskComplete(j, false);
						}
					}
					else if (questStateTask.completedTask != -1)
					{
						for (int k = 0; k <= questStateTask.completedTask; k++)
						{
							this.questProfile.MarkTaskComplete(k, false);
						}
					}
				}
			}
		}
		if (flag)
		{
			this.questProfile.Save();
		}
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x000037C1 File Offset: 0x000019C1
	public void SetCompleted()
	{
		this.questProfile.MarkCompleted();
	}

	// Token: 0x040002B9 RID: 697
	public QuestStates stateMachine;

	// Token: 0x040002BA RID: 698
	public QuestProfile questProfile;

	// Token: 0x040002BB RID: 699
	public SyncQuestStates.QuestStateTask[] statePairs;

	// Token: 0x02000089 RID: 137
	[Serializable]
	public class QuestStateTask
	{
		// Token: 0x040002BC RID: 700
		[HideInInspector]
		public string name;

		// Token: 0x040002BD RID: 701
		[StateMachineLookup("stateMachine")]
		public int stateIndex;

		// Token: 0x040002BE RID: 702
		[QuestTaskLookup("questProfile")]
		public int visibleTask = -1;

		// Token: 0x040002BF RID: 703
		[QuestTaskLookup("questProfile")]
		public int completedTask = -1;

		// Token: 0x040002C0 RID: 704
		public bool completePrevious = true;
	}
}
