using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000182 RID: 386
public class NavMeshGoal : MonoBehaviour
{
	// Token: 0x060007F3 RID: 2035 RVA: 0x000267B0 File Offset: 0x000249B0
	private void Awake()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x000267BE File Offset: 0x000249BE
	private void FixedUpdate()
	{
		this.agent.SetDestination(this.goal.position);
	}

	// Token: 0x04000A25 RID: 2597
	public Transform goal;

	// Token: 0x04000A26 RID: 2598
	private NavMeshAgent agent;
}
