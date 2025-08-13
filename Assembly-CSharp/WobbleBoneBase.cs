using System;
using UnityEngine;

// Token: 0x02000328 RID: 808
public class WobbleBoneBase : MonoBehaviour
{
	// Token: 0x06000FCC RID: 4044 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void Initialize()
	{
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void RunWobbleUpdate()
	{
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x0000B219 File Offset: 0x00009419
	protected float Extrapolate(float t)
	{
		return t;
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void ApplyPosition()
	{
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void ApplyPosition(float t)
	{
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void ApplyRotation()
	{
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void ApplyRotation(float t)
	{
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void Reacclimate()
	{
	}

	// Token: 0x04001470 RID: 5232
	public Vector3 position;

	// Token: 0x04001471 RID: 5233
	public Quaternion rotation;

	// Token: 0x04001472 RID: 5234
	[HideInInspector]
	public Vector3 forces;

	// Token: 0x04001473 RID: 5235
	[HideInInspector]
	public Vector3 interpolatedPosition;

	// Token: 0x04001474 RID: 5236
	[HideInInspector]
	public Vector3 interpolatedDirection;

	// Token: 0x04001475 RID: 5237
	[HideInInspector]
	public Quaternion interpolatedRotation;
}
