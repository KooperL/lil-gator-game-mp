using System;
using System.Collections;
using System.IO;
using System.Text;
using Steamworks;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class FileUtil : MonoBehaviour
{
	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001DB93 File Offset: 0x0001BD93
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

	// Token: 0x060005A4 RID: 1444 RVA: 0x0001DBB1 File Offset: 0x0001BDB1
	public static bool IsSaveFileStarted(int index)
	{
		return FileUtil.gameSaveDataInfo[index].isStarted;
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x0001DBC3 File Offset: 0x0001BDC3
	public static bool IsSaveFileReal(int index)
	{
		return FileUtil.gameSaveDataInfo[index].isStarted || FileUtil.gameSaveDataInfo[index].newGameIndex != 0;
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x0001DBEC File Offset: 0x0001BDEC
	public static bool IsSaveFileCompleted(int index)
	{
		return FileUtil.gameSaveDataInfo[index].sis;
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x0001DBFE File Offset: 0x0001BDFE
	private static void WriteNewFile(string filePath, int fileSize, string contents)
	{
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0001DC00 File Offset: 0x0001BE00
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

	// Token: 0x060005A9 RID: 1449 RVA: 0x0001DC6C File Offset: 0x0001BE6C
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

	// Token: 0x060005AA RID: 1450 RVA: 0x0001DD30 File Offset: 0x0001BF30
	private void Awake()
	{
		if (FileUtil.instance == null)
		{
			FileUtil.instance = this;
		}
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x0001DD45 File Offset: 0x0001BF45
	private void Start()
	{
		FileUtil.Initialize();
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x0001DD4C File Offset: 0x0001BF4C
	private void OnDestroy()
	{
		if (this.isThisOneInitialized)
		{
			FileUtil.isInitialized = false;
		}
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x0001DD5C File Offset: 0x0001BF5C
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

	// Token: 0x060005AE RID: 1454 RVA: 0x0001DD9C File Offset: 0x0001BF9C
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

	// Token: 0x060005AF RID: 1455 RVA: 0x0001DDDA File Offset: 0x0001BFDA
	public static void CopyGameSaveData(int sourceIndex, int targetIndex)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		FileUtil.WriteSaveData(FileUtil.ReadSaveData(sourceIndex, false), targetIndex, false);
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x0001DDF6 File Offset: 0x0001BFF6
	public static void EraseGameSaveData(int index)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		FileUtil.WriteSaveData(FileUtil.Instance.newGameSave.gameSaveData, index, false);
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x0001DE1C File Offset: 0x0001C01C
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

	// Token: 0x060005B2 RID: 1458 RVA: 0x0001DF58 File Offset: 0x0001C158
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

	// Token: 0x060005B3 RID: 1459 RVA: 0x0001E00C File Offset: 0x0001C20C
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

	// Token: 0x060005B4 RID: 1460 RVA: 0x0001E088 File Offset: 0x0001C288
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

	// Token: 0x060005B5 RID: 1461 RVA: 0x0001E0E3 File Offset: 0x0001C2E3
	public static IEnumerator WriteSaveDataOverMultipleFrames(GameSaveData saveData, int index)
	{
		FileUtil.isWritingSaveDataOverMultipleFrames = true;
		FileUtil.Write(FileUtil.saveFilePaths[index], JsonUtility.ToJson(saveData), 65536, false, false);
		yield return null;
		FileUtil.UpdateSaveDataInfo(saveData, index, true);
		FileUtil.isWritingSaveDataOverMultipleFrames = false;
		yield break;
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x0001E0F9 File Offset: 0x0001C2F9
	public static void UpdateSaveDataInfo(GameSaveData saveData, int index, bool commit = true)
	{
		FileUtil.gameSaveDataInfo[index] = new GameSaveDataInfo(saveData);
		FileUtil.Write(FileUtil.infoFilePaths[index], JsonUtility.ToJson(FileUtil.gameSaveDataInfo[index]), 1024, commit, false);
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0001E134 File Offset: 0x0001C334
	public static void UpdateSaveDataInfo(int index, bool commit = true)
	{
		FileUtil.gameSaveDataInfo[index] = default(GameSaveDataInfo);
		FileUtil.Write(FileUtil.infoFilePaths[index], JsonUtility.ToJson(FileUtil.gameSaveDataInfo[index]), 1024, commit, false);
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x0001E170 File Offset: 0x0001C370
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

	// Token: 0x060005B9 RID: 1465 RVA: 0x0001E20C File Offset: 0x0001C40C
	public static void WriteSettingsData(Settings.SettingsData settingsData)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		FileUtil.Write(FileUtil.settingsFilePath, JsonUtility.ToJson(settingsData), 131072, true, true);
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0001E231 File Offset: 0x0001C431
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

	// Token: 0x060005BB RID: 1467 RVA: 0x0001E258 File Offset: 0x0001C458
	private static bool DoesSaveFileExist(int index)
	{
		if (!FileUtil.isInitialized)
		{
			FileUtil.Initialize();
		}
		return FileUtil.DoesFileExist(FileUtil.saveFilePaths[index], false);
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0001E274 File Offset: 0x0001C474
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

	// Token: 0x060005BD RID: 1469 RVA: 0x0001E2E8 File Offset: 0x0001C4E8
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

	// Token: 0x060005BE RID: 1470 RVA: 0x0001E37C File Offset: 0x0001C57C
	private static void OnRemoteStorageFileWriteAsyncComplete(RemoteStorageFileWriteAsyncComplete_t m_eResult, bool bIOFailure)
	{
	}

	// Token: 0x060005BF RID: 1471 RVA: 0x0001E37E File Offset: 0x0001C57E
	public static void Commit()
	{
	}

	// Token: 0x040007C5 RID: 1989
	private static FileUtil instance;

	// Token: 0x040007C6 RID: 1990
	private const int saveFileSize = 65536;

	// Token: 0x040007C7 RID: 1991
	private const int saveFileInfoSize = 1024;

	// Token: 0x040007C8 RID: 1992
	private const int settingsFileSize = 131072;

	// Token: 0x040007C9 RID: 1993
	public static string saveFilePath;

	// Token: 0x040007CA RID: 1994
	public static string saveFilePath2;

	// Token: 0x040007CB RID: 1995
	public static string saveFilePath3;

	// Token: 0x040007CC RID: 1996
	public static string[] saveFilePaths;

	// Token: 0x040007CD RID: 1997
	public static string infoFilePath;

	// Token: 0x040007CE RID: 1998
	public static string infoFilePath2;

	// Token: 0x040007CF RID: 1999
	public static string infoFilePath3;

	// Token: 0x040007D0 RID: 2000
	public static string[] infoFilePaths;

	// Token: 0x040007D1 RID: 2001
	public static string settingsFilePath;

	// Token: 0x040007D2 RID: 2002
	private const bool mustCreateFile = false;

	// Token: 0x040007D3 RID: 2003
	private static CallResult<RemoteStorageFileWriteAsyncComplete_t> OnRemoteStorageFileWriteAsyncCompleteCallResult;

	// Token: 0x040007D4 RID: 2004
	private static float lastSaveDataLoad = -100f;

	// Token: 0x040007D5 RID: 2005
	private static GameSaveData cachedSaveData;

	// Token: 0x040007D6 RID: 2006
	public static GameSaveDataInfo[] gameSaveDataInfo;

	// Token: 0x040007D7 RID: 2007
	private static float lastSettingsDataLoad = -100f;

	// Token: 0x040007D8 RID: 2008
	private static Settings.SettingsData cachedSettingsData;

	// Token: 0x040007D9 RID: 2009
	public static bool isInitialized = false;

	// Token: 0x040007DA RID: 2010
	private bool isThisOneInitialized;

	// Token: 0x040007DB RID: 2011
	public GameSaveWrapper newGameSave;

	// Token: 0x040007DC RID: 2012
	private static int asyncWriteCount = 0;

	// Token: 0x040007DD RID: 2013
	public static bool isApplicationQuitting = false;

	// Token: 0x040007DE RID: 2014
	[Header("New Game Plus")]
	public string[] ngpBools;

	// Token: 0x040007DF RID: 2015
	public string[] ngpInts;

	// Token: 0x040007E0 RID: 2016
	private static bool isWritingSaveDataOverMultipleFrames = false;
}
