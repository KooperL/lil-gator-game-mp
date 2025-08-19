using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class Settings : MonoBehaviour
{
	// Token: 0x06000F47 RID: 3911 RVA: 0x0000D37D File Offset: 0x0000B57D
	public static float NormalizedAudioVolume(float flatVolume)
	{
		return Mathf.Log10(Mathf.Max(flatVolume, 1E-05f)) * 20f;
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x00007694 File Offset: 0x00005894
	public static int FloatToInt(float f)
	{
		return Mathf.FloorToInt(f * 1000f);
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x000076A2 File Offset: 0x000058A2
	public static float IntToFloat(int i)
	{
		return (float)i / 1000f;
	}

	// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00050448 File Offset: 0x0004E648
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

	// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00050488 File Offset: 0x0004E688
	// (set) Token: 0x06000F4C RID: 3916 RVA: 0x0000D395 File Offset: 0x0000B595
	public static Settings s
	{
		get
		{
			if (Settings.instance == null)
			{
				Settings.instance = global::UnityEngine.Object.FindObjectOfType<Settings>();
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

	// Token: 0x06000F4D RID: 3917 RVA: 0x000504D4 File Offset: 0x0004E6D4
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

	// Token: 0x06000F4E RID: 3918 RVA: 0x0000D39D File Offset: 0x0000B59D
	private void OnEnable()
	{
		this.LoadSettings();
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x0000D39D File Offset: 0x0000B59D
	private void Start()
	{
		this.LoadSettings();
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x0000D3A5 File Offset: 0x0000B5A5
	private void OnDisable()
	{
		if (Settings.instance == this)
		{
			this.WriteToDisk();
		}
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x0000D3A5 File Offset: 0x0000B5A5
	private void OnApplicationQuit()
	{
		if (Settings.instance == this)
		{
			this.WriteToDisk();
		}
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x00050608 File Offset: 0x0004E808
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

	// Token: 0x06000F53 RID: 3923 RVA: 0x0000D3BA File Offset: 0x0000B5BA
	public void WriteToDisk()
	{
		this.settingsData.v = 12;
		FileUtil.WriteSettingsData(this.settingsData);
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
	private IEnumerator LoadSettingsNextFrame()
	{
		yield return null;
		this.willUpdateSettings = false;
		this.LoadSettings();
		yield break;
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x00050658 File Offset: 0x0004E858
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

	// Token: 0x06000F56 RID: 3926 RVA: 0x0000D3E3 File Offset: 0x0000B5E3
	private Vector2Int FindClosestResolution()
	{
		return new Vector2Int(0, 0);
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x0000D3EC File Offset: 0x0000B5EC
	public void Write(string key, bool value)
	{
		if (this.settingsData.bools.ContainsKey(key))
		{
			this.settingsData.bools[key] = value;
			return;
		}
		this.settingsData.bools.Add(key, value);
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x000511D0 File Offset: 0x0004F3D0
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

	// Token: 0x06000F59 RID: 3929 RVA: 0x0000D426 File Offset: 0x0000B626
	public void Write(string key, int value)
	{
		if (this.settingsData.ints.ContainsKey(key))
		{
			this.settingsData.ints[key] = value;
			return;
		}
		this.settingsData.ints.Add(key, value);
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x00051208 File Offset: 0x0004F408
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

	// Token: 0x06000F5B RID: 3931 RVA: 0x0000D460 File Offset: 0x0000B660
	public void Write(string key, float value)
	{
		this.Write(key, Settings.FloatToInt(value));
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x0000D46F File Offset: 0x0000B66F
	public float ReadFloat(string key, float defaultValue = 0f)
	{
		return Settings.IntToFloat(this.ReadInt(key, Settings.FloatToInt(defaultValue)));
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x0000D483 File Offset: 0x0000B683
	public void Write(string key, string value)
	{
		if (this.settingsData.strings.ContainsKey(key))
		{
			this.settingsData.strings[key] = value;
			return;
		}
		this.settingsData.strings.Add(key, value);
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x0000D4BD File Offset: 0x0000B6BD
	public string ReadString(string key)
	{
		if (!this.settingsData.strings.ContainsKey(key))
		{
			return "";
		}
		return this.settingsData.strings[key];
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x0000D4E9 File Offset: 0x0000B6E9
	public bool HasString(string key)
	{
		return this.settingsData.strings.ContainsKey(key);
	}

	public static Language language;

	public static float gameVolume;

	private float heightmapPixelError = -1f;

	public static bool hasMusic;

	public static UnityEvent onHasMusicChanged = new UnityEvent();

	private const float floatToIntScale = 1000f;

	public static Vector2Int[] pixelResolutions;

	public static int[] refreshRates;

	public static Settings.SettingsData cachedSettingsData;

	public static bool hasCachedData = false;

	public Settings.SettingsData settingsData;

	private static Settings instance;

	public static float mouseSensitivity = 1f;

	public static bool mouseInvertHorizontal = false;

	public static bool mouseInvertVertical = false;

	public static bool reCenterCameraIsToggle = false;

	public AudioMixer volumeMixer;

	public Terrain[] terrains = new Terrain[0];

	public Light sunLight;

	public GameObject postProcessVolumes;

	public PostProcessProfile postProcessProfile;

	private AmbientOcclusion ambientOcclusionSetting;

	public GameObject fpsCounter;

	public ShaderVariables shaderVariables;

	public CameraLayerCullDistances cameraLayerCullDistances;

	public GameObject skybox;

	public Material[] leafMaterials;

	public Material[] queueMaterials;

	public Material[] instancingMaterials;

	public Texture2D[] terrainTextures;

	public DynamicResolutionScaler dynamicResolutionScaler;

	protected bool initialized;

	private bool willUpdateSettings;

	[Serializable]
	public class SettingsData
	{
		public const int currentVersion = 12;

		public int v = 12;

		public BoolDictionary bools = new BoolDictionary();

		public IntDictionary ints = new IntDictionary();

		public StringDictionary strings = new StringDictionary();
	}
}
