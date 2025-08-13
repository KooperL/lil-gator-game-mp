using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x0200026D RID: 621
public class SetAudioSnapshot : MonoBehaviour
{
	// Token: 0x06000D56 RID: 3414 RVA: 0x000407DB File Offset: 0x0003E9DB
	private void Start()
	{
		if (this.onAwake)
		{
			this.SetSnapshot();
		}
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x000407EB File Offset: 0x0003E9EB
	private void OnDisable()
	{
		if (this.onDisable)
		{
			this.SetSnapshot();
		}
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x000407FB File Offset: 0x0003E9FB
	public void SetSnapshot()
	{
		this.snapshot.TransitionTo(this.transitionTime);
	}

	// Token: 0x04001198 RID: 4504
	public bool onAwake = true;

	// Token: 0x04001199 RID: 4505
	public bool onDisable;

	// Token: 0x0400119A RID: 4506
	public AudioMixerSnapshot snapshot;

	// Token: 0x0400119B RID: 4507
	public float transitionTime = 1f;
}
