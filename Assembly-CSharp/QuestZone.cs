using System;
using UnityEngine;

// Token: 0x02000086 RID: 134
public class QuestZone : MonoBehaviour
{
	// Token: 0x060001C9 RID: 457 RVA: 0x0000374C File Offset: 0x0000194C
	private void OnTriggerStay(Collider other)
	{
		this.questProfile.QuestZoneTriggered();
	}

	// Token: 0x040002B8 RID: 696
	public QuestProfile questProfile;
}
