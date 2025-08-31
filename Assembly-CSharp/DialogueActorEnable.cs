using System;
using UnityEngine;

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

	public DialogueActor dialogueActor;
}
