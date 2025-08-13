using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000140 RID: 320
public class EnvironmentalSettings : MonoBehaviour
{
	// Token: 0x060005FA RID: 1530 RVA: 0x0002F390 File Offset: 0x0002D590
	[ContextMenu("Get default values")]
	public void GetDefaultValues()
	{
		ShaderVariables shaderVariables = Object.FindObjectOfType<ShaderVariables>();
		this.fogDistance = shaderVariables.cartoonFogDistance;
		this.fogFadeDistance = shaderVariables.cartoonFogFadeDistance;
		this.fogTexture = shaderVariables.fogFactor;
		this.shadedColor = shaderVariables.shadedColor;
		this.shadowStrength = shaderVariables.shadowStrength;
		this.shadedBrightness = shaderVariables.shadedBrightness;
		this.shadedDarkest = shaderVariables.shadedDarkest;
		this.cartoonShadedColor = shaderVariables.cartoonShadedColor;
		this.cartoonBrightness = shaderVariables.cartoonBrightness;
		this.cartoonShadowStrength = shaderVariables.cartoonShadowStrength;
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0002F41C File Offset: 0x0002D61C
	private void OnEnable()
	{
		int num = -1;
		for (int i = 0; i < EnvironmentalSettings.environmentalSettings.Count; i++)
		{
			if (EnvironmentalSettings.environmentalSettings[i].priority > this.priority)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			EnvironmentalSettings.environmentalSettings.Add(this);
		}
		else
		{
			EnvironmentalSettings.environmentalSettings.Insert(num, this);
		}
		ShaderVariables.s.UpdateVariables();
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x000063E7 File Offset: 0x000045E7
	private void OnDisable()
	{
		EnvironmentalSettings.environmentalSettings.Remove(this);
		ShaderVariables.s.UpdateVariables();
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x000063FF File Offset: 0x000045FF
	public void FadeIn(float fadeTime)
	{
		this.target = 1f;
		this.fadeTime = fadeTime;
		if (this.fadeCoroutine == null)
		{
			this.fadeCoroutine = this.FadeCoroutine();
			CoroutineUtil.Start(this.fadeCoroutine);
		}
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x00006433 File Offset: 0x00004633
	public void FadeOut(float fadeTime)
	{
		this.target = 0f;
		this.fadeTime = fadeTime;
		if (this.fadeCoroutine == null)
		{
			this.fadeCoroutine = this.FadeCoroutine();
			CoroutineUtil.Start(this.fadeCoroutine);
		}
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x00006467 File Offset: 0x00004667
	private IEnumerator FadeCoroutine()
	{
		while (this.strength != this.target)
		{
			this.strength = Mathf.MoveTowards(this.strength, this.target, Time.deltaTime / this.fadeTime);
			yield return null;
		}
		this.fadeCoroutine = null;
		yield break;
	}

	// Token: 0x040007FD RID: 2045
	public static List<EnvironmentalSettings> environmentalSettings = new List<EnvironmentalSettings>();

	// Token: 0x040007FE RID: 2046
	public int priority;

	// Token: 0x040007FF RID: 2047
	[Range(0f, 1f)]
	public float strength = 1f;

	// Token: 0x04000800 RID: 2048
	[Header("Settings")]
	public Material skybox;

	// Token: 0x04000801 RID: 2049
	public float fogDistance;

	// Token: 0x04000802 RID: 2050
	public float fogFadeDistance;

	// Token: 0x04000803 RID: 2051
	public Texture2D fogTexture;

	// Token: 0x04000804 RID: 2052
	public Texture2D waterFogTexture;

	// Token: 0x04000805 RID: 2053
	public Color shadedColor;

	// Token: 0x04000806 RID: 2054
	[Range(0f, 1f)]
	public float shadowStrength;

	// Token: 0x04000807 RID: 2055
	[Range(-1f, 1f)]
	public float shadedBrightness;

	// Token: 0x04000808 RID: 2056
	[Range(0f, 1f)]
	public float shadedDarkest;

	// Token: 0x04000809 RID: 2057
	public Color cartoonShadedColor;

	// Token: 0x0400080A RID: 2058
	[Range(0f, 1f)]
	public float cartoonBrightness;

	// Token: 0x0400080B RID: 2059
	[Range(0f, 1f)]
	public float cartoonShadowStrength;

	// Token: 0x0400080C RID: 2060
	private float target;

	// Token: 0x0400080D RID: 2061
	private float fadeTime;

	// Token: 0x0400080E RID: 2062
	private IEnumerator fadeCoroutine;
}
