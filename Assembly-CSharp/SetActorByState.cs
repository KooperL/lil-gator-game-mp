using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001DF RID: 479
public class SetActorByState : MonoBehaviour
{
	// Token: 0x060008DF RID: 2271 RVA: 0x00008A12 File Offset: 0x00006C12
	private void OnValidate()
	{
		if (this.stateMachine == null)
		{
			this.stateMachine = base.GetComponentInParent<QuestStates>();
		}
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00008A2E File Offset: 0x00006C2E
	private void Awake()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.UpdateState));
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x00008A4C File Offset: 0x00006C4C
	private void Start()
	{
		this.UpdateState(this.stateMachine.StateID);
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x00002229 File Offset: 0x00000429
	private void UpdateState(int newState)
	{
	}

	// Token: 0x04000B81 RID: 2945
	public QuestStates stateMachine;
}
