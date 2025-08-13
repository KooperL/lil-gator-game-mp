using System;
using UnityEngine;

// Token: 0x02000085 RID: 133
public class QuestProfileHandler : MonoBehaviour
{
	// Token: 0x060001C6 RID: 454 RVA: 0x0000373F File Offset: 0x0000193F
	[ContextMenu("Set Complete")]
	public void SetComplete()
	{
		this.profile.MarkCompleted();
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00002229 File Offset: 0x00000429
	public void SetStarted()
	{
	}

	// Token: 0x040002B7 RID: 695
	public QuestProfile profile;
}
