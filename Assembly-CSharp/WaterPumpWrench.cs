using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class WaterPumpWrench : MonoBehaviour, Interaction
{
	// Token: 0x0600013E RID: 318 RVA: 0x0001B498 File Offset: 0x00019698
	public void Interact()
	{
		if (DialogueManager.d.IsInImportantDialogue)
		{
			return;
		}
		if (this.waterPump.activated)
		{
			DialogueManager.d.Bubble("Cool_Pump_Activated", this.actors, 0f, false, true, true);
			return;
		}
		if (!GameData.g.ReadBool("Sword_Wrench", false))
		{
			DialogueManager.d.Bubble("Cool_Pump_Wrench1", this.actors, 0f, false, true, true);
			return;
		}
		DialogueManager.d.Bubble("Cool_Pump_Wrench2", this.actors, 0f, false, true, true);
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0000316A File Offset: 0x0000136A
	public void OnHit()
	{
		if (!this.waterPump.activated && ItemManager.i.GetPrimaryID() == "Sword_Wrench")
		{
			this.waterPump.Activate();
		}
	}

	// Token: 0x040001D3 RID: 467
	public WaterPump waterPump;

	// Token: 0x040001D4 RID: 468
	public DialogueActor[] actors;
}
