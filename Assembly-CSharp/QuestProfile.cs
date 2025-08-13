using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000081 RID: 129
[CreateAssetMenu]
public class QuestProfile : ScriptableObject
{
	// Token: 0x1700001E RID: 30
	// (get) Token: 0x060001A7 RID: 423 RVA: 0x0001CCC8 File Offset: 0x0001AEC8
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

	// Token: 0x060001A8 RID: 424 RVA: 0x0001CD20 File Offset: 0x0001AF20
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

	// Token: 0x060001A9 RID: 425 RVA: 0x0001CDE0 File Offset: 0x0001AFE0
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

	// Token: 0x060001AA RID: 426 RVA: 0x00003600 File Offset: 0x00001800
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

	// Token: 0x060001AB RID: 427 RVA: 0x00003633 File Offset: 0x00001833
	private void OnDisable()
	{
		if (QuestProfile.loadedQuestProfiles.Contains(this))
		{
			QuestProfile.loadedQuestProfiles.Remove(this);
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0001CE7C File Offset: 0x0001B07C
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

	// Token: 0x060001AD RID: 429 RVA: 0x0001CED0 File Offset: 0x0001B0D0
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

	// Token: 0x060001AE RID: 430 RVA: 0x0001CF6C File Offset: 0x0001B16C
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

	// Token: 0x060001AF RID: 431 RVA: 0x0000364E File Offset: 0x0000184E
	public string GetTitle()
	{
		if (this.document == null)
		{
			return this.questTitle;
		}
		return this.document.FetchString(this.questTitle, Language.English);
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x0001CFEC File Offset: 0x0001B1EC
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
				return this.document.FetchString(questTask.statedTask, Language.English);
			}
			else
			{
				i++;
			}
		}
		return "";
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0001D04C File Offset: 0x0001B24C
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

	// Token: 0x060001B2 RID: 434 RVA: 0x0001D09C File Offset: 0x0001B29C
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

	// Token: 0x060001B3 RID: 435 RVA: 0x0001D170 File Offset: 0x0001B370
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

	// Token: 0x060001B4 RID: 436 RVA: 0x00003677 File Offset: 0x00001877
	public static IEnumerable<int> GetDigits(int num)
	{
		while (num > 0)
		{
			yield return num % 10;
			num /= 10;
		}
		yield break;
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x060001B5 RID: 437 RVA: 0x00003687 File Offset: 0x00001887
	public static QuestProfile ActiveQuestProfile
	{
		get
		{
			QuestProfile.UpdateActiveQuest(false);
			return QuestProfile.activeQuest;
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0001D238 File Offset: 0x0001B438
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

	// Token: 0x060001B7 RID: 439 RVA: 0x0001D30C File Offset: 0x0001B50C
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

	// Token: 0x060001B8 RID: 440 RVA: 0x00003694 File Offset: 0x00001894
	public void WasDisplayed()
	{
		this.lastDisplayTime = Time.realtimeSinceStartup;
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x060001B9 RID: 441 RVA: 0x000036A1 File Offset: 0x000018A1
	public bool ShouldShowQuest
	{
		get
		{
			return this.isVisible && this.tasks[0].taskState != QuestProfile.QuestTaskState.NotVisible && !this.IsComplete;
		}
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0001D3B4 File Offset: 0x0001B5B4
	[ContextMenu("Debug best quest")]
	public void DebugBestQuest()
	{
		string text = QuestProfile.activeQuestProfiles.Count.ToString();
		QuestProfile activeQuestProfile = QuestProfile.ActiveQuestProfile;
		Debug.Log(text + ((activeQuestProfile != null) ? activeQuestProfile.ToString() : null));
	}

	// Token: 0x04000296 RID: 662
	public static List<QuestProfile> loadedQuestProfiles = new List<QuestProfile>();

	// Token: 0x04000297 RID: 663
	public string id;

	// Token: 0x04000298 RID: 664
	public UnityEvent onComplete = new UnityEvent();

	// Token: 0x04000299 RID: 665
	public bool displayOnComplete = true;

	// Token: 0x0400029A RID: 666
	public bool isComplete;

	// Token: 0x0400029B RID: 667
	public MultilingualTextDocument document;

	// Token: 0x0400029C RID: 668
	public string key;

	// Token: 0x0400029D RID: 669
	[TextLookup("document")]
	public string questTitle;

	// Token: 0x0400029E RID: 670
	public bool standardTasks = true;

	// Token: 0x0400029F RID: 671
	public QuestProfile.QuestTask[] tasks;

	// Token: 0x040002A0 RID: 672
	public bool isVisible = true;

	// Token: 0x040002A1 RID: 673
	private bool hasChangesToDisplay;

	// Token: 0x040002A2 RID: 674
	private static List<QuestProfile> activeQuestProfiles = new List<QuestProfile>();

	// Token: 0x040002A3 RID: 675
	private static QuestProfile activeQuest;

	// Token: 0x040002A4 RID: 676
	[Header("Quest Zone")]
	public int priority;

	// Token: 0x040002A5 RID: 677
	public bool showWhenEnteringZone = true;

	// Token: 0x040002A6 RID: 678
	private float lastZoneTrigger = -1000f;

	// Token: 0x040002A7 RID: 679
	private const float zoneTriggerDelay = 20f;

	// Token: 0x040002A8 RID: 680
	private bool isActiveQuestZone;

	// Token: 0x040002A9 RID: 681
	private float lastDisplayTime = 100f;

	// Token: 0x02000082 RID: 130
	public enum QuestTaskState
	{
		// Token: 0x040002AB RID: 683
		NotVisible,
		// Token: 0x040002AC RID: 684
		Visible,
		// Token: 0x040002AD RID: 685
		Completed
	}

	// Token: 0x02000083 RID: 131
	[Serializable]
	public struct QuestTask
	{
		// Token: 0x060001BD RID: 445 RVA: 0x000036DF File Offset: 0x000018DF
		public string GetTaskText()
		{
			if (this.document == null)
			{
				return this.name;
			}
			return this.document.FetchString(this.statedTask, Language.English);
		}

		// Token: 0x040002AE RID: 686
		[HideInInspector]
		public MultilingualTextDocument document;

		// Token: 0x040002AF RID: 687
		public string name;

		// Token: 0x040002B0 RID: 688
		[TextLookup("document")]
		public string statedTask;

		// Token: 0x040002B1 RID: 689
		public QuestProfile.QuestTaskState taskState;
	}
}
