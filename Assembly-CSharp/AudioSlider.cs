using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
	// Token: 0x0600105E RID: 4190 RVA: 0x0000E0D7 File Offset: 0x0000C2D7
	private void LateUpdate()
	{
		this.mixer.SetFloat(this.mixerVariable, this.variableValue);
	}

	public AudioMixer mixer;

	public string mixerVariable;

	public float variableValue;
}
