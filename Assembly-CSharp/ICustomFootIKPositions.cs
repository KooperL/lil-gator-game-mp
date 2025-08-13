using System;
using UnityEngine;

// Token: 0x02000119 RID: 281
public interface ICustomFootIKPositions
{
	// Token: 0x060005CD RID: 1485
	Vector3 GetLeftFootTarget(Vector3 currentPosition);

	// Token: 0x060005CE RID: 1486
	Vector3 GetRightFootTarget(Vector3 currentPosition);
}
