using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class DisableWhenMusicDone : MonoBehaviour
{
	// Token: 0x06000061 RID: 97 RVA: 0x000036D7 File Offset: 0x000018D7
	private void Update()
	{
		if (this.audioSource.isPlaying)
		{
			return;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000088 RID: 136
	public AudioSource audioSource;
}
