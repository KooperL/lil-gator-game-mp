using System;
using UnityEngine;

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

	public MusicSystem musicSystem;

	public float delayFromEnable = 14.76923f;
}
