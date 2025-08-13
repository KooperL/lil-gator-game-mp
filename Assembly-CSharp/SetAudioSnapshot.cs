using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000335 RID: 821
public class SetAudioSnapshot : MonoBehaviour
{
	// Token: 0x06001004 RID: 4100 RVA: 0x0000DD7E File Offset: 0x0000BF7E
	private void Start()
	{
		if (this.onAwake)
		{
			this.SetSnapshot();
		}
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x0000DD8E File Offset: 0x0000BF8E
	private void OnDisable()
	{
		if (this.onDisable)
		{
			this.SetSnapshot();
		}
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x0000DD9E File Offset: 0x0000BF9E
	public void SetSnapshot()
	{
		this.snapshot.TransitionTo(this.transitionTime);
	}

	// Token: 0x040014BE RID: 5310
	public bool onAwake = true;

	// Token: 0x040014BF RID: 5311
	public bool onDisable;

	// Token: 0x040014C0 RID: 5312
	public AudioMixerSnapshot snapshot;

	// Token: 0x040014C1 RID: 5313
	public float transitionTime = 1f;
}
