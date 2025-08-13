using System;
using UnityEngine;

// Token: 0x0200026F RID: 623
[CreateAssetMenu]
public class VoiceProfile : ScriptableObject
{
	// Token: 0x040011A3 RID: 4515
	public AudioClip[] voiceClips;

	// Token: 0x040011A4 RID: 4516
	public float pitch = 1f;

	// Token: 0x040011A5 RID: 4517
	public float variance = 0.1f;
}
