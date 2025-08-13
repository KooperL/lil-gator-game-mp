using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020002D2 RID: 722
public class QuestStateFork : MonoBehaviour
{
	// Token: 0x06000E13 RID: 3603 RVA: 0x0004B6DC File Offset: 0x000498DC
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

	// Token: 0x04001247 RID: 4679
	public QuestStates stateReference;

	// Token: 0x04001248 RID: 4680
	public UnityEvent[] forkedEvents;
}
