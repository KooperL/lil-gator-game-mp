using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Dialogue Sequence/Choose Event")]
public class DSChooseEvent : DialogueSequence
{
	// Token: 0x06000594 RID: 1428 RVA: 0x0002F094 File Offset: 0x0002D294
	public override YieldInstruction Run()
	{
		UnityEvent unityEvent = this.choices[DialogueManager.optionChosen];
		if (unityEvent != null)
		{
			unityEvent.Invoke();
		}
		return null;
	}

	public UnityEvent[] choices;
}
