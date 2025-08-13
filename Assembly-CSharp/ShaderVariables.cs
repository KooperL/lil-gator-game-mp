using System;
using UnityEngine;

// Token: 0x020002FA RID: 762
[ExecuteInEditMode]
public class ShaderVariables : MonoBehaviour
{
	// Token: 0x06000F0D RID: 3853 RVA: 0x0000D1DF File Offset: 0x0000B3DF
	private void Awake()
	{
		this.skybox = RenderSettings.skybox;
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x0000D1EC File Offset: 0x0000B3EC
	public void OnEnable()
	{
		ShaderVariables.s = this;
		this.randomVector = default(Vector4);
		this.randomVectorChangeTime = new Vector4(Time.time, Time.time, Time.time, Time.time);
		this.UpdateVariables();
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x0000D225 File Offset: 0x0000B425
	public void OnDisable()
	{
		this.UpdateVariables();
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x0000D225 File Offset: 0x0000B425
	private void OnValidate()
	{
		this.UpdateVariables();
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x0004F38C File Offset: 0x0004D58C
	public void UpdateVariables()
	{
		Texture2D fogTexture = this.fogFactor;
		Texture2D texture2D = this.secondaryFogFactor;
		float num = this.fogLerp;
		Texture2D waterFogTexture = this.waterFogFactor;
		Color color = this.shadedColor;
		Color color2 = this.highlightColor;
		float num2 = this.shadedBrightness;
		float num3 = this.shadedDarkest;
		float num4 = this.shadowStrength;
		Color color3 = this.cartoonShadedColor;
		float num5 = this.cartoonBrightness;
		float num6 = this.cartoonShadowStrength;
		float num7 = this.cartoonFogDistance;
		float num8 = this.cartoonFogFadeDistance;
		Material material = this.skybox;
		if (EnvironmentalSettings.environmentalSettings != null)
		{
			foreach (EnvironmentalSettings environmentalSettings in EnvironmentalSettings.environmentalSettings)
			{
				if (environmentalSettings.strength != 0f)
				{
					if (environmentalSettings.strength == 1f)
					{
						fogTexture = environmentalSettings.fogTexture;
						texture2D = null;
						num = 0f;
					}
					else if (environmentalSettings.strength > num)
					{
						texture2D = environmentalSettings.fogTexture;
						num = environmentalSettings.strength;
					}
					if (environmentalSettings.strength > 0.5f)
					{
						if (environmentalSettings.waterFogTexture != null)
						{
							waterFogTexture = environmentalSettings.waterFogTexture;
						}
						if (environmentalSettings.skybox != null)
						{
							material = environmentalSettings.skybox;
						}
					}
					color = Color.Lerp(color, environmentalSettings.shadedColor, environmentalSettings.strength);
					num2 = Mathf.Lerp(num2, environmentalSettings.shadedBrightness, environmentalSettings.strength);
					num3 = Mathf.Lerp(num3, environmentalSettings.shadedDarkest, environmentalSettings.strength);
					num4 = Mathf.Lerp(num4, environmentalSettings.shadowStrength, environmentalSettings.strength);
					color3 = Color.Lerp(color3, environmentalSettings.cartoonShadedColor, environmentalSettings.strength);
					num5 = Mathf.Lerp(num5, environmentalSettings.cartoonBrightness, environmentalSettings.strength);
					num6 = Mathf.Lerp(num6, environmentalSettings.cartoonShadowStrength, environmentalSettings.strength);
					num7 = Mathf.Lerp(num7, environmentalSettings.fogDistance, environmentalSettings.strength);
					num8 = Mathf.Lerp(num8, environmentalSettings.fogFadeDistance, environmentalSettings.strength);
				}
			}
		}
		RenderSettings.skybox = material;
		Shader.SetGlobalTexture(ShaderVariables._WaterFogTex, waterFogTexture);
		Shader.SetGlobalTexture(ShaderVariables.cartoonFogFactor, fogTexture);
		if (num > 0f)
		{
			Shader.EnableKeyword("CARTOON_FOG_SECONDARY");
			Shader.SetGlobalTexture(ShaderVariables.cartoonFogFactor2, texture2D);
			Shader.SetGlobalFloat(ShaderVariables.cartoonFogLerp, num);
		}
		else
		{
			Shader.DisableKeyword("CARTOON_FOG_SECONDARY");
		}
		Shader.SetGlobalVector(ShaderVariables._ShadedColor, color);
		Shader.SetGlobalVector(ShaderVariables._HighlightColor, color2);
		Shader.SetGlobalFloat(ShaderVariables._ShadedBrightness, num2);
		Shader.SetGlobalFloat(ShaderVariables._ShadedDarkest, num3);
		Shader.SetGlobalFloat(ShaderVariables._ShadowStrength, num4);
		Shader.SetGlobalVector(ShaderVariables._CartoonShadedColor, color3);
		Shader.SetGlobalFloat(ShaderVariables._CartoonBrightness, num5);
		Shader.SetGlobalFloat(ShaderVariables._CartoonShadowStrength, num6);
		Shader.SetGlobalFloat(ShaderVariables._SolidFogStart, num7 - num8);
		Shader.SetGlobalFloat(ShaderVariables._SolidFogEnd, num7);
		Shader.SetGlobalFloat(ShaderVariables._SolidFogScale, 1f / num8);
		if (EnvironmentalSettings.environmentalSettings.Count == 0)
		{
			this.dynamicEnvironment = false;
		}
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x00002229 File Offset: 0x00000429
	public void SetFog(bool isUnderwater)
	{
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x0004F6C0 File Offset: 0x0004D8C0
	private void LateUpdate()
	{
		if (Player.transform != null)
		{
			this.playerPosition = Vector3.SmoothDamp(this.playerPosition, Player.Position, ref this.playerPositionVelocity, 0.05f);
			Shader.SetGlobalVector(ShaderVariables._PlayerPosition, this.playerPosition);
			Shader.SetGlobalVector(ShaderVariables._PlayerPositionView, MainCamera.c.WorldToViewportPoint(this.playerPosition));
		}
		for (int i = 0; i < 4; i++)
		{
			if (Time.time > this.randomVectorChangeTime[i])
			{
				ref Vector4 ptr = ref this.randomVectorChangeTime;
				int num = i;
				ptr[num] += this.randomVectorChangeDelay[i];
				this.randomVector[i] = Random.value;
			}
		}
		if (EnvironmentalSettings.environmentalSettings.Count > 0)
		{
			this.UpdateVariables();
		}
		if (Game.g != null)
		{
			bool flag = Game.State != GameState.Dialogue && !ItemCamera.isActive;
			ShaderVariables.proximityFade = Mathf.MoveTowards(ShaderVariables.proximityFade, flag ? 1f : 0f, Time.deltaTime * 2f);
			Shader.SetGlobalFloat(ShaderVariables._ProximityFade, 1f - ShaderVariables.proximityFade);
		}
	}

	// Token: 0x04001336 RID: 4918
	public static ShaderVariables s;

	// Token: 0x04001337 RID: 4919
	public Material skybox;

	// Token: 0x04001338 RID: 4920
	public Material[] waterMaterials = new Material[0];

	// Token: 0x04001339 RID: 4921
	public Texture2D fogFactor;

	// Token: 0x0400133A RID: 4922
	public Texture2D underwaterFogFactor;

	// Token: 0x0400133B RID: 4923
	public Texture2D secondaryFogFactor;

	// Token: 0x0400133C RID: 4924
	[Range(0f, 1f)]
	public float fogLerp;

	// Token: 0x0400133D RID: 4925
	public Texture2D waterFogFactor;

	// Token: 0x0400133E RID: 4926
	public Texture2D ditherPattern;

	// Token: 0x0400133F RID: 4927
	public Color shadedColor;

	// Token: 0x04001340 RID: 4928
	public Color highlightColor;

	// Token: 0x04001341 RID: 4929
	[Range(0f, 1f)]
	public float shadowStrength;

	// Token: 0x04001342 RID: 4930
	[Range(0f, 1f)]
	public float shadedBrightness;

	// Token: 0x04001343 RID: 4931
	[Range(0f, 1f)]
	public float shadedDarkest;

	// Token: 0x04001344 RID: 4932
	public Color cartoonShadedColor;

	// Token: 0x04001345 RID: 4933
	[Range(0f, 1f)]
	public float cartoonBrightness;

	// Token: 0x04001346 RID: 4934
	[Range(0f, 1f)]
	public float cartoonShadowStrength;

	// Token: 0x04001347 RID: 4935
	public float cartoonFogDistance = 55f;

	// Token: 0x04001348 RID: 4936
	public float cartoonFogFadeDistance = 10f;

	// Token: 0x04001349 RID: 4937
	private Vector3 playerPosition;

	// Token: 0x0400134A RID: 4938
	private Vector3 playerPositionVelocity;

	// Token: 0x0400134B RID: 4939
	private Vector4 randomVector;

	// Token: 0x0400134C RID: 4940
	public Vector4 randomVectorChangeDelay;

	// Token: 0x0400134D RID: 4941
	private Vector4 randomVectorChangeTime;

	// Token: 0x0400134E RID: 4942
	[HideInInspector]
	public bool allowNXMode = true;

	// Token: 0x0400134F RID: 4943
	private bool dynamicEnvironment;

	// Token: 0x04001350 RID: 4944
	private static readonly int cartoonFogFactor = Shader.PropertyToID("cartoonFogFactor");

	// Token: 0x04001351 RID: 4945
	private static readonly int cartoonFogFactor2 = Shader.PropertyToID("cartoonFogFactor2");

	// Token: 0x04001352 RID: 4946
	private static readonly int cartoonFogLerp = Shader.PropertyToID("cartoonFogLerp");

	// Token: 0x04001353 RID: 4947
	private static readonly int _FogTex = Shader.PropertyToID("_FogTex");

	// Token: 0x04001354 RID: 4948
	private static readonly int _WaterFogTex = Shader.PropertyToID("_WaterFogTex");

	// Token: 0x04001355 RID: 4949
	private static readonly int _ShadedColor = Shader.PropertyToID("_ShadedColor");

	// Token: 0x04001356 RID: 4950
	private static readonly int _HighlightColor = Shader.PropertyToID("_HighlightColor");

	// Token: 0x04001357 RID: 4951
	private static readonly int _ShadedBrightness = Shader.PropertyToID("_ShadedBrightness");

	// Token: 0x04001358 RID: 4952
	private static readonly int _ShadedDarkest = Shader.PropertyToID("_ShadedDarkest");

	// Token: 0x04001359 RID: 4953
	private static readonly int _ShadowStrength = Shader.PropertyToID("_ShadowStrength");

	// Token: 0x0400135A RID: 4954
	private static readonly int _CartoonShadedColor = Shader.PropertyToID("_CartoonShadedColor");

	// Token: 0x0400135B RID: 4955
	private static readonly int _CartoonBrightness = Shader.PropertyToID("_CartoonBrightness");

	// Token: 0x0400135C RID: 4956
	private static readonly int _CartoonShadowStrength = Shader.PropertyToID("_CartoonShadowStrength");

	// Token: 0x0400135D RID: 4957
	private static readonly int _SolidFogStart = Shader.PropertyToID("_SolidFogStart");

	// Token: 0x0400135E RID: 4958
	private static readonly int _SolidFogEnd = Shader.PropertyToID("_SolidFogEnd");

	// Token: 0x0400135F RID: 4959
	private static readonly int _SolidFogScale = Shader.PropertyToID("_SolidFogScale");

	// Token: 0x04001360 RID: 4960
	private static readonly int _PlayerPosition = Shader.PropertyToID("_PlayerPosition");

	// Token: 0x04001361 RID: 4961
	private static readonly int _PlayerPositionView = Shader.PropertyToID("_PlayerPositionView");

	// Token: 0x04001362 RID: 4962
	private static readonly int _RandomVector = Shader.PropertyToID("_RandomVector");

	// Token: 0x04001363 RID: 4963
	private static readonly int _ProximityFade = Shader.PropertyToID("_ProximityFade");

	// Token: 0x04001364 RID: 4964
	private static bool isProximityFadeIgnored = false;

	// Token: 0x04001365 RID: 4965
	private static float proximityFade = 0f;
}
