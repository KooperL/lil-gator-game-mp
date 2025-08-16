using System;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderVariables : MonoBehaviour
{
	// Token: 0x06000F69 RID: 3945 RVA: 0x0000D572 File Offset: 0x0000B772
	private void Awake()
	{
		this.skybox = RenderSettings.skybox;
	}

	// Token: 0x06000F6A RID: 3946 RVA: 0x0000D57F File Offset: 0x0000B77F
	public void OnEnable()
	{
		ShaderVariables.s = this;
		this.randomVector = default(Vector4);
		this.randomVectorChangeTime = new Vector4(Time.time, Time.time, Time.time, Time.time);
		this.UpdateVariables();
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
	public void OnDisable()
	{
		this.UpdateVariables();
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
	private void OnValidate()
	{
		this.UpdateVariables();
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x00051120 File Offset: 0x0004F320
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

	// Token: 0x06000F6E RID: 3950 RVA: 0x00002229 File Offset: 0x00000429
	public void SetFog(bool isUnderwater)
	{
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x00051454 File Offset: 0x0004F654
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
				this.randomVector[i] = global::UnityEngine.Random.value;
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

	public static ShaderVariables s;

	public Material skybox;

	public Material[] waterMaterials = new Material[0];

	public Texture2D fogFactor;

	public Texture2D underwaterFogFactor;

	public Texture2D secondaryFogFactor;

	[Range(0f, 1f)]
	public float fogLerp;

	public Texture2D waterFogFactor;

	public Texture2D ditherPattern;

	public Color shadedColor;

	public Color highlightColor;

	[Range(0f, 1f)]
	public float shadowStrength;

	[Range(0f, 1f)]
	public float shadedBrightness;

	[Range(0f, 1f)]
	public float shadedDarkest;

	public Color cartoonShadedColor;

	[Range(0f, 1f)]
	public float cartoonBrightness;

	[Range(0f, 1f)]
	public float cartoonShadowStrength;

	public float cartoonFogDistance = 55f;

	public float cartoonFogFadeDistance = 10f;

	private Vector3 playerPosition;

	private Vector3 playerPositionVelocity;

	private Vector4 randomVector;

	public Vector4 randomVectorChangeDelay;

	private Vector4 randomVectorChangeTime;

	[HideInInspector]
	public bool allowNXMode = true;

	private bool dynamicEnvironment;

	private static readonly int cartoonFogFactor = Shader.PropertyToID("cartoonFogFactor");

	private static readonly int cartoonFogFactor2 = Shader.PropertyToID("cartoonFogFactor2");

	private static readonly int cartoonFogLerp = Shader.PropertyToID("cartoonFogLerp");

	private static readonly int _FogTex = Shader.PropertyToID("_FogTex");

	private static readonly int _WaterFogTex = Shader.PropertyToID("_WaterFogTex");

	private static readonly int _ShadedColor = Shader.PropertyToID("_ShadedColor");

	private static readonly int _HighlightColor = Shader.PropertyToID("_HighlightColor");

	private static readonly int _ShadedBrightness = Shader.PropertyToID("_ShadedBrightness");

	private static readonly int _ShadedDarkest = Shader.PropertyToID("_ShadedDarkest");

	private static readonly int _ShadowStrength = Shader.PropertyToID("_ShadowStrength");

	private static readonly int _CartoonShadedColor = Shader.PropertyToID("_CartoonShadedColor");

	private static readonly int _CartoonBrightness = Shader.PropertyToID("_CartoonBrightness");

	private static readonly int _CartoonShadowStrength = Shader.PropertyToID("_CartoonShadowStrength");

	private static readonly int _SolidFogStart = Shader.PropertyToID("_SolidFogStart");

	private static readonly int _SolidFogEnd = Shader.PropertyToID("_SolidFogEnd");

	private static readonly int _SolidFogScale = Shader.PropertyToID("_SolidFogScale");

	private static readonly int _PlayerPosition = Shader.PropertyToID("_PlayerPosition");

	private static readonly int _PlayerPositionView = Shader.PropertyToID("_PlayerPositionView");

	private static readonly int _RandomVector = Shader.PropertyToID("_RandomVector");

	private static readonly int _ProximityFade = Shader.PropertyToID("_ProximityFade");

	private static bool isProximityFadeIgnored = false;

	private static float proximityFade = 0f;
}
