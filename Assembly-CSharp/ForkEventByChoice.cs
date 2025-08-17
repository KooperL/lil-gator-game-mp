using System;
using UnityEngine;
using UnityEngine.Events;

public class ForkEventByChoice : MonoBehaviour
{
	// Token: 0x060008D0 RID: 2256 RVA: 0x000089F9 File Offset: 0x00006BF9
	public void Fork()
	{
		if (DialogueManager.optionChosen >= 0 && DialogueManager.optionChosen < this.choices.Length)
		{
			this.choices[DialogueManager.optionChosen].Execute();
		}
	}

	public ForkEventByChoice.ChoiceEvent[] choices;

	[Serializable]
	public struct ChoiceEvent
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x00008A27 File Offset: 0x00006C27
		public void Execute()
		{
			this.onChoose.Invoke();
		}

		public UnityEvent onChoose;
	}
}
