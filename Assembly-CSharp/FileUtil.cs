using System;
using System.Collections;
using System.IO;
using System.Text;
using Steamworks;
using UnityEngine;

public class FileUtil : MonoBehaviour
{
	// (get) Token: 0x06000701 RID: 1793 RVA: 0x000071A8 File Offset: 0x000053A8
	private static FileUtil Instance
	{
		get
		{
			if (FileUtil.instance == null)
			{
				FileUtil.instance = global::UnityEngine.Object.FindObjectOfType<FileUtil>();
			}
			return FileUtil.instance;
		}
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x000071C6 File Offset: 0x000053C6
	public static bool IsSaveFileStarted(int index)
	{
		return FileUtil.gameSaveDataInfo[index].isStarted;
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x000071D8 File Offset: 0x000053D8
	public static bool IsSaveFileReal(int index)
	{
		return FileUtil.gameSaveDataInfo[index].isStarted || FileUtil.gameSaveDataInfo[index].newGameIndex != 0;
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x00007201 File Offset: 0x00005401
	public static bool IsSaveFileCompleted(int index)
	{
		return FileUtil.gameSaveDataInfo[index].sis;
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x00002229 File Offset: 0x00000429
	private static void WriteNewFile(string filePath, int fileSize, string contents)
	{
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x00033650 File Offset: 0x00031850
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

	// Token: 0x06000707 RID: 1799 RVA: 0x000336BC File Offset: 0x000318BC
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

	// Token: 0x06000708 RID: 1800 RVA: 0x00007213 File Offset: 0x00005413
	private void Awake()
	{
		if (FileUtil.instance == null)
		{
			FileUtil.instance = this;
		}
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x00007228 File Offset: 0x00005428
	private void Start()
	{
		FileUtil.Initialize();
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0000722F File Offset: 0x0000542F
	private void OnDestroy()
	{
		if (this.isThisOneInitialized)
		{
			FileUtil.isInitialized = false;
		}
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00033780 File Offset: 0x00031980
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

	// Token: 0x0600070C RID: 1804 RVA: 0x000337C0 File Offset: 0x000319C0
	public static bool HasCompletedSaveData()
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		GameSaveDataInfo[] array = FileUtil.gameSaveDataInfo;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].sis)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0000723F File Offset: 0x0000543F
	public static void CopyGameSaveData(int sourceIndex, int targetIndex)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		FileUtil.WriteSaveData(FileUtil.ReadSaveData(sourceIndex, false), targetIndex, false);
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0000725B File Offset: 0x0000545B
	public static void EraseGameSaveData(int index)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		FileUtil.WriteSaveData(FileUtil.Instance.newGameSave.gameSaveData, index, false);
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x00033800 File Offset: 0x00031A00
	public static void CreateNewGamePlusSaveData(int sourceIndex, int targetIndex)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		GameSaveData gameSaveData = FileUtil.ReadSaveData(sourceIndex, false);
		GameSaveData gameSaveData2 = FileUtil.instance.newGameSave.gameSaveData.Clone();
		foreach (string text in FileUtil.instance.ngpBools)
		{
			bool flag;
			if (gameSaveData.bools.TryGetValue(text, out flag))
			{
				if (gameSaveData2.bools.ContainsKey(text))
				{
					gameSaveData2.bools[text] = flag;
				}
				else
				{
					gameSaveData2.bools.Add(text, flag);
				}
			}
		}
		foreach (string text2 in FileUtil.instance.ngpInts)
		{
			int num;
			if (gameSaveData.ints.TryGetValue(text2, out num))
			{
				if (gameSaveData2.ints.ContainsKey(text2))
				{
					gameSaveData2.ints[text2] = num;
				}
				else
				{
					gameSaveData2.ints.Add(text2, num);
				}
			}
		}
		int num2 = 1;
		if (gameSaveData2.ints.ContainsKey("NewGameIndex"))
		{
			gameSaveData2.ints["NewGameIndex"] = num2;
		}
		else
		{
			gameSaveData2.ints.Add("NewGameIndex", num2);
		}
		FileUtil.WriteSaveData(gameSaveData2, targetIndex, false);
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0003393C File Offset: 0x00031B3C
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

	// Token: 0x06000711 RID: 1809 RVA: 0x000339F0 File Offset: 0x00031BF0
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

	// Token: 0x06000712 RID: 1810 RVA: 0x00033A6C File Offset: 0x00031C6C
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

	// Token: 0x06000713 RID: 1811 RVA: 0x0000727F File Offset: 0x0000547F
	public static IEnumerator WriteSaveDataOverMultipleFrames(GameSaveData saveData, int index)
	{
		FileUtil.isWritingSaveDataOverMultipleFrames = true;
		FileUtil.Write(FileUtil.saveFilePaths[index], JsonUtility.ToJson(saveData), 65536, false, false);
		yield return null;
		FileUtil.UpdateSaveDataInfo(saveData, index, true);
		FileUtil.isWritingSaveDataOverMultipleFrames = false;
		yield break;
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x00007295 File Offset: 0x00005495
	public static void UpdateSaveDataInfo(GameSaveData saveData, int index, bool commit = true)
	{
		FileUtil.gameSaveDataInfo[index] = new GameSaveDataInfo(saveData);
		FileUtil.Write(FileUtil.infoFilePaths[index], JsonUtility.ToJson(FileUtil.gameSaveDataInfo[index]), 1024, commit, false);
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x000072D0 File Offset: 0x000054D0
	public static void UpdateSaveDataInfo(int index, bool commit = true)
	{
		FileUtil.gameSaveDataInfo[index] = default(GameSaveDataInfo);
		FileUtil.Write(FileUtil.infoFilePaths[index], JsonUtility.ToJson(FileUtil.gameSaveDataInfo[index]), 1024, commit, false);
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x00033AC8 File Offset: 0x00031CC8
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

	// Token: 0x06000717 RID: 1815 RVA: 0x0000730B File Offset: 0x0000550B
	public static void WriteSettingsData(Settings.SettingsData settingsData)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		FileUtil.Write(FileUtil.settingsFilePath, JsonUtility.ToJson(settingsData), 131072, true, true);
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x00007330 File Offset: 0x00005530
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

	// Token: 0x06000719 RID: 1817 RVA: 0x00007357 File Offset: 0x00005557
	private static bool DoesSaveFileExist(int index)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		return FileUtil.DoesFileExist(FileUtil.saveFilePaths[index], false);
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x00033B64 File Offset: 0x00031D64
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

	// Token: 0x0600071B RID: 1819 RVA: 0x00033BD8 File Offset: 0x00031DD8
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

	// Token: 0x0600071C RID: 1820 RVA: 0x00002229 File Offset: 0x00000429
	private static void OnRemoteStorageFileWriteAsyncComplete(RemoteStorageFileWriteAsyncComplete_t m_eResult, bool bIOFailure)
	{
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x00002229 File Offset: 0x00000429
	public static void Commit()
	{
	}

	private static FileUtil instance;

	private const int saveFileSize = 65536;

	private const int saveFileInfoSize = 1024;

	private const int settingsFileSize = 131072;

	public static string saveFilePath;

	public static string saveFilePath2;

	public static string saveFilePath3;

	public static string[] saveFilePaths;

	public static string infoFilePath;

	public static string infoFilePath2;

	public static string infoFilePath3;

	public static string[] infoFilePaths;

	public static string settingsFilePath;

	private const bool mustCreateFile = false;

	private static CallResult<RemoteStorageFileWriteAsyncComplete_t> OnRemoteStorageFileWriteAsyncCompleteCallResult;

	private static float lastSaveDataLoad = -100f;

	private static GameSaveData cachedSaveData;

	public static GameSaveDataInfo[] gameSaveDataInfo;

	private static float lastSettingsDataLoad = -100f;

	private static Settings.SettingsData cachedSettingsData;

	public static bool isInitialized = false;

	private bool isThisOneInitialized;

	public GameSaveWrapper newGameSave;

	private static int asyncWriteCount = 0;

	public static bool isApplicationQuitting = false;

	[Header("New Game Plus")]
	public string[] ngpBools;

	public string[] ngpInts;

	private static bool isWritingSaveDataOverMultipleFrames = false;
}
