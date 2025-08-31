using System;
using UnityEngine;
using UnityEngine.Audio;

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

	public bool onAwake = true;

	public bool onDisable;

	public AudioMixerSnapshot snapshot;

	public float transitionTime = 1f;
}
