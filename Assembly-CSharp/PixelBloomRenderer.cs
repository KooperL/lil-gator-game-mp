using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000331 RID: 817
public sealed class PixelBloomRenderer : PostProcessEffectRenderer<PixelBloom>
{
	// Token: 0x06000FFB RID: 4091 RVA: 0x00052D54 File Offset: 0x00050F54
	public override void Render(PostProcessRenderContext context)
	{
		PropertySheet propertySheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/PixelBloom"));
		propertySheet.properties.SetFloat("_Blend", base.settings.blend);
		RuntimeUtilities.BlitFullscreenTriangle(context.command, context.source, context.destination, propertySheet, 0, false, null, false);
	}
}
