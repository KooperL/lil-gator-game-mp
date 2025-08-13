using System;
using UnityEngine;

// Token: 0x02000171 RID: 369
public interface ICustomFootIKPositions
{
	// Token: 0x060006F3 RID: 1779
	Vector3 GetLeftFootTarget(Vector3 currentPosition);

	// Token: 0x060006F4 RID: 1780
	Vector3 GetRightFootTarget(Vector3 currentPosition);
}
