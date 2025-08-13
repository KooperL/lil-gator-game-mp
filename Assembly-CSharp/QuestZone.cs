using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class QuestZone : MonoBehaviour
{
	// Token: 0x06000194 RID: 404 RVA: 0x0000930D File Offset: 0x0000750D
	private void OnTriggerStay(Collider other)
	{
		this.questProfile.QuestZoneTriggered();
	}

	// Token: 0x04000239 RID: 569
	public QuestProfile questProfile;
}
