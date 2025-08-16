using System;
using UnityEngine;

public interface ICustomFootIKPositions
{
	Vector3 GetLeftFootTarget(Vector3 currentPosition);

	Vector3 GetRightFootTarget(Vector3 currentPosition);
}
