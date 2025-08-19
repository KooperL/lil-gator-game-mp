using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrackerPopup : MonoBehaviour
{
	// Token: 0x0600110C RID: 4364 RVA: 0x000575AC File Offset: 0x000557AC
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

	// Token: 0x0600110D RID: 4365 RVA: 0x0000E8B7 File Offset: 0x0000CAB7
	private void OnEnable()
	{
		QuestTrackerPopup.q = this;
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x0000E8BF File Offset: 0x0000CABF
	private void OnDisable()
	{
		this == null;
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x000575FC File Offset: 0x000557FC
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

	// Token: 0x06001110 RID: 4368 RVA: 0x000576A0 File Offset: 0x000558A0
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
			global::UnityEngine.Object.Destroy(gameObject);
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
				gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(gameObject2, this.taskParent);
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

	// (get) Token: 0x06001111 RID: 4369 RVA: 0x0000E8C9 File Offset: 0x0000CAC9
	private bool CanDisplay
	{
		get
		{
			return Game.State == GameState.Play || this.isShowingActiveQuest;
		}
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x000577FC File Offset: 0x000559FC
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

	public static QuestTrackerPopup q;

	public UIHideBehavior hideBehavior;

	public Text questTitle;

	public Text questTask;

	private Queue<QuestProfile> profileQueue = new Queue<QuestProfile>();

	private QuestProfile displayedProfile;

	public Transform taskParent;

	public GameObject visibleTask;

	public GameObject completedTask;

	private List<GameObject> displayedTasks;

	public float completePopupTime;

	public float incompletePopupTime;

	private float instantiateTime = -1f;

	public bool isShowingActiveQuest;
}
