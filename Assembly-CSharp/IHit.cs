using System;
using UnityEngine;

public interface IHit
{
	void Hit(Vector3 velocity, bool isHeavy = false);
}
