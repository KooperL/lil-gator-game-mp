using System;
using UnityEngine;

// Token: 0x020001D9 RID: 473
[AddComponentMenu("Logic/Fork Interaction By State")]
public class LogicForkInteraction : LogicFork, Interaction
{
	// Token: 0x060008C1 RID: 2241 RVA: 0x00038328 File Offset: 0x00036528
	protected override void OnValidate()
	{
		DialogueActor dialogueActor;
		if ((this.actors == null || this.actors.Length == 0) && base.TryGetComponent<DialogueActor>(ref dialogueActor))
		{
			this.actors = new DialogueActor[] { dialogueActor };
		}
		base.OnValidate();
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x00008873 File Offset: 0x00006A73
	public void Interact()
	{
		base.Action();
	}
}
