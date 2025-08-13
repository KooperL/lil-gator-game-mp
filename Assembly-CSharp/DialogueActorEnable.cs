using System;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class DialogueActorEnable : MonoBehaviour
{
	// Token: 0x060003E7 RID: 999 RVA: 0x00004E7E File Offset: 0x0000307E
	private void OnEnable()
	{
		if (this.dialogueActor != null)
		{
			this.dialogueActor.enabled = true;
		}
	}

	// Token: 0x0400058F RID: 1423
	public DialogueActor dialogueActor;
}
