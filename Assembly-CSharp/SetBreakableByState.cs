using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200032E RID: 814
public class SetBreakableByState : MonoBehaviour
{
	// Token: 0x06000FF3 RID: 4083 RVA: 0x0000DC61 File Offset: 0x0000BE61
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

	// Token: 0x06000FF4 RID: 4084 RVA: 0x00052CFC File Offset: 0x00050EFC
	private void OnEnable()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.OnStateChange));
		this.breakableObject.isInvincible = ((this.stateMachine.StateID >= this.thresholdState) ? this.isInvincibleAfter : this.isInvincibleBefore);
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x0000DC97 File Offset: 0x0000BE97
	private void OnDisable()
	{
		this.stateMachine.onStateChange.RemoveListener(new UnityAction<int>(this.OnStateChange));
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x0000DCB5 File Offset: 0x0000BEB5
	private void OnStateChange(int newState)
	{
		this.breakableObject.isInvincible = ((newState >= this.thresholdState) ? this.isInvincibleAfter : this.isInvincibleBefore);
	}

	// Token: 0x040014B1 RID: 5297
	public QuestStates stateMachine;

	// Token: 0x040014B2 RID: 5298
	public BreakableObject breakableObject;

	// Token: 0x040014B3 RID: 5299
	public int thresholdState;

	// Token: 0x040014B4 RID: 5300
	public bool isInvincibleBefore = true;

	// Token: 0x040014B5 RID: 5301
	public bool isInvincibleAfter;
}
