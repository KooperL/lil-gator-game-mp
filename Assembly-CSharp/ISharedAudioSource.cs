using System;
using UnityEngine;

public interface ISharedAudioSource
{
	void GetAudioData(Vector3 positionReference, out SharedAudioProfile profile, out Vector3 direction, out float strength);

	void WasRemoved();
}
