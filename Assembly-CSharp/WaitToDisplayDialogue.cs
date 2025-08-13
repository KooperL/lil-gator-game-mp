using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000326 RID: 806
public class WaitToDisplayDialogue : MonoBehaviour
{
	// Token: 0x06000FB7 RID: 4023 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
	private void Update()
	{
		if (this.CanRunDialogue())
		{
			this.sequencer.JustStartSequence();
		}
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x000515D0 File Offset: 0x0004F7D0
	private bool CanRunDialogue()
	{
		if (DialogueManager.d.IsInImportantDialogue)
		{
			return false;
		}
		if (Game.State != GameState.Play)
		{
			return false;
		}
		foreach (WaitToDisplayDialogue waitToDisplayDialogue in WaitToDisplayDialogue.allWaiting)
		{
			if (waitToDisplayDialogue != this && waitToDisplayDialogue.priority > this.priority)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0400143F RID: 5183
	public static List<WaitToDisplayDialogue> allWaiting = new List<WaitToDisplayDialogue>();

	// Token: 0x04001440 RID: 5184
	public int priority;

	// Token: 0x04001441 RID: 5185
	public DialogueSequencer sequencer;
}
