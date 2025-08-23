using System;
using UnityEngine;
using UnityEngine.Events;

public class ForkEventByChoice : MonoBehaviour
{
	// Token: 0x060008D1 RID: 2257 RVA: 0x00008A03 File Offset: 0x00006C03
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
		// Token: 0x060008D3 RID: 2259 RVA: 0x00008A31 File Offset: 0x00006C31
		public void Execute()
		{
			this.onChoose.Invoke();
		}

		public UnityEvent onChoose;
	}
}
