using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SyncQuestStates : MonoBehaviour
{
	// Token: 0x06000199 RID: 409 RVA: 0x000093C8 File Offset: 0x000075C8
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

	// Token: 0x0600019A RID: 410 RVA: 0x00009457 File Offset: 0x00007657
	public virtual void OnEnable()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.UpdateState));
		if (GameData.g != null)
		{
			this.UpdateState(this.stateMachine.StateID);
		}
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00009493 File Offset: 0x00007693
	private void OnDisable()
	{
		if (this.stateMachine != null)
		{
			this.stateMachine.onStateChange.RemoveListener(new UnityAction<int>(this.UpdateState));
		}
	}

	// Token: 0x0600019C RID: 412 RVA: 0x000094C0 File Offset: 0x000076C0
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

	// Token: 0x0600019D RID: 413 RVA: 0x000095A0 File Offset: 0x000077A0
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

	// Token: 0x0600019E RID: 414 RVA: 0x00009686 File Offset: 0x00007886
	public void SetCompleted()
	{
		this.questProfile.MarkCompleted();
	}

	public QuestStates stateMachine;

	public QuestProfile questProfile;

	public SyncQuestStates.QuestStateTask[] statePairs;

	[Serializable]
	public class QuestStateTask
	{
		[HideInInspector]
		public string name;

		[StateMachineLookup("stateMachine")]
		public int stateIndex;

		[QuestTaskLookup("questProfile")]
		public int visibleTask = -1;

		[QuestTaskLookup("questProfile")]
		public int completedTask = -1;

		public bool completePrevious = true;
	}
}
