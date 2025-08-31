using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
	// Token: 0x06000D54 RID: 3412 RVA: 0x000407B9 File Offset: 0x0003E9B9
	private void LateUpdate()
	{
		this.mixer.SetFloat(this.mixerVariable, this.variableValue);
	}

	public AudioMixer mixer;

	public string mixerVariable;

	public float variableValue;
}
