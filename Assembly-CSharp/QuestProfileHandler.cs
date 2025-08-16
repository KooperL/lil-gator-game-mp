using System;
using UnityEngine;

public class QuestProfileHandler : MonoBehaviour
{
	// Token: 0x060001D3 RID: 467 RVA: 0x0000382B File Offset: 0x00001A2B
	[ContextMenu("Set Complete")]
	public void SetComplete()
	{
		this.profile.MarkCompleted();
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00002229 File Offset: 0x00000429
	public void SetStarted()
	{
	}

	public QuestProfile profile;
}
