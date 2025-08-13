using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
[AddComponentMenu("Logic/Fork Interaction By State")]
public class LogicForkInteraction : LogicFork, Interaction
{
	// Token: 0x06000769 RID: 1897 RVA: 0x00024D74 File Offset: 0x00022F74
	protected override void OnValidate()
	{
		DialogueActor dialogueActor;
		if ((this.actors == null || this.actors.Length == 0) && base.TryGetComponent<DialogueActor>(out dialogueActor))
		{
			this.actors = new DialogueActor[] { dialogueActor };
		}
		base.OnValidate();
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00024DB2 File Offset: 0x00022FB2
	public void Interact()
	{
		base.Action();
	}
}
