using System;
using UnityEngine;

public class SetActorFromSelectedOption : MonoBehaviour
{
	// Token: 0x060004DC RID: 1244 RVA: 0x00005902 File Offset: 0x00003B02
	private void OnEnable()
	{
		this.UpdateSelectedOption();
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0000590A File Offset: 0x00003B0A
	private void OnDisable()
	{
		this.currentOption = -1;
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x00005913 File Offset: 0x00003B13
	private void Update()
	{
		if (this.currentOption != DialogueOptions.CurrentlySelectedIndex)
		{
			this.UpdateSelectedOption();
		}
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x00005928 File Offset: 0x00003B28
	private void UpdateSelectedOption()
	{
		this.currentOption = DialogueOptions.CurrentlySelectedIndex;
		if (this.currentOption < 0 || this.currentOption >= this.states.Length)
		{
			return;
		}
		this.actor.State = (int)this.states[this.currentOption];
	}

	public DialogueActor actor;

	public ActorState[] states;

	private int currentOption = -1;
}
