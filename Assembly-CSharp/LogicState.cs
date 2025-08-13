using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001DA RID: 474
[RequireComponent(typeof(QuestStates))]
public class LogicState : MonoBehaviour
{
	// Token: 0x060008C4 RID: 2244 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void OnValidate()
	{
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x00008883 File Offset: 0x00006A83
	private void Awake()
	{
		this.stateMachine.onStateChange.AddListener(new UnityAction<int>(this.UpdateState));
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x000088A1 File Offset: 0x00006AA1
	public virtual void Start()
	{
		this.UpdateState(this.stateMachine.StateID);
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x00038368 File Offset: 0x00036568
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

	// Token: 0x060008C8 RID: 2248 RVA: 0x000088B4 File Offset: 0x00006AB4
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

	// Token: 0x060008C9 RID: 2249 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void CheckLogic()
	{
	}

	// Token: 0x04000B65 RID: 2917
	public QuestStates stateMachine;

	// Token: 0x04000B66 RID: 2918
	public string description;

	// Token: 0x04000B67 RID: 2919
	public int stateIndex;

	// Token: 0x04000B68 RID: 2920
	public int[] additionalStates;

	// Token: 0x04000B69 RID: 2921
	[ReadOnly]
	public string stateName;

	// Token: 0x04000B6A RID: 2922
	[Space]
	public bool progressState = true;

	// Token: 0x04000B6B RID: 2923
	[ConditionalHide("progressState", true)]
	public bool overrideNewState;

	// Token: 0x04000B6C RID: 2924
	[ConditionalHide("overrideNewState", true, ConditionalSourceField2 = "progressState")]
	public int newState;
}
