using System;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class DialogueActorEnable : MonoBehaviour
{
	// Token: 0x06000388 RID: 904 RVA: 0x00014EFC File Offset: 0x000130FC
	private void OnEnable()
	{
		if (this.dialogueActor != null)
		{
			this.dialogueActor.enabled = true;
		}
	}

	// Token: 0x040004E2 RID: 1250
	public DialogueActor dialogueActor;
}
