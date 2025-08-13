using System;
using UnityEngine;

// Token: 0x02000267 RID: 615
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

	// Token: 0x04001190 RID: 4496
	private Rigidbody rigidbody;

	// Token: 0x04001191 RID: 4497
	public int solverIterations = 20;

	// Token: 0x04001192 RID: 4498
	public int solverVelocityIterations = 20;
}
