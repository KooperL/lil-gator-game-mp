using System;
using UnityEngine;

public class WobbleBoneBase : MonoBehaviour
{
	// Token: 0x06001027 RID: 4135 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void Initialize()
	{
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void RunWobbleUpdate()
	{
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x0000B50C File Offset: 0x0000970C
	protected float Extrapolate(float t)
	{
		return t;
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void ApplyPosition()
	{
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void ApplyPosition(float t)
	{
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void ApplyRotation()
	{
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void ApplyRotation(float t)
	{
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void Reacclimate()
	{
	}

	public Vector3 position;

	public Quaternion rotation;

	[HideInInspector]
	public Vector3 forces;

	[HideInInspector]
	public Vector3 interpolatedPosition;

	[HideInInspector]
	public Vector3 interpolatedDirection;

	[HideInInspector]
	public Quaternion interpolatedRotation;
}
