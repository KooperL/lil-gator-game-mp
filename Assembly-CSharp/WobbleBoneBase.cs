using System;
using UnityEngine;

// Token: 0x02000261 RID: 609
public class WobbleBoneBase : MonoBehaviour
{
	// Token: 0x06000D1E RID: 3358 RVA: 0x0003F89A File Offset: 0x0003DA9A
	public virtual void Initialize()
	{
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x0003F89C File Offset: 0x0003DA9C
	public virtual void RunWobbleUpdate()
	{
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x0003F89E File Offset: 0x0003DA9E
	protected float Extrapolate(float t)
	{
		return t;
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x0003F8A1 File Offset: 0x0003DAA1
	public virtual void ApplyPosition()
	{
	}

	// Token: 0x06000D22 RID: 3362 RVA: 0x0003F8A3 File Offset: 0x0003DAA3
	public virtual void ApplyPosition(float t)
	{
	}

	// Token: 0x06000D23 RID: 3363 RVA: 0x0003F8A5 File Offset: 0x0003DAA5
	public virtual void ApplyRotation()
	{
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x0003F8A7 File Offset: 0x0003DAA7
	public virtual void ApplyRotation(float t)
	{
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0003F8A9 File Offset: 0x0003DAA9
	public virtual void Reacclimate()
	{
	}

	// Token: 0x0400114D RID: 4429
	public Vector3 position;

	// Token: 0x0400114E RID: 4430
	public Quaternion rotation;

	// Token: 0x0400114F RID: 4431
	[HideInInspector]
	public Vector3 forces;

	// Token: 0x04001150 RID: 4432
	[HideInInspector]
	public Vector3 interpolatedPosition;

	// Token: 0x04001151 RID: 4433
	[HideInInspector]
	public Vector3 interpolatedDirection;

	// Token: 0x04001152 RID: 4434
	[HideInInspector]
	public Quaternion interpolatedRotation;
}
