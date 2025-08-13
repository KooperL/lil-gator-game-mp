using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200015D RID: 349
public class ForkEventByChoice : MonoBehaviour
{
	// Token: 0x06000746 RID: 1862 RVA: 0x000244DE File Offset: 0x000226DE
	public void Fork()
	{
		if (DialogueManager.optionChosen >= 0 && DialogueManager.optionChosen < this.choices.Length)
		{
			this.choices[DialogueManager.optionChosen].Execute();
		}
	}

	// Token: 0x04000987 RID: 2439
	public ForkEventByChoice.ChoiceEvent[] choices;

	// Token: 0x020003C1 RID: 961
	[Serializable]
	public struct ChoiceEvent
	{
		// Token: 0x0600197D RID: 6525 RVA: 0x0006D017 File Offset: 0x0006B217
		public void Execute()
		{
			this.onChoose.Invoke();
		}

		// Token: 0x04001BBC RID: 7100
		public UnityEvent onChoose;
	}
}
