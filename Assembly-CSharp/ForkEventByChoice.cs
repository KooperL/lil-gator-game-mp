using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001C7 RID: 455
public class ForkEventByChoice : MonoBehaviour
{
	// Token: 0x06000890 RID: 2192 RVA: 0x000086DC File Offset: 0x000068DC
	public void Fork()
	{
		if (DialogueManager.optionChosen >= 0 && DialogueManager.optionChosen < this.choices.Length)
		{
			this.choices[DialogueManager.optionChosen].Execute();
		}
	}

	// Token: 0x04000B27 RID: 2855
	public ForkEventByChoice.ChoiceEvent[] choices;

	// Token: 0x020001C8 RID: 456
	[Serializable]
	public struct ChoiceEvent
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x0000870A File Offset: 0x0000690A
		public void Execute()
		{
			this.onChoose.Invoke();
		}

		// Token: 0x04000B28 RID: 2856
		public UnityEvent onChoose;
	}
}
