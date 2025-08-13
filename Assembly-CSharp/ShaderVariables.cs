using System;
using UnityEngine;

// Token: 0x02000239 RID: 569
[ExecuteInEditMode]
public class ShaderVariables : MonoBehaviour
{
	// Token: 0x06000C67 RID: 3175 RVA: 0x0003C1C4 File Offset: 0x0003A3C4
	private void Awake()
	{
		this.skybox = RenderSettings.skybox;
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x0003C1D1 File Offset: 0x0003A3D1
	public void OnEnable()
	{
		ShaderVariables.s = this;
		this.randomVector = default(Vector4);
		this.randomVectorChangeTime = new Vector4(Time.time, Time.time, Time.time, Time.time);
		this.UpdateVariables();
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x0003C20A File Offset: 0x0003A40A
	public void OnDisable()
	{
		this.UpdateVariables();
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x0003C212 File Offset: 0x0003A412
	private void OnValidate()
	{
		this.UpdateVariables();
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x0003C21C File Offset: 0x0003A41C
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

	// Token: 0x06000C6C RID: 3180 RVA: 0x0003C550 File Offset: 0x0003A750
	public void SetFog(bool isUnderwater)
	{
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x0003C554 File Offset: 0x0003A754
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
			bool flag = Game.State == GameState.Play && !ItemCamera.isActive;
			ShaderVariables.proximityFade = Mathf.MoveTowards(ShaderVariables.proximityFade, flag ? 1f : 0f, Time.deltaTime * 2f);
			Shader.SetGlobalFloat(ShaderVariables._ProximityFade, 1f - ShaderVariables.proximityFade);
		}
	}

	// Token: 0x0400102E RID: 4142
	public static ShaderVariables s;

	// Token: 0x0400102F RID: 4143
	public Material skybox;

	// Token: 0x04001030 RID: 4144
	public Material[] waterMaterials = new Material[0];

	// Token: 0x04001031 RID: 4145
	public Texture2D fogFactor;

	// Token: 0x04001032 RID: 4146
	public Texture2D underwaterFogFactor;

	// Token: 0x04001033 RID: 4147
	public Texture2D secondaryFogFactor;

	// Token: 0x04001034 RID: 4148
	[Range(0f, 1f)]
	public float fogLerp;

	// Token: 0x04001035 RID: 4149
	public Texture2D waterFogFactor;

	// Token: 0x04001036 RID: 4150
	public Texture2D ditherPattern;

	// Token: 0x04001037 RID: 4151
	public Color shadedColor;

	// Token: 0x04001038 RID: 4152
	public Color highlightColor;

	// Token: 0x04001039 RID: 4153
	[Range(0f, 1f)]
	public float shadowStrength;

	// Token: 0x0400103A RID: 4154
	[Range(0f, 1f)]
	public float shadedBrightness;

	// Token: 0x0400103B RID: 4155
	[Range(0f, 1f)]
	public float shadedDarkest;

	// Token: 0x0400103C RID: 4156
	public Color cartoonShadedColor;

	// Token: 0x0400103D RID: 4157
	[Range(0f, 1f)]
	public float cartoonBrightness;

	// Token: 0x0400103E RID: 4158
	[Range(0f, 1f)]
	public float cartoonShadowStrength;

	// Token: 0x0400103F RID: 4159
	public float cartoonFogDistance = 55f;

	// Token: 0x04001040 RID: 4160
	public float cartoonFogFadeDistance = 10f;

	// Token: 0x04001041 RID: 4161
	private Vector3 playerPosition;

	// Token: 0x04001042 RID: 4162
	private Vector3 playerPositionVelocity;

	// Token: 0x04001043 RID: 4163
	private Vector4 randomVector;

	// Token: 0x04001044 RID: 4164
	public Vector4 randomVectorChangeDelay;

	// Token: 0x04001045 RID: 4165
	private Vector4 randomVectorChangeTime;

	// Token: 0x04001046 RID: 4166
	[HideInInspector]
	public bool allowNXMode = true;

	// Token: 0x04001047 RID: 4167
	private bool dynamicEnvironment;

	// Token: 0x04001048 RID: 4168
	private static readonly int cartoonFogFactor = Shader.PropertyToID("cartoonFogFactor");

	// Token: 0x04001049 RID: 4169
	private static readonly int cartoonFogFactor2 = Shader.PropertyToID("cartoonFogFactor2");

	// Token: 0x0400104A RID: 4170
	private static readonly int cartoonFogLerp = Shader.PropertyToID("cartoonFogLerp");

	// Token: 0x0400104B RID: 4171
	private static readonly int _FogTex = Shader.PropertyToID("_FogTex");

	// Token: 0x0400104C RID: 4172
	private static readonly int _WaterFogTex = Shader.PropertyToID("_WaterFogTex");

	// Token: 0x0400104D RID: 4173
	private static readonly int _ShadedColor = Shader.PropertyToID("_ShadedColor");

	// Token: 0x0400104E RID: 4174
	private static readonly int _HighlightColor = Shader.PropertyToID("_HighlightColor");

	// Token: 0x0400104F RID: 4175
	private static readonly int _ShadedBrightness = Shader.PropertyToID("_ShadedBrightness");

	// Token: 0x04001050 RID: 4176
	private static readonly int _ShadedDarkest = Shader.PropertyToID("_ShadedDarkest");

	// Token: 0x04001051 RID: 4177
	private static readonly int _ShadowStrength = Shader.PropertyToID("_ShadowStrength");

	// Token: 0x04001052 RID: 4178
	private static readonly int _CartoonShadedColor = Shader.PropertyToID("_CartoonShadedColor");

	// Token: 0x04001053 RID: 4179
	private static readonly int _CartoonBrightness = Shader.PropertyToID("_CartoonBrightness");

	// Token: 0x04001054 RID: 4180
	private static readonly int _CartoonShadowStrength = Shader.PropertyToID("_CartoonShadowStrength");

	// Token: 0x04001055 RID: 4181
	private static readonly int _SolidFogStart = Shader.PropertyToID("_SolidFogStart");

	// Token: 0x04001056 RID: 4182
	private static readonly int _SolidFogEnd = Shader.PropertyToID("_SolidFogEnd");

	// Token: 0x04001057 RID: 4183
	private static readonly int _SolidFogScale = Shader.PropertyToID("_SolidFogScale");

	// Token: 0x04001058 RID: 4184
	private static readonly int _PlayerPosition = Shader.PropertyToID("_PlayerPosition");

	// Token: 0x04001059 RID: 4185
	private static readonly int _PlayerPositionView = Shader.PropertyToID("_PlayerPositionView");

	// Token: 0x0400105A RID: 4186
	private static readonly int _RandomVector = Shader.PropertyToID("_RandomVector");

	// Token: 0x0400105B RID: 4187
	private static readonly int _ProximityFade = Shader.PropertyToID("_ProximityFade");

	// Token: 0x0400105C RID: 4188
	private static bool isProximityFadeIgnored = false;

	// Token: 0x0400105D RID: 4189
	private static float proximityFade = 0f;
}
