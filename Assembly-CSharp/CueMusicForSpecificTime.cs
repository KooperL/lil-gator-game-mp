using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class CueMusicForSpecificTime : MonoBehaviour
{
	// Token: 0x06000057 RID: 87 RVA: 0x00002421 File Offset: 0x00000621
	private void OnEnable()
	{
		if (this.musicSystem != null)
		{
			this.musicSystem.CueStartPlaying(this.delayFromEnable);
			this.musicSystem.isLocked = true;
		}
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0000244E File Offset: 0x0000064E
	private void OnDisable()
	{
		if (this.musicSystem != null)
		{
			this.musicSystem.isLocked = false;
		}
	}

	// Token: 0x04000070 RID: 112
	public MusicSystem musicSystem;

	// Token: 0x04000071 RID: 113
	public float delayFromEnable = 14.76923f;
}
