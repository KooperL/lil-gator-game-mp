using System;
using System.Text;
using AOT;
using Steamworks;
using UnityEngine;

// Token: 0x02000073 RID: 115
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060001D0 RID: 464 RVA: 0x00009E99 File Offset: 0x00008099
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

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x060001D1 RID: 465 RVA: 0x00009EBD File Offset: 0x000080BD
	public static bool Initialized
	{
		get
		{
			return (!SteamManager.s_EverInitialized || !(SteamManager.s_instance == null)) && SteamManager.Instance.m_bInitialized;
		}
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x00009EDF File Offset: 0x000080DF
	public static void ForceInitialize()
	{
		SteamManager.Instance.Initialize();
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x00009EEB File Offset: 0x000080EB
	[MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
	protected static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		Debug.LogWarning(pchDebugText);
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00009EF3 File Offset: 0x000080F3
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void InitOnPlayMode()
	{
		SteamManager.s_EverInitialized = false;
		SteamManager.s_instance = null;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00009F04 File Offset: 0x00008104
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

	// Token: 0x060001D6 RID: 470 RVA: 0x00009FF4 File Offset: 0x000081F4
	protected virtual void Awake()
	{
		this.Initialize();
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x00009FFC File Offset: 0x000081FC
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

	// Token: 0x060001D8 RID: 472 RVA: 0x0000A04A File Offset: 0x0000824A
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

	// Token: 0x060001D9 RID: 473 RVA: 0x0000A06E File Offset: 0x0000826E
	protected virtual void Update()
	{
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.RunCallbacks();
	}

	// Token: 0x04000269 RID: 617
	protected static bool s_EverInitialized = false;

	// Token: 0x0400026A RID: 618
	protected static SteamManager s_instance;

	// Token: 0x0400026B RID: 619
	protected bool m_bInitialized;

	// Token: 0x0400026C RID: 620
	protected SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

	// Token: 0x0400026D RID: 621
	public const uint appID = 1586800U;

	// Token: 0x0400026E RID: 622
	public static readonly AppId_t appID_t = new AppId_t(1586800U);

	// Token: 0x0400026F RID: 623
	private bool thisInitialized;
}
