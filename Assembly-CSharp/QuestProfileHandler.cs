using System;
using UnityEngine;

public class QuestProfileHandler : MonoBehaviour
{
	// Token: 0x06000191 RID: 401 RVA: 0x000092F6 File Offset: 0x000074F6
	[ContextMenu("Set Complete")]
	public void SetComplete()
	{
		this.profile.MarkCompleted();
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00009303 File Offset: 0x00007503
	public void SetStarted()
	{
	}

	public QuestProfile profile;
}
