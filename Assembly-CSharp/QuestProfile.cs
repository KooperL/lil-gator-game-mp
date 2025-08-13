using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000065 RID: 101
[CreateAssetMenu]
public class QuestProfile : ScriptableObject
{
	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600017B RID: 379 RVA: 0x00008A9C File Offset: 0x00006C9C
	public bool IsComplete
	{
		get
		{
			if (this.tasks != null && this.tasks.Length != 0)
			{
				this.isComplete = true;
				QuestProfile.QuestTask[] array = this.tasks;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].taskState != QuestProfile.QuestTaskState.Completed)
					{
						this.isComplete = false;
					}
				}
			}
			return this.isComplete;
		}
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00008AF4 File Offset: 0x00006CF4
	private void OnValidate()
	{
		if (this.key == "" && this.questTitle != "")
		{
			this.key = this.questTitle;
		}
		for (int i = 0; i < this.tasks.Length; i++)
		{
			this.tasks[i].document = this.document;
		}
		if (!string.IsNullOrEmpty(this.key) && this.tasks != null && this.standardTasks)
		{
			for (int j = 0; j < this.tasks.Length; j++)
			{
				this.tasks[j].statedTask = string.Format("{0}_{1:0}", this.key, j);
			}
		}
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00008BB4 File Offset: 0x00006DB4
	[ContextMenu("Add entries to document")]
	public void AddEntries()
	{
		MLTextUtil.AddMLStringEntry(this.document, this.key, this.questTitle);
		this.questTitle = this.key;
		for (int i = 0; i < this.tasks.Length; i++)
		{
			MLTextUtil.AddMLStringEntry(this.document, string.Format("{0}_{1:0}", this.key, i), this.tasks[i].name);
			this.tasks[i].statedTask = string.Format("{0}_{1:0}", this.key, i);
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00008C50 File Offset: 0x00006E50
	private void OnEnable()
	{
		if (!QuestProfile.loadedQuestProfiles.Contains(this))
		{
			QuestProfile.loadedQuestProfiles.Add(this);
		}
		this.ResetTasks();
		if (GameData.g != null)
		{
			this.Load();
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00008C83 File Offset: 0x00006E83
	private void OnDisable()
	{
		if (QuestProfile.loadedQuestProfiles.Contains(this))
		{
			QuestProfile.loadedQuestProfiles.Remove(this);
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00008CA0 File Offset: 0x00006EA0
	public bool MarkTaskVisible(int taskIndex, bool autoSave = true)
	{
		if (taskIndex >= this.tasks.Length)
		{
			return false;
		}
		if (this.tasks[taskIndex].taskState != QuestProfile.QuestTaskState.NotVisible)
		{
			return false;
		}
		this.tasks[taskIndex].taskState = QuestProfile.QuestTaskState.Visible;
		this.hasChangesToDisplay = true;
		if (autoSave)
		{
			this.Save();
		}
		return true;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x00008CF4 File Offset: 0x00006EF4
	public bool MarkTaskComplete(int taskIndex, bool autoSave = true)
	{
		if (taskIndex >= this.tasks.Length)
		{
			return false;
		}
		if (this.tasks[taskIndex].taskState == QuestProfile.QuestTaskState.Completed)
		{
			return false;
		}
		this.tasks[taskIndex].taskState = QuestProfile.QuestTaskState.Completed;
		this.hasChangesToDisplay = true;
		bool flag = true;
		QuestProfile.QuestTask[] array = this.tasks;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].taskState != QuestProfile.QuestTaskState.Completed)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			this.isComplete = true;
			this.Save();
			this.onComplete.Invoke();
		}
		else if (autoSave)
		{
			this.Save();
		}
		return true;
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00008D90 File Offset: 0x00006F90
	public void MarkCompleted()
	{
		bool flag = this.isComplete;
		this.isComplete = true;
		if (this.tasks != null && this.tasks.Length != 0)
		{
			flag = true;
			for (int i = 0; i < this.tasks.Length; i++)
			{
				if (this.tasks[i].taskState != QuestProfile.QuestTaskState.Completed)
				{
					flag = false;
					this.tasks[i].taskState = QuestProfile.QuestTaskState.Completed;
				}
			}
		}
		if (!flag)
		{
			this.Save();
			this.onComplete.Invoke();
		}
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00008E0D File Offset: 0x0000700D
	public string GetTitle()
	{
		if (this.document == null)
		{
			return this.questTitle;
		}
		return this.document.FetchString(this.questTitle, Language.Auto);
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00008E38 File Offset: 0x00007038
	public string GetActiveTask()
	{
		QuestProfile.QuestTask[] array = this.tasks;
		int i = 0;
		while (i < array.Length)
		{
			QuestProfile.QuestTask questTask = array[i];
			if (questTask.taskState == QuestProfile.QuestTaskState.Visible)
			{
				if (this.document == null)
				{
					return questTask.statedTask;
				}
				return this.document.FetchString(questTask.statedTask, Language.Auto);
			}
			else
			{
				i++;
			}
		}
		return "";
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00008E9C File Offset: 0x0000709C
	public void ResetTasks()
	{
		this.lastDisplayTime = -100f;
		this.lastZoneTrigger = -100f;
		this.isActiveQuestZone = false;
		for (int i = 0; i < this.tasks.Length; i++)
		{
			this.tasks[i].taskState = QuestProfile.QuestTaskState.NotVisible;
		}
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00008EEC File Offset: 0x000070EC
	[ContextMenu("Load")]
	public void Load()
	{
		if (GameData.g == null)
		{
			return;
		}
		if (this.tasks == null || this.tasks.Length == 0)
		{
			this.isComplete = GameData.g.ReadBool(this.id, false);
			return;
		}
		int num = GameData.g.ReadInt(this.id, 0);
		int num2 = 0;
		using (IEnumerator<int> enumerator = QuestProfile.GetDigits(num).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int num3 = enumerator.Current;
				if (num2 < this.tasks.Length)
				{
					this.tasks[num2].taskState = (QuestProfile.QuestTaskState)num3;
					num2++;
				}
			}
			goto IL_00A9;
		}
		IL_0093:
		this.tasks[num2].taskState = QuestProfile.QuestTaskState.NotVisible;
		num2++;
		IL_00A9:
		if (num2 >= this.tasks.Length)
		{
			return;
		}
		goto IL_0093;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00008FC0 File Offset: 0x000071C0
	[ContextMenu("Save")]
	public void Save()
	{
		if (GameData.g == null)
		{
			return;
		}
		if (this.tasks == null || this.tasks.Length == 0)
		{
			GameData.g.Write(this.id, this.isComplete);
			return;
		}
		int num = 0;
		for (int i = 0; i < this.tasks.Length; i++)
		{
			num = (int)(num + this.tasks[i].taskState * (QuestProfile.QuestTaskState)Mathf.RoundToInt(Mathf.Pow(10f, (float)i)));
		}
		GameData.g.Write(this.id, num);
		if (this.hasChangesToDisplay && QuestTrackerPopup.q != null && this.tasks != null && this.tasks.Length != 0)
		{
			this.hasChangesToDisplay = false;
			QuestTrackerPopup.q.QuestUpdated(this);
		}
	}

	// Token: 0x06000188 RID: 392 RVA: 0x00009087 File Offset: 0x00007287
	public static IEnumerable<int> GetDigits(int num)
	{
		while (num > 0)
		{
			yield return num % 10;
			num /= 10;
		}
		yield break;
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000189 RID: 393 RVA: 0x00009097 File Offset: 0x00007297
	public static QuestProfile ActiveQuestProfile
	{
		get
		{
			QuestProfile.UpdateActiveQuest(false);
			return QuestProfile.activeQuest;
		}
	}

	// Token: 0x0600018A RID: 394 RVA: 0x000090A4 File Offset: 0x000072A4
	public static void UpdateActiveQuest(bool added)
	{
		int num = -1;
		QuestProfile questProfile = null;
		for (int i = 0; i < QuestProfile.activeQuestProfiles.Count; i++)
		{
			if (Time.realtimeSinceStartup - QuestProfile.activeQuestProfiles[i].lastZoneTrigger > 5f)
			{
				QuestProfile.activeQuestProfiles[i].isActiveQuestZone = false;
				QuestProfile.activeQuestProfiles.RemoveAt(i);
				i--;
			}
			else if (QuestProfile.activeQuestProfiles[i].priority > num && QuestProfile.activeQuestProfiles[i].ShouldShowQuest)
			{
				questProfile = QuestProfile.activeQuestProfiles[i];
				num = questProfile.priority;
			}
		}
		if (QuestProfile.activeQuest != questProfile)
		{
			QuestProfile.activeQuest = questProfile;
			if (added && QuestProfile.activeQuest != null)
			{
				QuestTrackerPopup.q.QuestUpdated(QuestProfile.activeQuest);
			}
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00009178 File Offset: 0x00007378
	public void QuestZoneTriggered()
	{
		if (this.isActiveQuestZone)
		{
			this.lastZoneTrigger = Time.realtimeSinceStartup;
			if (QuestProfile.activeQuest == null && Time.timeSinceLevelLoad < 5f)
			{
				QuestProfile.UpdateActiveQuest(true);
				return;
			}
		}
		else
		{
			this.isActiveQuestZone = true;
			bool flag = this.showWhenEnteringZone && (Time.timeSinceLevelLoad < 10f || (Time.realtimeSinceStartup - this.lastZoneTrigger > 20f && Time.realtimeSinceStartup - this.lastDisplayTime > 20f));
			this.lastZoneTrigger = Time.realtimeSinceStartup;
			QuestProfile.activeQuestProfiles.Add(this);
			QuestProfile.UpdateActiveQuest(flag);
		}
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000921D File Offset: 0x0000741D
	public void WasDisplayed()
	{
		this.lastDisplayTime = Time.realtimeSinceStartup;
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x0600018D RID: 397 RVA: 0x0000922A File Offset: 0x0000742A
	public bool ShouldShowQuest
	{
		get
		{
			return this.isVisible && this.tasks[0].taskState != QuestProfile.QuestTaskState.NotVisible && !this.IsComplete;
		}
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00009254 File Offset: 0x00007454
	[ContextMenu("Debug best quest")]
	public void DebugBestQuest()
	{
		string text = QuestProfile.activeQuestProfiles.Count.ToString();
		QuestProfile activeQuestProfile = QuestProfile.ActiveQuestProfile;
		Debug.Log(text + ((activeQuestProfile != null) ? activeQuestProfile.ToString() : null));
	}

	// Token: 0x04000224 RID: 548
	public static List<QuestProfile> loadedQuestProfiles = new List<QuestProfile>();

	// Token: 0x04000225 RID: 549
	public string id;

	// Token: 0x04000226 RID: 550
	public UnityEvent onComplete = new UnityEvent();

	// Token: 0x04000227 RID: 551
	public bool displayOnComplete = true;

	// Token: 0x04000228 RID: 552
	public bool isComplete;

	// Token: 0x04000229 RID: 553
	public MultilingualTextDocument document;

	// Token: 0x0400022A RID: 554
	public string key;

	// Token: 0x0400022B RID: 555
	[TextLookup("document")]
	public string questTitle;

	// Token: 0x0400022C RID: 556
	public bool standardTasks = true;

	// Token: 0x0400022D RID: 557
	public QuestProfile.QuestTask[] tasks;

	// Token: 0x0400022E RID: 558
	public bool isVisible = true;

	// Token: 0x0400022F RID: 559
	private bool hasChangesToDisplay;

	// Token: 0x04000230 RID: 560
	private static List<QuestProfile> activeQuestProfiles = new List<QuestProfile>();

	// Token: 0x04000231 RID: 561
	private static QuestProfile activeQuest;

	// Token: 0x04000232 RID: 562
	[Header("Quest Zone")]
	public int priority;

	// Token: 0x04000233 RID: 563
	public bool showWhenEnteringZone = true;

	// Token: 0x04000234 RID: 564
	private float lastZoneTrigger = -1000f;

	// Token: 0x04000235 RID: 565
	private const float zoneTriggerDelay = 20f;

	// Token: 0x04000236 RID: 566
	private bool isActiveQuestZone;

	// Token: 0x04000237 RID: 567
	private float lastDisplayTime = 100f;

	// Token: 0x0200036D RID: 877
	public enum QuestTaskState
	{
		// Token: 0x04001A51 RID: 6737
		NotVisible,
		// Token: 0x04001A52 RID: 6738
		Visible,
		// Token: 0x04001A53 RID: 6739
		Completed
	}

	// Token: 0x0200036E RID: 878
	[Serializable]
	public struct QuestTask
	{
		// Token: 0x0600182C RID: 6188 RVA: 0x0006754E File Offset: 0x0006574E
		public string GetTaskText()
		{
			if (this.document == null)
			{
				return this.name;
			}
			return this.document.FetchString(this.statedTask, Language.Auto);
		}

		// Token: 0x04001A54 RID: 6740
		[HideInInspector]
		public MultilingualTextDocument document;

		// Token: 0x04001A55 RID: 6741
		public string name;

		// Token: 0x04001A56 RID: 6742
		[TextLookup("document")]
		public string statedTask;

		// Token: 0x04001A57 RID: 6743
		public QuestProfile.QuestTaskState taskState;
	}
}
