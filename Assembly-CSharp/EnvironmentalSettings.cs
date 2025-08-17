using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalSettings : MonoBehaviour
{
	// Token: 0x06000634 RID: 1588 RVA: 0x00030A8C File Offset: 0x0002EC8C
	[ContextMenu("Get default values")]
	public void GetDefaultValues()
	{
		ShaderVariables shaderVariables = global::UnityEngine.Object.FindObjectOfType<ShaderVariables>();
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

	// Token: 0x06000635 RID: 1589 RVA: 0x00030B18 File Offset: 0x0002ED18
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

	// Token: 0x06000636 RID: 1590 RVA: 0x000066AD File Offset: 0x000048AD
	private void OnDisable()
	{
		EnvironmentalSettings.environmentalSettings.Remove(this);
		ShaderVariables.s.UpdateVariables();
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x000066C5 File Offset: 0x000048C5
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

	// Token: 0x06000638 RID: 1592 RVA: 0x000066F9 File Offset: 0x000048F9
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

	// Token: 0x06000639 RID: 1593 RVA: 0x0000672D File Offset: 0x0000492D
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

	public static List<EnvironmentalSettings> environmentalSettings = new List<EnvironmentalSettings>();

	public int priority;

	[Range(0f, 1f)]
	public float strength = 1f;

	[Header("Settings")]
	public Material skybox;

	public float fogDistance;

	public float fogFadeDistance;

	public Texture2D fogTexture;

	public Texture2D waterFogTexture;

	public Color shadedColor;

	[Range(0f, 1f)]
	public float shadowStrength;

	[Range(-1f, 1f)]
	public float shadedBrightness;

	[Range(0f, 1f)]
	public float shadedDarkest;

	public Color cartoonShadedColor;

	[Range(0f, 1f)]
	public float cartoonBrightness;

	[Range(0f, 1f)]
	public float cartoonShadowStrength;

	private float target;

	private float fadeTime;

	private IEnumerator fadeCoroutine;
}
