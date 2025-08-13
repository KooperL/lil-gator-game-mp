using System;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class DisableWhenMusicDone : MonoBehaviour
{
	// Token: 0x0600005A RID: 90 RVA: 0x0000247D File Offset: 0x0000067D
	private void Update()
	{
		if (this.audioSource.isPlaying)
		{
			return;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000072 RID: 114
	public AudioSource audioSource;
}
