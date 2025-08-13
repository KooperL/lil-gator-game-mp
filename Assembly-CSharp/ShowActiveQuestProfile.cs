using System;
using UnityEngine;

// Token: 0x02000087 RID: 135
public class ShowActiveQuestProfile : MonoBehaviour
{
	// Token: 0x060001CB RID: 459 RVA: 0x0001D4E4 File Offset: 0x0001B6E4
	private void OnEnable()
	{
		if (QuestTrackerPopup.q == null)
		{
			return;
		}
		QuestTrackerPopup.q.isShowingActiveQuest = true;
		QuestProfile activeQuestProfile = QuestProfile.ActiveQuestProfile;
		if (activeQuestProfile != null)
		{
			QuestTrackerPopup.q.QuestUpdated(activeQuestProfile);
		}
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0001D524 File Offset: 0x0001B724
	private void OnDisable()
	{
		if (QuestTrackerPopup.q == null)
		{
			return;
		}
		QuestTrackerPopup.q.isShowingActiveQuest = false;
		if (!QuestTrackerPopup.q.hideBehavior.isHiding)
		{
			QuestTrackerPopup.q.hideBehavior.enabled = true;
			QuestTrackerPopup.q.hideBehavior.autoHideTime = Time.time;
		}
	}
}
