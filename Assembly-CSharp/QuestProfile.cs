using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class QuestProfile : ScriptableObject
{
	// (get) Token: 0x060001B4 RID: 436 RVA: 0x0001D58C File Offset: 0x0001B78C
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

	// Token: 0x060001B5 RID: 437 RVA: 0x0001D5E4 File Offset: 0x0001B7E4
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

	// Token: 0x060001B6 RID: 438 RVA: 0x0001D6A4 File Offset: 0x0001B8A4
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

	// Token: 0x060001B7 RID: 439 RVA: 0x000036EA File Offset: 0x000018EA
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

	// Token: 0x060001B8 RID: 440 RVA: 0x0000371D File Offset: 0x0000191D
	private void OnDisable()
	{
		if (QuestProfile.loadedQuestProfiles.Contains(this))
		{
			QuestProfile.loadedQuestProfiles.Remove(this);
		}
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x0001D740 File Offset: 0x0001B940
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

	// Token: 0x060001BA RID: 442 RVA: 0x0001D794 File Offset: 0x0001B994
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

	// Token: 0x060001BB RID: 443 RVA: 0x0001D830 File Offset: 0x0001BA30
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

	// Token: 0x060001BC RID: 444 RVA: 0x00003738 File Offset: 0x00001938
	public string GetTitle()
	{
		if (this.document == null)
		{
			return this.questTitle;
		}
		return this.document.FetchString(this.questTitle, Language.Auto);
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0001D8B0 File Offset: 0x0001BAB0
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

	// Token: 0x060001BE RID: 446 RVA: 0x0001D914 File Offset: 0x0001BB14
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

	// Token: 0x060001BF RID: 447 RVA: 0x0001D964 File Offset: 0x0001BB64
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

	// Token: 0x060001C0 RID: 448 RVA: 0x0001DA38 File Offset: 0x0001BC38
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

	// Token: 0x060001C1 RID: 449 RVA: 0x00003762 File Offset: 0x00001962
	public static IEnumerable<int> GetDigits(int num)
	{
		while (num > 0)
		{
			yield return num % 10;
			num /= 10;
		}
		yield break;
	}

	// (get) Token: 0x060001C2 RID: 450 RVA: 0x00003772 File Offset: 0x00001972
	public static QuestProfile ActiveQuestProfile
	{
		get
		{
			QuestProfile.UpdateActiveQuest(false);
			return QuestProfile.activeQuest;
		}
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0001DB00 File Offset: 0x0001BD00
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

	// Token: 0x060001C4 RID: 452 RVA: 0x0001DBD4 File Offset: 0x0001BDD4
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

	// Token: 0x060001C5 RID: 453 RVA: 0x0000377F File Offset: 0x0000197F
	public void WasDisplayed()
	{
		this.lastDisplayTime = Time.realtimeSinceStartup;
	}

	// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000378C File Offset: 0x0000198C
	public bool ShouldShowQuest
	{
		get
		{
			return this.isVisible && this.tasks[0].taskState != QuestProfile.QuestTaskState.NotVisible && !this.IsComplete;
		}
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0001DC7C File Offset: 0x0001BE7C
	[ContextMenu("Debug best quest")]
	public void DebugBestQuest()
	{
		string text = QuestProfile.activeQuestProfiles.Count.ToString();
		QuestProfile activeQuestProfile = QuestProfile.ActiveQuestProfile;
		Debug.Log(text + ((activeQuestProfile != null) ? activeQuestProfile.ToString() : null));
	}

	public static List<QuestProfile> loadedQuestProfiles = new List<QuestProfile>();

	public string id;

	public UnityEvent onComplete = new UnityEvent();

	public bool displayOnComplete = true;

	public bool isComplete;

	public MultilingualTextDocument document;

	public string key;

	[TextLookup("document")]
	public string questTitle;

	public bool standardTasks = true;

	public QuestProfile.QuestTask[] tasks;

	public bool isVisible = true;

	private bool hasChangesToDisplay;

	private static List<QuestProfile> activeQuestProfiles = new List<QuestProfile>();

	private static QuestProfile activeQuest;

	[Header("Quest Zone")]
	public int priority;

	public bool showWhenEnteringZone = true;

	private float lastZoneTrigger = -1000f;

	private const float zoneTriggerDelay = 20f;

	private bool isActiveQuestZone;

	private float lastDisplayTime = 100f;

	public enum QuestTaskState
	{
		NotVisible,
		Visible,
		Completed
	}

	[Serializable]
	public struct QuestTask
	{
		// Token: 0x060001CA RID: 458 RVA: 0x000037CA File Offset: 0x000019CA
		public string GetTaskText()
		{
			if (this.document == null)
			{
				return this.name;
			}
			return this.document.FetchString(this.statedTask, Language.Auto);
		}

		[HideInInspector]
		public MultilingualTextDocument document;

		public string name;

		[TextLookup("document")]
		public string statedTask;

		public QuestProfile.QuestTaskState taskState;
	}
}
