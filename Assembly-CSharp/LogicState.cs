using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000169 RID: 361
[RequireComponent(typeof(QuestStates))]
public class LogicState : MonoBehaviour
{
	// Token: 0x0600076C RID: 1900 RVA: 0x00024DC2 File Offset: 0x00022FC2
	public virtual void OnValidate()
	{
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00024DC4 File Offset: 0x00022FC4
	private void Awake()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.UpdateState));
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00024DE2 File Offset: 0x00022FE2
	public virtual void Start()
	{
		this.UpdateState(this.stateMachine.StateID);
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x00024DF8 File Offset: 0x00022FF8
	public void UpdateState(int newState)
	{
		base.enabled = newState == this.stateIndex;
		if (!base.enabled && this.additionalStates.Length != 0)
		{
			base.enabled = this.additionalStates.Contains(newState);
		}
		if (base.enabled)
		{
			this.CheckLogic();
		}
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x00024E45 File Offset: 0x00023045
	public virtual void LogicCompleted()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.progressState)
		{
			if (this.overrideNewState)
			{
				this.stateMachine.ProgressState(this.newState);
				return;
			}
			this.stateMachine.JustProgressState();
		}
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x00024E7E File Offset: 0x0002307E
	public virtual void CheckLogic()
	{
	}

	// Token: 0x040009A4 RID: 2468
	public QuestStates stateMachine;

	// Token: 0x040009A5 RID: 2469
	public string description;

	// Token: 0x040009A6 RID: 2470
	public int stateIndex;

	// Token: 0x040009A7 RID: 2471
	public int[] additionalStates;

	// Token: 0x040009A8 RID: 2472
	[ReadOnly]
	public string stateName;

	// Token: 0x040009A9 RID: 2473
	[Space]
	public bool progressState = true;

	// Token: 0x040009AA RID: 2474
	[ConditionalHide("progressState", true)]
	public bool overrideNewState;

	// Token: 0x040009AB RID: 2475
	[ConditionalHide("overrideNewState", true, ConditionalSourceField2 = "progressState")]
	public int newState;
}
