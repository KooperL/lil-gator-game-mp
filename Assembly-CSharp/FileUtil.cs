using System;
using System.Collections;
using System.IO;
using System.Text;
using Steamworks;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class FileUtil : MonoBehaviour
{
	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00006EE2 File Offset: 0x000050E2
	private static FileUtil Instance
	{
		get
		{
			if (FileUtil.instance == null)
			{
				FileUtil.instance = Object.FindObjectOfType<FileUtil>();
			}
			return FileUtil.instance;
		}
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x00006F00 File Offset: 0x00005100
	public static bool IsSaveFileStarted(int index)
	{
		return FileUtil.gameSaveDataInfo[index].isStarted;
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00002229 File Offset: 0x00000429
	private static void WriteNewFile(string filePath, int fileSize, string contents)
	{
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x00031F40 File Offset: 0x00030140
	private static void CreateFilePathArrays()
	{
		FileUtil.saveFilePaths = new string[3];
		FileUtil.infoFilePaths = new string[3];
		FileUtil.saveFilePaths[0] = FileUtil.saveFilePath;
		FileUtil.saveFilePaths[1] = FileUtil.saveFilePath2;
		FileUtil.saveFilePaths[2] = FileUtil.saveFilePath3;
		FileUtil.infoFilePaths[0] = FileUtil.infoFilePath;
		FileUtil.infoFilePaths[1] = FileUtil.infoFilePath2;
		FileUtil.infoFilePaths[2] = FileUtil.infoFilePath3;
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x00031FAC File Offset: 0x000301AC
	public static void Initialize()
	{
		if (FileUtil.isInitialized)
		{
			return;
		}
		if (FileUtil.Instance == null)
		{
			return;
		}
		FileUtil.Instance.isThisOneInitialized = true;
		FileUtil.isInitialized = true;
		FileUtil.gameSaveDataInfo = new GameSaveDataInfo[3];
		SteamManager.ForceInitialize();
		FileUtil.saveFilePath = "saveFile";
		FileUtil.saveFilePath2 = "saveFile2";
		FileUtil.saveFilePath3 = "saveFile3";
		FileUtil.infoFilePath = "saveFileInfo";
		FileUtil.infoFilePath2 = "saveFile2Info";
		FileUtil.infoFilePath3 = "saveFile3Info";
		FileUtil.CreateFilePathArrays();
		FileUtil.settingsFilePath = Path.Combine(Application.persistentDataPath, "Settings");
		FileUtil.OnRemoteStorageFileWriteAsyncCompleteCallResult = CallResult<RemoteStorageFileWriteAsyncComplete_t>.Create(new CallResult<RemoteStorageFileWriteAsyncComplete_t>.APIDispatchDelegate(FileUtil.OnRemoteStorageFileWriteAsyncComplete));
		FileUtil.ReadGameSaveDataInfo(0);
		FileUtil.ReadGameSaveDataInfo(1);
		FileUtil.ReadGameSaveDataInfo(2);
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x00006F12 File Offset: 0x00005112
	private void Awake()
	{
		if (FileUtil.instance == null)
		{
			FileUtil.instance = this;
		}
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x00006F27 File Offset: 0x00005127
	private void Start()
	{
		FileUtil.Initialize();
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x00006F2E File Offset: 0x0000512E
	private void OnDestroy()
	{
		if (this.isThisOneInitialized)
		{
			FileUtil.isInitialized = false;
		}
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x00032070 File Offset: 0x00030270
	public static bool HasInitializedSaveData()
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		GameSaveDataInfo[] array = FileUtil.gameSaveDataInfo;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].isStarted)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x00006F3E File Offset: 0x0000513E
	public static void CopyGameSaveData(int sourceIndex, int targetIndex)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		FileUtil.WriteSaveData(FileUtil.ReadSaveData(sourceIndex, false), targetIndex, false);
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x00006F5A File Offset: 0x0000515A
	public static void EraseGameSaveData(int index)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		FileUtil.WriteSaveData(FileUtil.Instance.newGameSave.gameSaveData, index, false);
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x000320B0 File Offset: 0x000302B0
	private static GameSaveDataInfo ReadGameSaveDataInfo(int index)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		if (FileUtil.DoesFileExist(FileUtil.infoFilePaths[index], false))
		{
			try
			{
				string text = FileUtil.Read(FileUtil.infoFilePaths[index], 1024, false);
				FileUtil.gameSaveDataInfo[index] = JsonUtility.FromJson<GameSaveDataInfo>(text);
				return FileUtil.gameSaveDataInfo[index];
			}
			catch
			{
			}
		}
		if (FileUtil.DoesFileExist(FileUtil.saveFilePaths[index], false))
		{
			FileUtil.UpdateSaveDataInfo(FileUtil.ReadSaveData(index, false), index, true);
			return FileUtil.gameSaveDataInfo[index];
		}
		FileUtil.gameSaveDataInfo[index] = default(GameSaveDataInfo);
		return FileUtil.gameSaveDataInfo[index];
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x00032164 File Offset: 0x00030364
	public static GameSaveData ReadSaveData(int index = 0, bool forceFreshSave = false)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		if (FileUtil.DoesSaveFileExist(index))
		{
			try
			{
				FileUtil.cachedSaveData = JsonUtility.FromJson<GameSaveData>(FileUtil.Read(FileUtil.saveFilePaths[index], 65536, false));
				return FileUtil.cachedSaveData;
			}
			catch
			{
				FileUtil.cachedSaveData = new GameSaveData();
				return FileUtil.cachedSaveData;
			}
		}
		FileUtil.cachedSaveData = new GameSaveData();
		return FileUtil.cachedSaveData;
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x000321E0 File Offset: 0x000303E0
	public static void WriteSaveData(GameSaveData saveData, int index = 0, bool overMultipleFrames = false)
	{
		if (FileUtil.isWritingSaveDataOverMultipleFrames)
		{
			return;
		}
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		saveData.v = 10;
		if (overMultipleFrames)
		{
			CoroutineUtil.Start(FileUtil.WriteSaveDataOverMultipleFrames(saveData, index));
			return;
		}
		FileUtil.Write(FileUtil.saveFilePaths[index], JsonUtility.ToJson(saveData), 65536, false, false);
		FileUtil.UpdateSaveDataInfo(saveData, index, true);
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00006F7E File Offset: 0x0000517E
	public static IEnumerator WriteSaveDataOverMultipleFrames(GameSaveData saveData, int index)
	{
		FileUtil.isWritingSaveDataOverMultipleFrames = true;
		FileUtil.Write(FileUtil.saveFilePaths[index], JsonUtility.ToJson(saveData), 65536, false, false);
		yield return null;
		FileUtil.UpdateSaveDataInfo(saveData, index, true);
		FileUtil.isWritingSaveDataOverMultipleFrames = false;
		yield break;
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00006F94 File Offset: 0x00005194
	public static void UpdateSaveDataInfo(GameSaveData saveData, int index, bool commit = true)
	{
		FileUtil.gameSaveDataInfo[index] = new GameSaveDataInfo(saveData);
		FileUtil.Write(FileUtil.infoFilePaths[index], JsonUtility.ToJson(FileUtil.gameSaveDataInfo[index]), 1024, commit, false);
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00006FCF File Offset: 0x000051CF
	public static void UpdateSaveDataInfo(int index, bool commit = true)
	{
		FileUtil.gameSaveDataInfo[index] = default(GameSaveDataInfo);
		FileUtil.Write(FileUtil.infoFilePaths[index], JsonUtility.ToJson(FileUtil.gameSaveDataInfo[index]), 1024, commit, false);
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0003223C File Offset: 0x0003043C
	public static Settings.SettingsData ReadSettingsData()
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		if (FileUtil.cachedSettingsData != null && Time.time - FileUtil.lastSettingsDataLoad < 10f)
		{
			return FileUtil.cachedSettingsData;
		}
		if (FileUtil.DoesFileExist(FileUtil.settingsFilePath, true))
		{
			try
			{
				FileUtil.cachedSettingsData = JsonUtility.FromJson<Settings.SettingsData>(FileUtil.Read(FileUtil.settingsFilePath, 131072, true));
				return FileUtil.cachedSettingsData;
			}
			catch
			{
				FileUtil.cachedSettingsData = new Settings.SettingsData();
				return FileUtil.cachedSettingsData;
			}
		}
		FileUtil.cachedSettingsData = new Settings.SettingsData();
		return FileUtil.cachedSettingsData;
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x0000700A File Offset: 0x0000520A
	public static void WriteSettingsData(Settings.SettingsData settingsData)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		FileUtil.Write(FileUtil.settingsFilePath, JsonUtility.ToJson(settingsData), 131072, true, true);
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0000702F File Offset: 0x0000522F
	private static bool DoesFileExist(string filePath, bool forceLocal = false)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		if (forceLocal)
		{
			return File.Exists(filePath);
		}
		return SteamManager.Initialized && SteamRemoteStorage.FileExists(filePath);
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00007056 File Offset: 0x00005256
	private static bool DoesSaveFileExist(int index)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		return FileUtil.DoesFileExist(FileUtil.saveFilePaths[index], false);
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x000322D8 File Offset: 0x000304D8
	private static string Read(string path, int fileSize, bool forceLocal = false)
	{
		if (forceLocal)
		{
			using (BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open)))
			{
				return binaryReader.ReadString();
			}
		}
		if (!SteamManager.Initialized)
		{
			return "";
		}
		byte[] array = new byte[SteamRemoteStorage.GetFileSize(path)];
		int num = SteamRemoteStorage.FileRead(path, array, array.Length);
		return Encoding.UTF8.GetString(array, 0, num);
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0003234C File Offset: 0x0003054C
	private static void Write(string path, string contents, int fileSize, bool commit, bool forceLocal = false)
	{
		if (forceLocal)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.Create)))
			{
				binaryWriter.Write(contents);
				goto IL_0071;
			}
		}
		if (!SteamManager.Initialized)
		{
			return;
		}
		byte[] array = new byte[Encoding.UTF8.GetByteCount(contents)];
		Encoding.UTF8.GetBytes(contents, 0, contents.Length, array, 0);
		if (FileUtil.isApplicationQuitting)
		{
			SteamRemoteStorage.FileWrite(path, array, array.Length);
		}
		else
		{
			SteamRemoteStorage.FileWriteAsync(path, array, (uint)array.Length);
		}
		IL_0071:
		UISaveIcon.ShowIcon();
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x00002229 File Offset: 0x00000429
	private static void OnRemoteStorageFileWriteAsyncComplete(RemoteStorageFileWriteAsyncComplete_t m_eResult, bool bIOFailure)
	{
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00002229 File Offset: 0x00000429
	public static void Commit()
	{
	}

	// Token: 0x0400091F RID: 2335
	private static FileUtil instance;

	// Token: 0x04000920 RID: 2336
	private const int saveFileSize = 65536;

	// Token: 0x04000921 RID: 2337
	private const int saveFileInfoSize = 1024;

	// Token: 0x04000922 RID: 2338
	private const int settingsFileSize = 131072;

	// Token: 0x04000923 RID: 2339
	public static string saveFilePath;

	// Token: 0x04000924 RID: 2340
	public static string saveFilePath2;

	// Token: 0x04000925 RID: 2341
	public static string saveFilePath3;

	// Token: 0x04000926 RID: 2342
	public static string[] saveFilePaths;

	// Token: 0x04000927 RID: 2343
	public static string infoFilePath;

	// Token: 0x04000928 RID: 2344
	public static string infoFilePath2;

	// Token: 0x04000929 RID: 2345
	public static string infoFilePath3;

	// Token: 0x0400092A RID: 2346
	public static string[] infoFilePaths;

	// Token: 0x0400092B RID: 2347
	public static string settingsFilePath;

	// Token: 0x0400092C RID: 2348
	private const bool mustCreateFile = false;

	// Token: 0x0400092D RID: 2349
	private static CallResult<RemoteStorageFileWriteAsyncComplete_t> OnRemoteStorageFileWriteAsyncCompleteCallResult;

	// Token: 0x0400092E RID: 2350
	private static float lastSaveDataLoad = -100f;

	// Token: 0x0400092F RID: 2351
	private static GameSaveData cachedSaveData;

	// Token: 0x04000930 RID: 2352
	public static GameSaveDataInfo[] gameSaveDataInfo;

	// Token: 0x04000931 RID: 2353
	private static float lastSettingsDataLoad = -100f;

	// Token: 0x04000932 RID: 2354
	private static Settings.SettingsData cachedSettingsData;

	// Token: 0x04000933 RID: 2355
	public static bool isInitialized = false;

	// Token: 0x04000934 RID: 2356
	private bool isThisOneInitialized;

	// Token: 0x04000935 RID: 2357
	public GameSaveWrapper newGameSave;

	// Token: 0x04000936 RID: 2358
	private static int asyncWriteCount = 0;

	// Token: 0x04000937 RID: 2359
	public static bool isApplicationQuitting = false;

	// Token: 0x04000938 RID: 2360
	private static bool isWritingSaveDataOverMultipleFrames = false;
}
