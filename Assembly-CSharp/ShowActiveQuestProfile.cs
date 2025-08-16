using System;
using UnityEngine;

public class ShowActiveQuestProfile : MonoBehaviour
{
	// Token: 0x060001D8 RID: 472 RVA: 0x0001DDAC File Offset: 0x0001BFAC
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

	// Token: 0x060001D9 RID: 473 RVA: 0x0001DDEC File Offset: 0x0001BFEC
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
