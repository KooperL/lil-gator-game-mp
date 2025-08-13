using System;
using UnityEngine;

// Token: 0x020001B1 RID: 433
public interface ICustomHandIKPositions
{
	// Token: 0x060008F6 RID: 2294
	Vector3 GetLeftHandTarget(Vector3 currentPosition);

	// Token: 0x060008F7 RID: 2295
	Vector3 GetRightHandTarget(Vector3 currentPosition);
}
