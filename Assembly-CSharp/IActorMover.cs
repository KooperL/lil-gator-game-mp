using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000A6 RID: 166
public interface IActorMover
{
	// Token: 0x0600032D RID: 813
	void SetMark(Vector3[] positions, Quaternion rotation, float speed, UnityEvent onReachMark, bool skipToStart = false, bool disableInteractionWhileMoving = true, bool playFootsteps = true);

	// Token: 0x0600032E RID: 814
	void CancelMove();
}
