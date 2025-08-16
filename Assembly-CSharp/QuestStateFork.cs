using System;
using UnityEngine;
using UnityEngine.Events;

public class QuestStateFork : MonoBehaviour
{
	// Token: 0x06000E5F RID: 3679 RVA: 0x0004D0D0 File Offset: 0x0004B2D0
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
