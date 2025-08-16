using System;
using UnityEngine;

[CreateAssetMenu]
public class VoiceProfile : ScriptableObject
{
	public AudioClip[] voiceClips;

	public float pitch = 1f;

	public float variance = 0.1f;
}
