using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000367 RID: 871
public class QuestTrackerPopup : MonoBehaviour
{
	// Token: 0x060010B1 RID: 4273 RVA: 0x0005565C File Offset: 0x0005385C
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

	// Token: 0x060010B2 RID: 4274 RVA: 0x0000E544 File Offset: 0x0000C744
	private void OnEnable()
	{
		QuestTrackerPopup.q = this;
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x0000E54C File Offset: 0x0000C74C
	private void OnDisable()
	{
		this == null;
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x000556AC File Offset: 0x000538AC
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

	// Token: 0x060010B5 RID: 4277 RVA: 0x00055750 File Offset: 0x00053950
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
				if (gameObject2.TryGetComponent<QuestTrackerTask>(ref questTrackerTask))
				{
					questTrackerTask.Load(questTask);
				}
			}
		}
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0000E556 File Offset: 0x0000C756
	private bool CanDisplay
	{
		get
		{
			return Game.State == GameState.Play || this.isShowingActiveQuest;
		}
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x000558AC File Offset: 0x00053AAC
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

	// Token: 0x040015B6 RID: 5558
	public static QuestTrackerPopup q;

	// Token: 0x040015B7 RID: 5559
	public UIHideBehavior hideBehavior;

	// Token: 0x040015B8 RID: 5560
	public Text questTitle;

	// Token: 0x040015B9 RID: 5561
	public Text questTask;

	// Token: 0x040015BA RID: 5562
	private Queue<QuestProfile> profileQueue = new Queue<QuestProfile>();

	// Token: 0x040015BB RID: 5563
	private QuestProfile displayedProfile;

	// Token: 0x040015BC RID: 5564
	public Transform taskParent;

	// Token: 0x040015BD RID: 5565
	public GameObject visibleTask;

	// Token: 0x040015BE RID: 5566
	public GameObject completedTask;

	// Token: 0x040015BF RID: 5567
	private List<GameObject> displayedTasks;

	// Token: 0x040015C0 RID: 5568
	public float completePopupTime;

	// Token: 0x040015C1 RID: 5569
	public float incompletePopupTime;

	// Token: 0x040015C2 RID: 5570
	private float instantiateTime = -1f;

	// Token: 0x040015C3 RID: 5571
	public bool isShowingActiveQuest;
}
