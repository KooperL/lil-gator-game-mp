using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public sealed class PixelBloomRenderer : PostProcessEffectRenderer<PixelBloom>
{
	// Token: 0x06001056 RID: 4182 RVA: 0x00054C78 File Offset: 0x00052E78
	public override void Render(PostProcessRenderContext context)
	{
		PropertySheet propertySheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/PixelBloom"));
		propertySheet.properties.SetFloat("_Blend", base.settings.blend);
		RuntimeUtilities.BlitFullscreenTriangle(context.command, context.source, context.destination, propertySheet, 0, false, null, false);
	}
}
