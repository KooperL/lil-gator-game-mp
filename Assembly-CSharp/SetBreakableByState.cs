using System;
using UnityEngine;
using UnityEngine.Events;

public class SetBreakableByState : MonoBehaviour
{
	// Token: 0x0600104E RID: 4174 RVA: 0x0000DFCA File Offset: 0x0000C1CA
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

	// Token: 0x0600104F RID: 4175 RVA: 0x00054C20 File Offset: 0x00052E20
	private void OnEnable()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.OnStateChange));
		this.breakableObject.isInvincible = ((this.stateMachine.StateID >= this.thresholdState) ? this.isInvincibleAfter : this.isInvincibleBefore);
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x0000E000 File Offset: 0x0000C200
	private void OnDisable()
	{
		this.stateMachine.onStateChange.RemoveListener(new UnityAction<int>(this.OnStateChange));
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x0000E01E File Offset: 0x0000C21E
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
