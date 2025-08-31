using System;
using UnityEngine;

public class SetActorFromSelectedOption : MonoBehaviour
{
	// Token: 0x06000402 RID: 1026 RVA: 0x000177DB File Offset: 0x000159DB
	private void OnEnable()
	{
		this.UpdateSelectedOption();
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x000177E3 File Offset: 0x000159E3
	private void OnDisable()
	{
		this.currentOption = -1;
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x000177EC File Offset: 0x000159EC
	private void Update()
	{
		if (this.currentOption != DialogueOptions.CurrentlySelectedIndex)
		{
			this.UpdateSelectedOption();
		}
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00017801 File Offset: 0x00015A01
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
