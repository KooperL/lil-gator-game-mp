using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalSettings : MonoBehaviour
{
	// Token: 0x060004EE RID: 1262 RVA: 0x0001A864 File Offset: 0x00018A64
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

	// Token: 0x060004EF RID: 1263 RVA: 0x0001A8F0 File Offset: 0x00018AF0
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

	// Token: 0x060004F0 RID: 1264 RVA: 0x0001A957 File Offset: 0x00018B57
	private void OnDisable()
	{
		EnvironmentalSettings.environmentalSettings.Remove(this);
		ShaderVariables.s.UpdateVariables();
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0001A96F File Offset: 0x00018B6F
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

	// Token: 0x060004F2 RID: 1266 RVA: 0x0001A9A3 File Offset: 0x00018BA3
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

	// Token: 0x060004F3 RID: 1267 RVA: 0x0001A9D7 File Offset: 0x00018BD7
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
