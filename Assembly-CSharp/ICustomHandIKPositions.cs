using System;
using UnityEngine;

public interface ICustomHandIKPositions
{
	Vector3 GetLeftHandTarget(Vector3 currentPosition);

	Vector3 GetRightHandTarget(Vector3 currentPosition);
}
