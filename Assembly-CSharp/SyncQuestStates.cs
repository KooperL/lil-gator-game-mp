using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SyncQuestStates : MonoBehaviour
{
	// Token: 0x060001DB RID: 475 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
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

	// Token: 0x060001DC RID: 476 RVA: 0x00003845 File Offset: 0x00001A45
	public virtual void OnEnable()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.UpdateState));
		if (GameData.g != null)
		{
			this.UpdateState(this.stateMachine.StateID);
		}
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00003881 File Offset: 0x00001A81
	private void OnDisable()
	{
		if (this.stateMachine != null)
		{
			this.stateMachine.onStateChange.RemoveListener(new UnityAction<int>(this.UpdateState));
		}
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0001E068 File Offset: 0x0001C268
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

	// Token: 0x060001DF RID: 479 RVA: 0x0001E148 File Offset: 0x0001C348
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

	// Token: 0x060001E0 RID: 480 RVA: 0x000038AD File Offset: 0x00001AAD
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
