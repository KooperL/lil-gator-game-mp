using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000334 RID: 820
public class AudioSlider : MonoBehaviour
{
	// Token: 0x06001002 RID: 4098 RVA: 0x0000DD64 File Offset: 0x0000BF64
	private void LateUpdate()
	{
		this.mixer.SetFloat(this.mixerVariable, this.variableValue);
	}

	// Token: 0x040014BB RID: 5307
	public AudioMixer mixer;

	// Token: 0x040014BC RID: 5308
	public string mixerVariable;

	// Token: 0x040014BD RID: 5309
	public float variableValue;
}
