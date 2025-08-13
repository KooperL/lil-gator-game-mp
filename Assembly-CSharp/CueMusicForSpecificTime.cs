using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class CueMusicForSpecificTime : MonoBehaviour
{
	// Token: 0x0600005E RID: 94 RVA: 0x0000367B File Offset: 0x0000187B
	private void OnEnable()
	{
		if (this.musicSystem != null)
		{
			this.musicSystem.CueStartPlaying(this.delayFromEnable);
			this.musicSystem.isLocked = true;
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x000036A8 File Offset: 0x000018A8
	private void OnDisable()
	{
		if (this.musicSystem != null)
		{
			this.musicSystem.isLocked = false;
		}
	}

	// Token: 0x04000086 RID: 134
	public MusicSystem musicSystem;

	// Token: 0x04000087 RID: 135
	public float delayFromEnable = 14.76923f;
}
