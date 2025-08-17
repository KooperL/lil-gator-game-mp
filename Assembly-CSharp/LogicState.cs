using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(QuestStates))]
public class LogicState : MonoBehaviour
{
	// Token: 0x06000904 RID: 2308 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void OnValidate()
	{
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00008BAC File Offset: 0x00006DAC
	private void Awake()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.UpdateState));
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00008BCA File Offset: 0x00006DCA
	public virtual void Start()
	{
		this.UpdateState(this.stateMachine.StateID);
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00039CD8 File Offset: 0x00037ED8
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

	// Token: 0x06000908 RID: 2312 RVA: 0x00008BDD File Offset: 0x00006DDD
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

	// Token: 0x06000909 RID: 2313 RVA: 0x00002229 File Offset: 0x00000429
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
