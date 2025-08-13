using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000223 RID: 547
public class QuestStateFork : MonoBehaviour
{
	// Token: 0x06000BBE RID: 3006 RVA: 0x00038E6C File Offset: 0x0003706C
	public void Fork()
	{
		if (this.stateReference == null)
		{
			return;
		}
		int stateID = this.stateReference.StateID;
		if (this.forkedEvents.Length <= stateID)
		{
			return;
		}
		this.forkedEvents[stateID].Invoke();
	}

	// Token: 0x04000F84 RID: 3972
	public QuestStates stateReference;

	// Token: 0x04000F85 RID: 3973
	public UnityEvent[] forkedEvents;
}
