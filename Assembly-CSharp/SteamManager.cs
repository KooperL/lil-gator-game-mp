using System;
using System.Text;
using AOT;
using Steamworks;
using UnityEngine;

[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	// (get) Token: 0x06000214 RID: 532 RVA: 0x00003BA1 File Offset: 0x00001DA1
	protected static SteamManager Instance
	{
		get
		{
			if (SteamManager.s_instance == null)
			{
				return new GameObject("SteamManager").AddComponent<SteamManager>();
			}
			return SteamManager.s_instance;
		}
	}

	// (get) Token: 0x06000215 RID: 533 RVA: 0x00003BC5 File Offset: 0x00001DC5
	public static bool Initialized
	{
		get
		{
			return (!SteamManager.s_EverInitialized || !(SteamManager.s_instance == null)) && SteamManager.Instance.m_bInitialized;
		}
	}

	// Token: 0x06000216 RID: 534 RVA: 0x00003BE7 File Offset: 0x00001DE7
	public static void ForceInitialize()
	{
		SteamManager.Instance.Initialize();
	}

	// Token: 0x06000217 RID: 535 RVA: 0x00003BF3 File Offset: 0x00001DF3
	[MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
	protected static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		Debug.LogWarning(pchDebugText);
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00003BFB File Offset: 0x00001DFB
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void InitOnPlayMode()
	{
		SteamManager.s_EverInitialized = false;
		SteamManager.s_instance = null;
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0001E738 File Offset: 0x0001C938
	protected void Initialize()
	{
		if (this.thisInitialized)
		{
			return;
		}
		this.thisInitialized = true;
		if (SteamManager.s_instance != null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		SteamManager.s_instance = this;
		if (SteamManager.s_EverInitialized)
		{
			throw new Exception("Tried to Initialize the SteamAPI twice in one session!");
		}
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		if (!Packsize.Test())
		{
			Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
		}
		if (!DllCheck.Test())
		{
			Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
		}
		try
		{
			if (SteamAPI.RestartAppIfNecessary(SteamManager.appID_t))
			{
				Application.Quit();
				return;
			}
		}
		catch (DllNotFoundException ex)
		{
			string text = "[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n";
			DllNotFoundException ex2 = ex;
			Debug.LogError(text + ((ex2 != null) ? ex2.ToString() : null), this);
			Application.Quit();
			return;
		}
		this.m_bInitialized = SteamAPI.Init();
		if (!this.m_bInitialized)
		{
			Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
			return;
		}
		SteamManager.s_EverInitialized = true;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x00003C09 File Offset: 0x00001E09
	protected virtual void Awake()
	{
		this.Initialize();
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0001E828 File Offset: 0x0001CA28
	protected virtual void OnEnable()
	{
		if (SteamManager.s_instance == null)
		{
			SteamManager.s_instance = this;
		}
		if (!this.m_bInitialized)
		{
			return;
		}
		if (this.m_SteamAPIWarningMessageHook == null)
		{
			this.m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook);
			SteamClient.SetWarningMessageHook(this.m_SteamAPIWarningMessageHook);
		}
	}

	// Token: 0x0600021C RID: 540 RVA: 0x00003C11 File Offset: 0x00001E11
	protected virtual void OnDisable()
	{
		if (SteamManager.s_instance != this)
		{
			return;
		}
		SteamManager.s_instance = null;
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.Shutdown();
	}

	// Token: 0x0600021D RID: 541 RVA: 0x00003C35 File Offset: 0x00001E35
	protected virtual void Update()
	{
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.RunCallbacks();
	}

	protected static bool s_EverInitialized = false;

	protected static SteamManager s_instance;

	protected bool m_bInitialized;

	protected SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

	public const uint appID = 1586800U;

	public static readonly AppId_t appID_t = new AppId_t(1586800U);

	private bool thisInitialized;
}
