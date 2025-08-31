using System;
using UnityEngine;

public class SetSolverIterations : MonoBehaviour
{
	// Token: 0x06000D4A RID: 3402 RVA: 0x000406BE File Offset: 0x0003E8BE
	private void OnEnable()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
		}
		this.rigidbody.solverIterations = this.solverIterations;
		this.rigidbody.solverVelocityIterations = this.solverVelocityIterations;
	}

	private Rigidbody rigidbody;

	public int solverIterations = 20;

	public int solverVelocityIterations = 20;
}
