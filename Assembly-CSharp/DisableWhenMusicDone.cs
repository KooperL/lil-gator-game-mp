using System;
using UnityEngine;

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

	public AudioSource audioSource;
}
