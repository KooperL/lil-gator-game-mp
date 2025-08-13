using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020001F8 RID: 504
public class NavMeshGoal : MonoBehaviour
{
	// Token: 0x06000955 RID: 2389 RVA: 0x0000917C File Offset: 0x0000737C
	private void Awake()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x0000918A File Offset: 0x0000738A
	private void FixedUpdate()
	{
		this.agent.SetDestination(this.goal.position);
	}

	// Token: 0x04000C01 RID: 3073
	public Transform goal;

	// Token: 0x04000C02 RID: 3074
	private NavMeshAgent agent;
}
