using System;
using UnityEngine;

public class QuestZone : MonoBehaviour
{
	// Token: 0x060001D6 RID: 470 RVA: 0x00003838 File Offset: 0x00001A38
	private void OnTriggerStay(Collider other)
	{
		this.questProfile.QuestZoneTriggered();
	}

	public QuestProfile questProfile;
}
