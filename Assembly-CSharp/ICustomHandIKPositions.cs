using System;
using UnityEngine;

// Token: 0x0200022D RID: 557
public interface ICustomHandIKPositions
{
	// Token: 0x06000A7D RID: 2685
	Vector3 GetLeftHandTarget(Vector3 currentPosition);

	// Token: 0x06000A7E RID: 2686
	Vector3 GetRightHandTarget(Vector3 currentPosition);
}
