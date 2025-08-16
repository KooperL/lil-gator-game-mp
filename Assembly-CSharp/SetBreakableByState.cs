using System;
using UnityEngine;
using UnityEngine.Events;

public class SetBreakableByState : MonoBehaviour
{
	// Token: 0x0600104E RID: 4174 RVA: 0x0000DFB5 File Offset: 0x0000C1B5
	public void OnValidate()
	{
		if (this.breakableObject == null)
		{
			this.breakableObject = base.GetComponent<BreakableObject>();
		}
		if (this.stateMachine == null)
		{
			this.stateMachine = base.GetComponentInParent<QuestStates>();
		}
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x00054A8C File Offset: 0x00052C8C
	private void OnEnable()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.OnStateChange));
		this.breakableObject.isInvincible = ((this.stateMachine.StateID >= this.thresholdState) ? this.isInvincibleAfter : this.isInvincibleBefore);
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x0000DFEB File Offset: 0x0000C1EB
	private void OnDisable()
	{
		this.stateMachine.onStateChange.RemoveListener(new UnityAction<int>(this.OnStateChange));
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x0000E009 File Offset: 0x0000C209
	private void OnStateChange(int newState)
	{
		this.breakableObject.isInvincible = ((newState >= this.thresholdState) ? this.isInvincibleAfter : this.isInvincibleBefore);
	}

	public QuestStates stateMachine;

	public BreakableObject breakableObject;

	public int thresholdState;

	public bool isInvincibleBefore = true;

	public bool isInvincibleAfter;
}
