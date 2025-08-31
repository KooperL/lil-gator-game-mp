using System;
using UnityEngine;

public class FallbackAudioListener : MonoBehaviour
{
	// Token: 0x0600013B RID: 315 RVA: 0x00007B2D File Offset: 0x00005D2D
	private void Start()
	{
		this.audioListener = Object.FindObjectOfType<AudioListener>(false);
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00007B3C File Offset: 0x00005D3C
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

	private AudioListener audioListener;

	public GameObject fallback;
}
