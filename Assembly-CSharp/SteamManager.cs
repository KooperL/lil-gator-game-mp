using System;
using System.Text;
using AOT;
using Steamworks;
using UnityEngine;

// Token: 0x02000094 RID: 148
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000207 RID: 519 RVA: 0x00003AB5 File Offset: 0x00001CB5
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

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000208 RID: 520 RVA: 0x00003AD9 File Offset: 0x00001CD9
	public static bool Initialized
	{
		get
		{
			return (!SteamManager.s_EverInitialized || !(SteamManager.s_instance == null)) && SteamManager.Instance.m_bInitialized;
		}
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00003AFB File Offset: 0x00001CFB
	public static void ForceInitialize()
	{
		SteamManager.Instance.Initialize();
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00003B07 File Offset: 0x00001D07
	[MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
	protected static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		Debug.LogWarning(pchDebugText);
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00003B0F File Offset: 0x00001D0F
	[RuntimeInitializeOnLoadMethod(4)]
	private static void InitOnPlayMode()
	{
		SteamManager.s_EverInitialized = false;
		SteamManager.s_instance = null;
	}

	// Token: 0x0600020C RID: 524 RVA: 0x0001DCE0 File Offset: 0x0001BEE0
	protected void Initialize()
	{
		if (this.thisInitialized)
		{
			return;
		}
		this.thisInitialized = true;
		if (SteamManager.s_instance != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		SteamManager.s_instance = this;
		if (SteamManager.s_EverInitialized)
		{
			throw new Exception("Tried to Initialize the SteamAPI twice in one session!");
		}
		Object.DontDestroyOnLoad(base.gameObject);
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

	// Token: 0x0600020D RID: 525 RVA: 0x00003B1D File Offset: 0x00001D1D
	protected virtual void Awake()
	{
		this.Initialize();
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0001DDD0 File Offset: 0x0001BFD0
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

	// Token: 0x0600020F RID: 527 RVA: 0x00003B25 File Offset: 0x00001D25
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

	// Token: 0x06000210 RID: 528 RVA: 0x00003B49 File Offset: 0x00001D49
	protected virtual void Update()
	{
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.RunCallbacks();
	}

	// Token: 0x040002EF RID: 751
	protected static bool s_EverInitialized = false;

	// Token: 0x040002F0 RID: 752
	protected static SteamManager s_instance;

	// Token: 0x040002F1 RID: 753
	protected bool m_bInitialized;

	// Token: 0x040002F2 RID: 754
	protected SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

	// Token: 0x040002F3 RID: 755
	public const uint appID = 1586800U;

	// Token: 0x040002F4 RID: 756
	public static readonly AppId_t appID_t = new AppId_t(1586800U);

	// Token: 0x040002F5 RID: 757
	private bool thisInitialized;
}
