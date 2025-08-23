using System;
using UnityEngine;

public class ShowActiveQuestProfile : MonoBehaviour
{
	// Token: 0x060001D8 RID: 472 RVA: 0x0001DF3C File Offset: 0x0001C13C
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

	// Token: 0x060001D9 RID: 473 RVA: 0x0001DF7C File Offset: 0x0001C17C
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
