using System;
using UnityEngine;

public class DialogueActorEnable : MonoBehaviour
{
	// Token: 0x0600040E RID: 1038 RVA: 0x000050B1 File Offset: 0x000032B1
	private void OnEnable()
	{
		if (this.dialogueActor != null)
		{
			this.dialogueActor.enabled = true;
		}
	}

	public DialogueActor dialogueActor;
}
