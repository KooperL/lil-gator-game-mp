using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x02000238 RID: 568
public class Settings : MonoBehaviour
{
	// Token: 0x06000C4C RID: 3148 RVA: 0x0003B1CC File Offset: 0x000393CC
	public static float NormalizedAudioVolume(float flatVolume)
	{
		return Mathf.Log10(Mathf.Max(flatVolume, 1E-05f)) * 20f;
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x0003B1E4 File Offset: 0x000393E4
	public static int FloatToInt(float f)
	{
		return Mathf.FloorToInt(f * 1000f);
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0003B1F2 File Offset: 0x000393F2
	public static float IntToFloat(int i)
	{
		return (float)i / 1000f;
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0003B1FC File Offset: 0x000393FC
	public static int CurrentResolutionIndex
	{
		get
		{
			Vector2Int vector2Int = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);
			int num;
			if (Settings.pixelResolutions.TryFindIndex(vector2Int, out num))
			{
				return num;
			}
			return -1;
		}
	}

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0003B23C File Offset: 0x0003943C
	// (set) Token: 0x06000C51 RID: 3153 RVA: 0x0003B288 File Offset: 0x00039488
	public static Settings s
	{
		get
		{
			if (Settings.instance == null)
			{
				Settings.instance = Object.FindObjectOfType<Settings>();
			}
			if (Settings.instance != null && !Settings.instance.initialized)
			{
				Settings.instance.Initialize();
			}
			return Settings.instance;
		}
		set
		{
			Settings.instance = value;
		}
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x0003B290 File Offset: 0x00039490
	private void Initialize()
	{
		if (this.initialized)
		{
			return;
		}
		this.initialized = true;
		Settings.s = this;
		List<Vector2Int> list = new List<Vector2Int>();
		List<int> list2 = new List<int>();
		foreach (Resolution resolution in Screen.resolutions)
		{
			Vector2Int vector2Int = resolution.PixelSize();
			if (!list.Contains(vector2Int))
			{
				list.Add(vector2Int);
			}
			if (!list2.Contains(resolution.refreshRate))
			{
				list2.Add(resolution.refreshRate);
			}
		}
		SteamManager.ForceInitialize();
		if (SteamUtils.IsSteamRunningOnSteamDeck())
		{
			Vector2Int vector2Int2 = new Vector2Int(1280, 800);
			if (!list.Contains(vector2Int2))
			{
				list.Add(vector2Int2);
			}
			if (!this.settingsData.ints.ContainsKey("ResolutionX"))
			{
				this.Write("ResolutionX", 1280);
				this.Write("ResolutionY", 800);
			}
		}
		Settings.pixelResolutions = list.ToArray();
		Settings.refreshRates = list2.ToArray();
		if (this.ambientOcclusionSetting == null && this.postProcessProfile != null)
		{
			this.ambientOcclusionSetting = this.postProcessProfile.GetSetting<AmbientOcclusion>();
		}
		this.ReadFromDisk();
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x0003B3C2 File Offset: 0x000395C2
	private void OnEnable()
	{
		this.LoadSettings();
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0003B3CA File Offset: 0x000395CA
	private void Start()
	{
		this.LoadSettings();
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x0003B3D2 File Offset: 0x000395D2
	private void OnDisable()
	{
		if (Settings.instance == this)
		{
			this.WriteToDisk();
		}
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x0003B3E7 File Offset: 0x000395E7
	private void OnApplicationQuit()
	{
		if (Settings.instance == this)
		{
			this.WriteToDisk();
		}
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x0003B3FC File Offset: 0x000395FC
	public void ReadFromDisk()
	{
		if (Settings.hasCachedData)
		{
			this.settingsData = Settings.cachedSettingsData;
			return;
		}
		Settings.SettingsData settingsData = FileUtil.ReadSettingsData();
		if (settingsData != null && settingsData.v == 12)
		{
			this.settingsData = settingsData;
		}
		if (this.settingsData == null)
		{
			this.settingsData = new Settings.SettingsData();
		}
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0003B449 File Offset: 0x00039649
	public void WriteToDisk()
	{
		this.settingsData.v = 12;
		FileUtil.WriteSettingsData(this.settingsData);
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x0003B463 File Offset: 0x00039663
	private IEnumerator LoadSettingsNextFrame()
	{
		yield return null;
		this.willUpdateSettings = false;
		this.LoadSettings();
		yield break;
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x0003B474 File Offset: 0x00039674
	public void LoadSettings()
	{
		if (!this.initialized)
		{
			this.Initialize();
		}
		bool flag = false;
		bool flag2 = this.ReadInt("PixelFilter", 0) != 0;
		ItemGlasses.SetPixelFilterEnabled(flag2);
		FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;
		switch (this.ReadInt("Fullscreen", 1))
		{
		case 0:
			fullScreenMode = FullScreenMode.ExclusiveFullScreen;
			break;
		case 1:
			fullScreenMode = FullScreenMode.FullScreenWindow;
			break;
		case 2:
			fullScreenMode = FullScreenMode.Windowed;
			break;
		}
		if (SteamUtils.IsSteamRunningOnSteamDeck())
		{
			fullScreenMode = FullScreenMode.FullScreenWindow;
		}
		Vector2Int vector2Int = Screen.currentResolution.PixelSize();
		Vector2Int vector2Int2 = new Vector2Int(this.ReadInt("ResolutionX", vector2Int.x), this.ReadInt("ResolutionY", vector2Int.y));
		if (!Settings.pixelResolutions.Contains(vector2Int2))
		{
			int num = (vector2Int2 - Settings.pixelResolutions[0]).sqrMagnitude;
			Vector2Int vector2Int3 = Settings.pixelResolutions[0];
			for (int i = 1; i < Settings.pixelResolutions.Length; i++)
			{
				int sqrMagnitude = (vector2Int2 - Settings.pixelResolutions[i]).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					vector2Int3 = Settings.pixelResolutions[i];
					num = sqrMagnitude;
				}
			}
			vector2Int2 = vector2Int3;
			this.Write("ResolutionX", vector2Int2.x);
			this.Write("ResolutionY", vector2Int2.y);
		}
		if (vector2Int != vector2Int2)
		{
			Screen.SetResolution(vector2Int2.x, vector2Int2.y, fullScreenMode);
		}
		else if (Screen.fullScreenMode != fullScreenMode)
		{
			Screen.fullScreenMode = fullScreenMode;
		}
		bool flag3 = false;
		if (MainCamera.c != null)
		{
			MainCamera.c.allowDynamicResolution = flag3;
			if (!flag3)
			{
				ScalableBufferManager.ResizeBuffers(1f, 1f);
			}
		}
		if (this.dynamicResolutionScaler != null)
		{
			this.dynamicResolutionScaler.enabled = flag3;
		}
		Settings.mouseSensitivity = Mathf.Pow(this.ReadFloat("MouseSensitivity", 1f), 2f);
		float num2 = this.ReadFloat("MasterVolume", 0.9f);
		this.volumeMixer.SetFloat("MasterVolume", Settings.NormalizedAudioVolume(num2));
		Settings.gameVolume = Settings.NormalizedAudioVolume(this.ReadFloat("GameVolume", 1f));
		if (FadeGameVolume.IsFaded)
		{
			FadeGameVolume.UpdateGameVolume();
		}
		else
		{
			this.volumeMixer.SetFloat("GameVolume", Settings.gameVolume);
		}
		float num3 = this.ReadFloat("MusicVolume", 0.5f);
		this.volumeMixer.SetFloat("MusicVolume", Settings.NormalizedAudioVolume(num3));
		Settings.hasMusic = num2 * num3 > 0.001f;
		PlayerOrbitCamera.gameplayDistanceMultiplier = this.ReadFloat("CameraDistance", 1f);
		Settings.mouseInvertHorizontal = this.ReadBool("InvertHorizontal", false);
		Settings.mouseInvertVertical = this.ReadBool("InvertVertical", false);
		Settings.reCenterCameraIsToggle = this.ReadInt("ReCenterCamera", 0) == 1;
		int num4 = this.ReadInt("ShadowResolution", 3);
		if (SpecialSettingsZone.specialSettings != null && SpecialSettingsZone.specialSettings.shadowResolution != -1)
		{
			num4 = SpecialSettingsZone.specialSettings.shadowResolution;
		}
		if (flag2)
		{
			num4 = Mathf.Max(num4, 3);
		}
		switch (num4)
		{
		case 0:
			if (this.sunLight != null)
			{
				this.sunLight.shadows = LightShadows.None;
			}
			break;
		case 1:
			if (this.sunLight != null)
			{
				this.sunLight.shadows = LightShadows.Soft;
				this.sunLight.shadowResolution = LightShadowResolution.Low;
				this.sunLight.shadowBias = 0.02f;
				this.sunLight.shadowNormalBias = 0.05f;
			}
			break;
		case 2:
			if (this.sunLight != null)
			{
				this.sunLight.shadows = LightShadows.Soft;
				this.sunLight.shadowResolution = LightShadowResolution.Medium;
				this.sunLight.shadowBias = 0.025f;
				this.sunLight.shadowNormalBias = 0.1f;
			}
			break;
		case 3:
			if (this.sunLight != null)
			{
				this.sunLight.shadows = LightShadows.Soft;
				this.sunLight.shadowResolution = LightShadowResolution.High;
				this.sunLight.shadowBias = 0.025f;
				this.sunLight.shadowNormalBias = 0.2f;
			}
			break;
		case 4:
			if (this.sunLight != null)
			{
				this.sunLight.shadows = LightShadows.Soft;
				this.sunLight.shadowResolution = LightShadowResolution.VeryHigh;
				this.sunLight.shadowBias = 0.02f;
				this.sunLight.shadowNormalBias = 0.3f;
			}
			break;
		}
		if (this.sunLight != null && this.sunLight.shadows != LightShadows.None)
		{
			this.sunLight.shadows = (this.ReadBool("ShadowSoft", true) ? LightShadows.Soft : LightShadows.Hard);
		}
		if (SpecialSettingsZone.specialSettings != null && SpecialSettingsZone.specialSettings.shadowCascades != -1)
		{
			QualitySettings.shadowCascades = SpecialSettingsZone.specialSettings.shadowCascades;
		}
		else
		{
			switch (this.ReadInt("ShadowCascades", 2))
			{
			case 0:
				QualitySettings.shadowCascades = 1;
				break;
			case 1:
				QualitySettings.shadowCascades = 2;
				break;
			case 2:
				QualitySettings.shadowCascades = 4;
				break;
			}
		}
		float num5 = 0f;
		switch (this.ReadInt("ShadowDistance", 2))
		{
		case 0:
			num5 = -10f;
			break;
		case 1:
			num5 = 0f;
			break;
		case 2:
			num5 = 20f;
			break;
		}
		float num6 = 50f;
		switch (this.ReadInt("FogDistance", 2))
		{
		case 0:
			num6 = 45f;
			break;
		case 1:
			num6 = 50f;
			break;
		case 2:
			num6 = 55f;
			break;
		}
		foreach (Terrain terrain in this.terrains)
		{
			terrain.detailObjectDistance = num6;
			terrain.basemapDistance = num6;
		}
		if (SpecialSettingsZone.specialSettings != null && SpecialSettingsZone.specialSettings.shadowDistance > 0f)
		{
			QualitySettings.shadowDistance = SpecialSettingsZone.specialSettings.shadowDistance;
		}
		else
		{
			QualitySettings.shadowDistance = num6 + num5;
		}
		if (this.shaderVariables != null)
		{
			this.shaderVariables.cartoonFogDistance = num6;
		}
		float num7 = 0f;
		switch (this.ReadInt("CullDistance", 1))
		{
		case 0:
			num7 = 0f;
			break;
		case 1:
			num7 = 20f;
			break;
		case 2:
			num7 = 40f;
			break;
		}
		if (this.cameraLayerCullDistances != null)
		{
			this.cameraLayerCullDistances.SetFogDistance(num6, num6 + num5, num7);
		}
		float num8 = this.heightmapPixelError;
		switch (this.ReadInt("TerrainQuality", 4))
		{
		case 0:
			num8 = 60f;
			break;
		case 1:
			num8 = 40f;
			break;
		case 2:
			num8 = 20f;
			break;
		case 3:
			num8 = 10f;
			break;
		case 4:
			num8 = 5f;
			break;
		case 5:
			num8 = 2f;
			break;
		}
		float num9 = (float)Screen.currentResolution.height;
		float num10 = 1080f;
		float num11;
		if (flag2)
		{
			num11 = 216f / num10;
		}
		else
		{
			num11 = num9 / num10;
		}
		num8 *= num11;
		int j;
		if (num8 != this.heightmapPixelError)
		{
			this.heightmapPixelError = num8;
			Terrain[] array = this.terrains;
			for (j = 0; j < array.Length; j++)
			{
				array[j].heightmapPixelError = this.heightmapPixelError;
			}
		}
		float num12 = 1f;
		switch (this.ReadInt("DetailDensity", 3))
		{
		case 0:
			num12 = 0.4f;
			break;
		case 1:
			num12 = 0.6f;
			break;
		case 2:
			num12 = 0.8f;
			break;
		case 3:
			num12 = 1f;
			break;
		}
		foreach (Terrain terrain2 in this.terrains)
		{
			if (terrain2.detailObjectDensity != num12)
			{
				terrain2.detailObjectDensity = num12;
			}
		}
		float num13 = 60f;
		switch (this.ReadInt("SettingTreeDistance", 2))
		{
		case 0:
			num13 = 5f;
			break;
		case 1:
			num13 = 25f;
			break;
		case 2:
			num13 = 55f;
			break;
		}
		int num14 = this.ReadInt("TreeQuality", 1);
		LODTree.SetTreeQualitySettings(num6 + num13, num14 == 1);
		Time.fixedDeltaTime = 0.02f;
		j = this.ReadInt("PostEffectQuality", 1);
		if (j != 0)
		{
			if (j == 1)
			{
				if (this.postProcessVolumes != null)
				{
					this.postProcessVolumes.SetActive(true);
				}
				if (this.ambientOcclusionSetting != null)
				{
					this.ambientOcclusionSetting.active = true;
				}
			}
		}
		else
		{
			if (this.postProcessVolumes != null)
			{
				this.postProcessVolumes.SetActive(true);
			}
			if (this.ambientOcclusionSetting != null)
			{
				this.ambientOcclusionSetting.active = false;
			}
		}
		foreach (Terrain terrain3 in this.terrains)
		{
			if (flag)
			{
				terrain3.terrainData.treePrototypes = terrain3.terrainData.treePrototypes;
			}
		}
		PlayerInput.autoSword = this.ReadBool("AutoSword", true);
		PlayerInput.useMovementToAim = this.ReadBool("UseMovementToAim", false);
		PlayerOrbitCamera.cameraSmoothingFactor = this.ReadFloat("CameraSmoothing", 1f);
		PlayerInput.interactMapping = (PlayerInput.InteractMapping)this.ReadInt("InteractMapping", 0);
		PlayerInput.secondaryMapping = (PlayerInput.SecondaryMapping)this.ReadInt("SecondaryMapping", 0);
		UIInteractButtonPrompt.showPrompt = this.ReadBool("ShowInteractPrompt", true);
		BabyPlayerToggle.SetBabyMode(this.ReadBool("BabyMode", false));
		SpeedrunData.IsSpeedrunMode = this.ReadInt("SpeedrunMode", 0) == 1;
		switch (this.ReadInt("Speedrun_AutoName", 0))
		{
		case 0:
			SpeedrunData.autoName = AutoNameFunctionality.LilGator;
			break;
		case 1:
			SpeedrunData.autoName = AutoNameFunctionality.File1;
			break;
		case 2:
			SpeedrunData.autoName = AutoNameFunctionality.File2;
			break;
		case 3:
			SpeedrunData.autoName = AutoNameFunctionality.File3;
			break;
		}
		SpeedrunData.split_tutorialEnd = this.ReadBool("Split_TutorialEnd", false);
		SpeedrunData.split_jillQuest = this.ReadBool("Split_Jill", false);
		SpeedrunData.split_martinQuest = this.ReadBool("Split_Martin", false);
		SpeedrunData.split_averyQuest = this.ReadBool("Split_Avery", false);
		SpeedrunData.split_showTownToSis = this.ReadBool("Split_ShowTownToSis", false);
		SpeedrunData.split_flashbackEnd = this.ReadBool("Split_FlashbackEnd", true);
		SpeedrunData.split_credits = this.ReadBool("Split_Credits", false);
		SpeedrunData.split_goHome = this.ReadBool("Split_GoHome", true);
		int num15 = this.ReadInt("Language", 0);
		if (!this.ReadBool("CheckLanguage", false))
		{
			int num16 = 0;
			SystemLanguage systemLanguage = Application.systemLanguage;
			if (systemLanguage <= SystemLanguage.French)
			{
				if (systemLanguage != SystemLanguage.English)
				{
					if (systemLanguage == SystemLanguage.French)
					{
						num16 = 1;
					}
				}
				else
				{
					num16 = 0;
				}
			}
			else if (systemLanguage != SystemLanguage.German)
			{
				if (systemLanguage != SystemLanguage.Portuguese)
				{
					if (systemLanguage == SystemLanguage.Spanish)
					{
						num16 = 2;
					}
				}
				else
				{
					num16 = 4;
				}
			}
			else
			{
				num16 = 3;
			}
			num15 = num16;
			this.Write("Language", num15);
			this.Write("CheckLanguage", true);
		}
		Language language;
		switch (num15)
		{
		case 0:
			language = Language.English;
			break;
		case 1:
			language = Language.French;
			break;
		case 2:
			language = Language.Spanish;
			break;
		case 3:
			language = Language.German;
			break;
		case 4:
			language = Language.BrazilianPortuguese;
			break;
		default:
			language = Settings.language;
			break;
		}
		if (Settings.language != language)
		{
			Settings.language = language;
			MLText.ForceRefresh();
			SetActorName.ForceRefresh();
		}
		Settings.cachedSettingsData = this.settingsData;
		Settings.hasCachedData = true;
		if (this.shaderVariables != null)
		{
			this.shaderVariables.UpdateVariables();
		}
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x0003BFEA File Offset: 0x0003A1EA
	private Vector2Int FindClosestResolution()
	{
		return new Vector2Int(0, 0);
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x0003BFF3 File Offset: 0x0003A1F3
	public void Write(string key, bool value)
	{
		if (this.settingsData.bools.ContainsKey(key))
		{
			this.settingsData.bools[key] = value;
			return;
		}
		this.settingsData.bools.Add(key, value);
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x0003C030 File Offset: 0x0003A230
	public bool ReadBool(string key, bool defaultValue = false)
	{
		bool flag;
		if (this.settingsData.bools.TryGetValue(key, out flag))
		{
			return flag;
		}
		this.settingsData.bools.Add(key, defaultValue);
		return defaultValue;
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x0003C067 File Offset: 0x0003A267
	public void Write(string key, int value)
	{
		if (this.settingsData.ints.ContainsKey(key))
		{
			this.settingsData.ints[key] = value;
			return;
		}
		this.settingsData.ints.Add(key, value);
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x0003C0A4 File Offset: 0x0003A2A4
	public int ReadInt(string key, int defaultValue = 0)
	{
		int num;
		if (this.settingsData.ints.TryGetValue(key, out num))
		{
			return num;
		}
		this.settingsData.ints.Add(key, defaultValue);
		return defaultValue;
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x0003C0DB File Offset: 0x0003A2DB
	public void Write(string key, float value)
	{
		this.Write(key, Settings.FloatToInt(value));
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x0003C0EA File Offset: 0x0003A2EA
	public float ReadFloat(string key, float defaultValue = 0f)
	{
		return Settings.IntToFloat(this.ReadInt(key, Settings.FloatToInt(defaultValue)));
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x0003C0FE File Offset: 0x0003A2FE
	public void Write(string key, string value)
	{
		if (this.settingsData.strings.ContainsKey(key))
		{
			this.settingsData.strings[key] = value;
			return;
		}
		this.settingsData.strings.Add(key, value);
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x0003C138 File Offset: 0x0003A338
	public string ReadString(string key)
	{
		if (!this.settingsData.strings.ContainsKey(key))
		{
			return "";
		}
		return this.settingsData.strings[key];
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x0003C164 File Offset: 0x0003A364
	public bool HasString(string key)
	{
		return this.settingsData.strings.ContainsKey(key);
	}

	// Token: 0x0400100D RID: 4109
	public static Language language;

	// Token: 0x0400100E RID: 4110
	public static float gameVolume;

	// Token: 0x0400100F RID: 4111
	private float heightmapPixelError = -1f;

	// Token: 0x04001010 RID: 4112
	public static bool hasMusic;

	// Token: 0x04001011 RID: 4113
	public static UnityEvent onHasMusicChanged = new UnityEvent();

	// Token: 0x04001012 RID: 4114
	private const float floatToIntScale = 1000f;

	// Token: 0x04001013 RID: 4115
	public static Vector2Int[] pixelResolutions;

	// Token: 0x04001014 RID: 4116
	public static int[] refreshRates;

	// Token: 0x04001015 RID: 4117
	public static Settings.SettingsData cachedSettingsData;

	// Token: 0x04001016 RID: 4118
	public static bool hasCachedData = false;

	// Token: 0x04001017 RID: 4119
	public Settings.SettingsData settingsData;

	// Token: 0x04001018 RID: 4120
	private static Settings instance;

	// Token: 0x04001019 RID: 4121
	public static float mouseSensitivity = 1f;

	// Token: 0x0400101A RID: 4122
	public static bool mouseInvertHorizontal = false;

	// Token: 0x0400101B RID: 4123
	public static bool mouseInvertVertical = false;

	// Token: 0x0400101C RID: 4124
	public static bool reCenterCameraIsToggle = false;

	// Token: 0x0400101D RID: 4125
	public AudioMixer volumeMixer;

	// Token: 0x0400101E RID: 4126
	public Terrain[] terrains = new Terrain[0];

	// Token: 0x0400101F RID: 4127
	public Light sunLight;

	// Token: 0x04001020 RID: 4128
	public GameObject postProcessVolumes;

	// Token: 0x04001021 RID: 4129
	public PostProcessProfile postProcessProfile;

	// Token: 0x04001022 RID: 4130
	private AmbientOcclusion ambientOcclusionSetting;

	// Token: 0x04001023 RID: 4131
	public GameObject fpsCounter;

	// Token: 0x04001024 RID: 4132
	public ShaderVariables shaderVariables;

	// Token: 0x04001025 RID: 4133
	public CameraLayerCullDistances cameraLayerCullDistances;

	// Token: 0x04001026 RID: 4134
	public GameObject skybox;

	// Token: 0x04001027 RID: 4135
	public Material[] leafMaterials;

	// Token: 0x04001028 RID: 4136
	public Material[] queueMaterials;

	// Token: 0x04001029 RID: 4137
	public Material[] instancingMaterials;

	// Token: 0x0400102A RID: 4138
	public Texture2D[] terrainTextures;

	// Token: 0x0400102B RID: 4139
	public DynamicResolutionScaler dynamicResolutionScaler;

	// Token: 0x0400102C RID: 4140
	protected bool initialized;

	// Token: 0x0400102D RID: 4141
	private bool willUpdateSettings;

	// Token: 0x0200041D RID: 1053
	[Serializable]
	public class SettingsData
	{
		// Token: 0x04001D33 RID: 7475
		public const int currentVersion = 12;

		// Token: 0x04001D34 RID: 7476
		public int v = 12;

		// Token: 0x04001D35 RID: 7477
		public BoolDictionary bools = new BoolDictionary();

		// Token: 0x04001D36 RID: 7478
		public IntDictionary ints = new IntDictionary();

		// Token: 0x04001D37 RID: 7479
		public StringDictionary strings = new StringDictionary();
	}
}
