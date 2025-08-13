using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200025F RID: 607
public class WaitToDisplayDialogue : MonoBehaviour
{
	// Token: 0x06000D09 RID: 3337 RVA: 0x0003EC73 File Offset: 0x0003CE73
	private void Update()
	{
		if (this.CanRunDialogue())
		{
			this.sequencer.JustStartSequence();
		}
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x0003EC88 File Offset: 0x0003CE88
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

	// Token: 0x0400111C RID: 4380
	public static List<WaitToDisplayDialogue> allWaiting = new List<WaitToDisplayDialogue>();

	// Token: 0x0400111D RID: 4381
	public int priority;

	// Token: 0x0400111E RID: 4382
	public DialogueSequencer sequencer;
}
