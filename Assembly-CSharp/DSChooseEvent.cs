using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000CF RID: 207
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

	// Token: 0x0400063C RID: 1596
	public UnityEvent[] choices;
}
