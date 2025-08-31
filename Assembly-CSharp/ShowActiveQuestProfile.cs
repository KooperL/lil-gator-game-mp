using System;
using UnityEngine;

public class ShowActiveQuestProfile : MonoBehaviour
{
	// Token: 0x06000196 RID: 406 RVA: 0x00009324 File Offset: 0x00007524
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

	// Token: 0x06000197 RID: 407 RVA: 0x00009364 File Offset: 0x00007564
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
