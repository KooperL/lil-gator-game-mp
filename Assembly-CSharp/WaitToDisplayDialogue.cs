using System;
using System.Collections.Generic;
using UnityEngine;

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

	public static List<WaitToDisplayDialogue> allWaiting = new List<WaitToDisplayDialogue>();

	public int priority;

	public DialogueSequencer sequencer;
}
