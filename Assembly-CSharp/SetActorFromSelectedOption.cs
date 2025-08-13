using System;
using UnityEngine;

// Token: 0x020000F9 RID: 249
public class SetActorFromSelectedOption : MonoBehaviour
{
	// Token: 0x060004B6 RID: 1206 RVA: 0x000056DC File Offset: 0x000038DC
	private void OnEnable()
	{
		this.UpdateSelectedOption();
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x000056E4 File Offset: 0x000038E4
	private void OnDisable()
	{
		this.currentOption = -1;
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x000056ED File Offset: 0x000038ED
	private void Update()
	{
		if (this.currentOption != DialogueOptions.CurrentlySelectedIndex)
		{
			this.UpdateSelectedOption();
		}
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x00005702 File Offset: 0x00003902
	private void UpdateSelectedOption()
	{
		this.currentOption = DialogueOptions.CurrentlySelectedIndex;
		if (this.currentOption < 0 || this.currentOption >= this.states.Length)
		{
			return;
		}
		this.actor.State = (int)this.states[this.currentOption];
	}

	// Token: 0x040006A0 RID: 1696
	public DialogueActor actor;

	// Token: 0x040006A1 RID: 1697
	public ActorState[] states;

	// Token: 0x040006A2 RID: 1698
	private int currentOption = -1;
}
