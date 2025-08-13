using System;
using UnityEngine;

// Token: 0x02000338 RID: 824
[CreateAssetMenu]
public class VoiceProfile : ScriptableObject
{
	// Token: 0x040014CC RID: 5324
	public AudioClip[] voiceClips;

	// Token: 0x040014CD RID: 5325
	public float pitch = 1f;

	// Token: 0x040014CE RID: 5326
	public float variance = 0.1f;
}
