using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public sealed class PixelBloomRenderer : PostProcessEffectRenderer<PixelBloom>
{
	// Token: 0x06000D4D RID: 3405 RVA: 0x00040734 File Offset: 0x0003E934
	public override void Render(PostProcessRenderContext context)
	{
		PropertySheet propertySheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/PixelBloom"));
		propertySheet.properties.SetFloat("_Blend", base.settings.blend);
		context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
	}
}
