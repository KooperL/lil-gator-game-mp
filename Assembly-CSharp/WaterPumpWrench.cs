using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class WaterPumpWrench : MonoBehaviour, Interaction
{
	// Token: 0x06000119 RID: 281 RVA: 0x00006DCC File Offset: 0x00004FCC
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

	// Token: 0x0600011A RID: 282 RVA: 0x00006E61 File Offset: 0x00005061
	public void OnHit()
	{
		if (!this.waterPump.activated && ItemManager.i.GetPrimaryID() == "Sword_Wrench")
		{
			this.waterPump.Activate();
		}
	}

	// Token: 0x04000189 RID: 393
	public WaterPump waterPump;

	// Token: 0x0400018A RID: 394
	public DialogueActor[] actors;
}
