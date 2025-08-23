using System;
using UnityEngine;
using UnityEngine.Audio;

public class SetAudioSnapshot : MonoBehaviour
{
	// Token: 0x06001060 RID: 4192 RVA: 0x0000E0F1 File Offset: 0x0000C2F1
	private void Start()
	{
		if (this.onAwake)
		{
			this.SetSnapshot();
		}
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x0000E101 File Offset: 0x0000C301
	private void OnDisable()
	{
		if (this.onDisable)
		{
			this.SetSnapshot();
		}
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x0000E111 File Offset: 0x0000C311
	public void SetSnapshot()
	{
		this.snapshot.TransitionTo(this.transitionTime);
	}

	public bool onAwake = true;

	public bool onDisable;

	public AudioMixerSnapshot snapshot;

	public float transitionTime = 1f;
}
