using System;
using UnityEngine;
using UnityEngine.Events;

public class QuestStateFork : MonoBehaviour
{
	// Token: 0x06000E60 RID: 3680 RVA: 0x0004D52C File Offset: 0x0004B72C
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
