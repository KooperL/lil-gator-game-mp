using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[PostProcess(typeof(PixelBloomRenderer), PostProcessEvent.AfterStack, "Custom/PixelBloom", true)]
[Serializable]
public sealed class PixelBloom : PostProcessEffectSettings
{
	[Range(0f, 1f)]
	[Tooltip("Grayscale effect intensity.")]
	public FloatParameter blend = new FloatParameter
	{
		value = 0.5f
	};
}
