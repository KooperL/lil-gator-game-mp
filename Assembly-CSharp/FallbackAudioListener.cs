using System;
using UnityEngine;

public class FallbackAudioListener : MonoBehaviour
{
	// Token: 0x06000168 RID: 360 RVA: 0x000033C5 File Offset: 0x000015C5
	private void Start()
	{
		this.audioListener = global::UnityEngine.Object.FindObjectOfType<AudioListener>(false);
	}

	// Token: 0x06000169 RID: 361 RVA: 0x0001C6AC File Offset: 0x0001A8AC
	private void LateUpdate()
	{
		if (this.audioListener == null)
		{
			this.fallback.SetActive(false);
			this.audioListener = global::UnityEngine.Object.FindObjectOfType<AudioListener>(false);
			if (this.audioListener == null)
			{
				this.fallback.SetActive(true);
			}
		}
	}

	private AudioListener audioListener;

	public GameObject fallback;
}
