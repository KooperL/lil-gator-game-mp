using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000119 RID: 281
[AddComponentMenu("Dialogue Sequence/Choose Event")]
public class DSChooseEvent : DialogueSequence
{
	// Token: 0x0600055A RID: 1370 RVA: 0x0002D998 File Offset: 0x0002BB98
	public override YieldInstruction Run()
	{
		UnityEvent unityEvent = this.choices[DialogueManager.optionChosen];
		if (unityEvent != null)
		{
			unityEvent.Invoke();
		}
		return null;
	}

	// Token: 0x0400075E RID: 1886
	public UnityEvent[] choices;
}
