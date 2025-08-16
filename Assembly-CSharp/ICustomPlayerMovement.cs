using System;
using UnityEngine;

public interface ICustomPlayerMovement
{
	void MovementUpdate(Vector3 input, ref Vector3 position, ref Vector3 velocity, ref Vector3 direction, ref Vector3 up, ref float animationIndex);

	void Cancel();
}
