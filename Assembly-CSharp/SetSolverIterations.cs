using System;
using UnityEngine;

public class SetSolverIterations : MonoBehaviour
{
	// Token: 0x06001053 RID: 4179 RVA: 0x0000E03C File Offset: 0x0000C23C
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
