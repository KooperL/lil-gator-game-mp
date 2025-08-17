using System;
using UnityEngine;
using UnityEngine.Audio;

public class SetAudioSnapshot : MonoBehaviour
{
	// Token: 0x0600105F RID: 4191 RVA: 0x0000E0E7 File Offset: 0x0000C2E7
	private void Start()
	{
		if (this.onAwake)
		{
			this.SetSnapshot();
		}
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x0000E0F7 File Offset: 0x0000C2F7
	private void OnDisable()
	{
		if (this.onDisable)
		{
			this.SetSnapshot();
		}
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x0000E107 File Offset: 0x0000C307
	public void SetSnapshot()
	{
		this.snapshot.TransitionTo(this.transitionTime);
	}

	public bool onAwake = true;

	public bool onDisable;

	public AudioMixerSnapshot snapshot;

	public float transitionTime = 1f;
}
