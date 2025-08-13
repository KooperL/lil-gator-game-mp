using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x0200026C RID: 620
public class AudioSlider : MonoBehaviour
{
	// Token: 0x06000D54 RID: 3412 RVA: 0x000407B9 File Offset: 0x0003E9B9
	private void LateUpdate()
	{
		this.mixer.SetFloat(this.mixerVariable, this.variableValue);
	}

	// Token: 0x04001195 RID: 4501
	public AudioMixer mixer;

	// Token: 0x04001196 RID: 4502
	public string mixerVariable;

	// Token: 0x04001197 RID: 4503
	public float variableValue;
}
