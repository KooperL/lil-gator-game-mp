using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public sealed class PixelBloomRenderer : PostProcessEffectRenderer<PixelBloom>
{
	// Token: 0x06001057 RID: 4183 RVA: 0x00054F40 File Offset: 0x00053140
	public override void Render(PostProcessRenderContext context)
	{
		PropertySheet propertySheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/PixelBloom"));
		propertySheet.properties.SetFloat("_Blend", base.settings.blend);
		context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
	}
}
