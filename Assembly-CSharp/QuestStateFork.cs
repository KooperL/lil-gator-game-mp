using System;
using UnityEngine;
using UnityEngine.Events;

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

	public QuestStates stateReference;

	public UnityEvent[] forkedEvents;
}
