using System;
using UnityEngine;
using UnityEngine.Events;

public class SetActorByState : MonoBehaviour
{
	// Token: 0x06000780 RID: 1920 RVA: 0x0002528B File Offset: 0x0002348B
	private void OnValidate()
	{
		if (this.stateMachine == null)
		{
			this.stateMachine = base.GetComponentInParent<QuestStates>();
		}
	}

	// Token: 0x06000781 RID: 1921 RVA: 0x000252A7 File Offset: 0x000234A7
	private void Awake()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.UpdateState));
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x000252C5 File Offset: 0x000234C5
	private void Start()
	{
		this.UpdateState(this.stateMachine.StateID);
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x000252D8 File Offset: 0x000234D8
	private void UpdateState(int newState)
	{
	}

	public QuestStates stateMachine;
}
