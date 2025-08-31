using System;
using UnityEngine;
using UnityEngine.Events;

public class SetBreakableByState : MonoBehaviour
{
	// Token: 0x06000D45 RID: 3397 RVA: 0x000405E0 File Offset: 0x0003E7E0
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

	// Token: 0x06000D46 RID: 3398 RVA: 0x00040618 File Offset: 0x0003E818
	private void OnEnable()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.OnStateChange));
		this.breakableObject.isInvincible = ((this.stateMachine.StateID >= this.thresholdState) ? this.isInvincibleAfter : this.isInvincibleBefore);
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x0004066D File Offset: 0x0003E86D
	private void OnDisable()
	{
		this.stateMachine.onStateChange.RemoveListener(new UnityAction<int>(this.OnStateChange));
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x0004068B File Offset: 0x0003E88B
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
