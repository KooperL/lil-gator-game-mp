using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000268 RID: 616
[PostProcess(typeof(PixelBloomRenderer), PostProcessEvent.AfterStack, "Custom/PixelBloom", true)]
[Serializable]
public sealed class PixelBloom : PostProcessEffectSettings
{
	// Token: 0x04001193 RID: 4499
	[Range(0f, 1f)]
	[Tooltip("Grayscale effect intensity.")]
	public FloatParameter blend = new FloatParameter
	{
		value = 0.5f
	};
}
