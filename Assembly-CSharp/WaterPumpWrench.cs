using System;
using UnityEngine;

public class WaterPumpWrench : MonoBehaviour, Interaction
{
	// Token: 0x06000146 RID: 326 RVA: 0x0001BCAC File Offset: 0x00019EAC
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

	// Token: 0x06000147 RID: 327 RVA: 0x0000320D File Offset: 0x0000140D
	public void OnHit()
	{
		if (!this.waterPump.activated && ItemManager.i.GetPrimaryID() == "Sword_Wrench")
		{
			this.waterPump.Activate();
		}
	}

	public WaterPump waterPump;

	public DialogueActor[] actors;
}
