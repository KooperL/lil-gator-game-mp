using System;
using UnityEngine;
using UnityEngine.Events;

public interface IActorMover
{
	void SetMark(Vector3[] positions, Quaternion rotation, float speed, UnityEvent onReachMark, bool skipToStart = false, bool disableInteractionWhileMoving = true, bool playFootsteps = true);

	void CancelMove();
}
