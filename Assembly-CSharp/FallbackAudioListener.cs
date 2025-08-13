using System;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class FallbackAudioListener : MonoBehaviour
{
	// Token: 0x06000160 RID: 352 RVA: 0x00003322 File Offset: 0x00001522
	private void Start()
	{
		this.audioListener = Object.FindObjectOfType<AudioListener>(false);
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0001C014 File Offset: 0x0001A214
	private void LateUpdate()
	{
		if (this.audioListener == null)
		{
			this.fallback.SetActive(false);
			this.audioListener = Object.FindObjectOfType<AudioListener>(false);
			if (this.audioListener == null)
			{
				this.fallback.SetActive(true);
			}
		}
	}

	// Token: 0x0400021E RID: 542
	private AudioListener audioListener;

	// Token: 0x0400021F RID: 543
	public GameObject fallback;
}
