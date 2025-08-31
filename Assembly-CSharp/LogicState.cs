using System;
using UnityEngine;
using UnityEngine.Events;

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

	public QuestStates stateMachine;

	public string description;

	public int stateIndex;

	public int[] additionalStates;

	[ReadOnly]
	public string stateName;

	[Space]
	public bool progressState = true;

	[ConditionalHide("progressState", true)]
	public bool overrideNewState;

	[ConditionalHide("overrideNewState", true, ConditionalSourceField2 = "progressState")]
	public int newState;
}
