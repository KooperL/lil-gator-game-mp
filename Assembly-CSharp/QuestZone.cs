using System;
using UnityEngine;

public class QuestZone : MonoBehaviour
{
	// Token: 0x06000194 RID: 404 RVA: 0x0000930D File Offset: 0x0000750D
	private void OnTriggerStay(Collider other)
	{
		this.questProfile.QuestZoneTriggered();
	}

	public QuestProfile questProfile;
}
