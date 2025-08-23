using System;
using System.Collections.Generic;
using UnityEngine;

public class WaitToDisplayDialogue : MonoBehaviour
{
	// Token: 0x06001013 RID: 4115 RVA: 0x0000DE27 File Offset: 0x0000C027
	private void Update()
	{
		if (this.CanRunDialogue())
		{
			this.sequencer.JustStartSequence();
		}
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x000537BC File Offset: 0x000519BC
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

	public static List<WaitToDisplayDialogue> allWaiting = new List<WaitToDisplayDialogue>();

	public int priority;

	public DialogueSequencer sequencer;
}
