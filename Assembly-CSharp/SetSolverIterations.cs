using System;
using UnityEngine;

// Token: 0x0200032F RID: 815
public class SetSolverIterations : MonoBehaviour
{
	// Token: 0x06000FF8 RID: 4088 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
	private void OnEnable()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponent<Rigidbody>();
		}
		this.rigidbody.solverIterations = this.solverIterations;
		this.rigidbody.solverVelocityIterations = this.solverVelocityIterations;
	}

	// Token: 0x040014B6 RID: 5302
	private Rigidbody rigidbody;

	// Token: 0x040014B7 RID: 5303
	public int solverIterations = 20;

	// Token: 0x040014B8 RID: 5304
	public int solverVelocityIterations = 20;
}
