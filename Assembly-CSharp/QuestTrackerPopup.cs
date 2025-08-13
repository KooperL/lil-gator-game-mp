using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200028D RID: 653
public class QuestTrackerPopup : MonoBehaviour
{
	// Token: 0x06000DE8 RID: 3560 RVA: 0x00043548 File Offset: 0x00041748
	private void Awake()
	{
		this.instantiateTime = Time.time;
		if (this.displayedTasks == null)
		{
			this.displayedTasks = new List<GameObject>();
		}
		if (this.profileQueue == null)
		{
			this.profileQueue = new Queue<QuestProfile>();
		}
		QuestTrackerPopup.q = this;
		this.hideBehavior.Hide();
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x00043597 File Offset: 0x00041797
	private void OnEnable()
	{
		QuestTrackerPopup.q = this;
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x0004359F File Offset: 0x0004179F
	private void OnDisable()
	{
		this == null;
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x000435AC File Offset: 0x000417AC
	public void QuestUpdated(QuestProfile profile)
	{
		if (profile == null || profile.tasks == null || profile.tasks.Length == 0 || Time.time - 1f < this.instantiateTime)
		{
			return;
		}
		if (profile.IsComplete && !profile.displayOnComplete)
		{
			if (profile == this.displayedProfile)
			{
				this.hideBehavior.Hide();
			}
			return;
		}
		if (profile.isVisible)
		{
			base.enabled = true;
			if (this.displayedProfile == profile)
			{
				this.DisplayQuest(profile);
				return;
			}
			if (!this.profileQueue.Contains(profile))
			{
				this.profileQueue.Enqueue(profile);
			}
		}
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x00043650 File Offset: 0x00041850
	private void DisplayQuest(QuestProfile profile)
	{
		profile.WasDisplayed();
		this.displayedProfile = profile;
		this.hideBehavior.Show();
		if (this.profileQueue.Count == 1 && this.isShowingActiveQuest)
		{
			this.hideBehavior.autoHideTime = -1f;
		}
		else
		{
			this.hideBehavior.autoHideTime = Time.time + (profile.IsComplete ? this.completePopupTime : this.incompletePopupTime);
		}
		this.questTitle.text = profile.GetTitle();
		foreach (GameObject gameObject in this.displayedTasks)
		{
			Object.Destroy(gameObject);
		}
		this.displayedTasks = new List<GameObject>();
		foreach (QuestProfile.QuestTask questTask in profile.tasks)
		{
			GameObject gameObject2 = null;
			QuestProfile.QuestTaskState taskState = questTask.taskState;
			if (taskState != QuestProfile.QuestTaskState.Visible)
			{
				if (taskState == QuestProfile.QuestTaskState.Completed)
				{
					gameObject2 = this.completedTask;
				}
			}
			else
			{
				gameObject2 = this.visibleTask;
			}
			if (gameObject2 != null)
			{
				gameObject2 = Object.Instantiate<GameObject>(gameObject2, this.taskParent);
				gameObject2.SetActive(true);
				this.displayedTasks.Add(gameObject2);
				QuestTrackerTask questTrackerTask;
				if (gameObject2.TryGetComponent<QuestTrackerTask>(out questTrackerTask))
				{
					questTrackerTask.Load(questTask);
				}
			}
		}
	}

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06000DED RID: 3565 RVA: 0x000437AC File Offset: 0x000419AC
	private bool CanDisplay
	{
		get
		{
			return Game.State == GameState.Play || this.isShowingActiveQuest;
		}
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x000437C0 File Offset: 0x000419C0
	private void Update()
	{
		if (!this.hideBehavior.gameObject.activeSelf && this.profileQueue.Count == 0)
		{
			this.displayedProfile = null;
			base.enabled = false;
		}
		if (this.profileQueue.Count > 0 && this.CanDisplay && !this.hideBehavior.gameObject.activeSelf)
		{
			if (this.displayedProfile == this.profileQueue.Peek() || (this.profileQueue.Peek().IsComplete && !this.profileQueue.Peek().displayOnComplete))
			{
				this.profileQueue.Dequeue();
			}
			this.displayedProfile = null;
			if (this.profileQueue.Count > 0)
			{
				this.DisplayQuest(this.profileQueue.Peek());
			}
		}
		if (!this.hideBehavior.isHiding && !this.CanDisplay)
		{
			this.hideBehavior.Hide();
			this.displayedProfile = null;
			this.profileQueue.Clear();
		}
	}

	// Token: 0x04001256 RID: 4694
	public static QuestTrackerPopup q;

	// Token: 0x04001257 RID: 4695
	public UIHideBehavior hideBehavior;

	// Token: 0x04001258 RID: 4696
	public Text questTitle;

	// Token: 0x04001259 RID: 4697
	public Text questTask;

	// Token: 0x0400125A RID: 4698
	private Queue<QuestProfile> profileQueue = new Queue<QuestProfile>();

	// Token: 0x0400125B RID: 4699
	private QuestProfile displayedProfile;

	// Token: 0x0400125C RID: 4700
	public Transform taskParent;

	// Token: 0x0400125D RID: 4701
	public GameObject visibleTask;

	// Token: 0x0400125E RID: 4702
	public GameObject completedTask;

	// Token: 0x0400125F RID: 4703
	private List<GameObject> displayedTasks;

	// Token: 0x04001260 RID: 4704
	public float completePopupTime;

	// Token: 0x04001261 RID: 4705
	public float incompletePopupTime;

	// Token: 0x04001262 RID: 4706
	private float instantiateTime = -1f;

	// Token: 0x04001263 RID: 4707
	public bool isShowingActiveQuest;
}
