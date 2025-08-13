using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000D0 RID: 208
public interface IActorMover
{
	// Token: 0x06000372 RID: 882
	void SetMark(Vector3[] positions, Quaternion rotation, float speed, UnityEvent onReachMark, bool skipToStart = false, bool disableInteractionWhileMoving = true, bool playFootsteps = true);

	// Token: 0x06000373 RID: 883
	void CancelMove();
}
