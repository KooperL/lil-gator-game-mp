using System;
using UnityEngine;

public class CueMusicForSpecificTime : MonoBehaviour
{
	// Token: 0x0600005F RID: 95 RVA: 0x00002485 File Offset: 0x00000685
	private void OnEnable()
	{
		if (this.musicSystem != null)
		{
			this.musicSystem.CueStartPlaying(this.delayFromEnable);
			this.musicSystem.isLocked = true;
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x000024B2 File Offset: 0x000006B2
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
