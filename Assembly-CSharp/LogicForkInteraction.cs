using System;
using UnityEngine;

[AddComponentMenu("Logic/Fork Interaction By State")]
public class LogicForkInteraction : LogicFork, Interaction
{
	// Token: 0x06000901 RID: 2305 RVA: 0x00039C98 File Offset: 0x00037E98
	protected override void OnValidate()
	{
		DialogueActor dialogueActor;
		if ((this.actors == null || this.actors.Length == 0) && base.TryGetComponent<DialogueActor>(out dialogueActor))
		{
			this.actors = new DialogueActor[] { dialogueActor };
		}
		base.OnValidate();
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00008B9C File Offset: 0x00006D9C
	public void Interact()
	{
		base.Action();
	}
}
