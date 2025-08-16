using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGoal : MonoBehaviour
{
	// Token: 0x06000996 RID: 2454 RVA: 0x00009498 File Offset: 0x00007698
	private void Awake()
	{
		this.agent = base.GetComponent<NavMeshAgent>();
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x000094A6 File Offset: 0x000076A6
	private void FixedUpdate()
	{
		this.agent.SetDestination(this.goal.position);
	}

	public Transform goal;

	private NavMeshAgent agent;
}
