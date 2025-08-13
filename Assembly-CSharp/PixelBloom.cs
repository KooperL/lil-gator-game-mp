using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000330 RID: 816
[PostProcess(typeof(PixelBloomRenderer), 2, "Custom/PixelBloom", true)]
[Serializable]
public sealed class PixelBloom : PostProcessEffectSettings
{
	// Token: 0x040014B9 RID: 5305
	[Range(0f, 1f)]
	[Tooltip("Grayscale effect intensity.")]
	public FloatParameter blend = new FloatParameter
	{
		value = 0.5f
	};
}
