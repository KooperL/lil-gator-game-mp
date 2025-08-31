using System;
using UnityEngine;
using UnityEngine.AI;

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

	public Transform goal;

	private NavMeshAgent agent;
}
