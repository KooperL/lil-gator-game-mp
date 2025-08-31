using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Dialogue Sequence/Choose Event")]
public class DSChooseEvent : DialogueSequence
{
	// Token: 0x06000472 RID: 1138 RVA: 0x00018F34 File Offset: 0x00017134
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
