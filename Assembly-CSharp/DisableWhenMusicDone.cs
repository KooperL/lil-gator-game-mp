using System;
using UnityEngine;

public class DisableWhenMusicDone : MonoBehaviour
{
	// Token: 0x06000062 RID: 98 RVA: 0x000024E1 File Offset: 0x000006E1
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
