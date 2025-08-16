using System;
using UnityEngine;
using UnityEngine.Events;

public class SetActorByState : MonoBehaviour
{
	// Token: 0x0600091F RID: 2335 RVA: 0x00008D26 File Offset: 0x00006F26
	private void OnValidate()
	{
		if (this.stateMachine == null)
		{
			this.stateMachine = base.GetComponentInParent<QuestStates>();
		}
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x00008D42 File Offset: 0x00006F42
	private void Awake()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.UpdateState));
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x00008D60 File Offset: 0x00006F60
	private void Start()
	{
		this.UpdateState(this.stateMachine.StateID);
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x00002229 File Offset: 0x00000429
	private void UpdateState(int newState)
	{
	}

	public QuestStates stateMachine;
}
