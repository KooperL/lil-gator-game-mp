using System;
using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
	// Token: 0x06000759 RID: 1881 RVA: 0x0000767F File Offset: 0x0000587F
	public static int FloatToInt(float f)
	{
		return Mathf.FloorToInt(f * 1000f);
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x0000768D File Offset: 0x0000588D
	public static float IntToFloat(int i)
	{
		return (float)i / 1000f;
	}

	// (get) Token: 0x0600075B RID: 1883 RVA: 0x00007697 File Offset: 0x00005897
	public static GameData g
	{
		get
		{
			if (GameData.instance == null && Application.isPlaying)
			{
				GameData.instance = global::UnityEngine.Object.FindObjectOfType<GameData>();
				if (GameData.instance != null)
				{
					GameData.instance.Awake();
				}
			}
			return GameData.instance;
		}
	}

	// (add) Token: 0x0600075C RID: 1884 RVA: 0x00034AFC File Offset: 0x00032CFC
	// (remove) Token: 0x0600075D RID: 1885 RVA: 0x00034B34 File Offset: 0x00032D34
	public event Action beforeSave;

	// (get) Token: 0x0600075E RID: 1886 RVA: 0x000076D3 File Offset: 0x000058D3
	public static string FilePath
	{
		get
		{
			return Path.Combine(Application.persistentDataPath, "saveFile");
		}
	}

	// (get) Token: 0x0600075F RID: 1887 RVA: 0x000076E4 File Offset: 0x000058E4
	public static string PlayerName
	{
		get
		{
			return GameData.g.gameSaveData.playerName;
		}
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x00034B6C File Offset: 0x00032D6C
	protected void Awake()
	{
		if (GameData.instance != null && GameData.instance != this)
		{
			return;
		}
		GameData.instance = this;
		this.savePath = GameData.FilePath;
		Game.onEnterDialogue += this.OnEnterDialogue;
		foreach (QuestProfile questProfile in QuestProfile.loadedQuestProfiles)
		{
			questProfile.ResetTasks();
		}
		if (GameData.hasPreloadedSave)
		{
			this.loadOnStart = true;
			this.save = true;
		}
		this.ReadFromDisk();
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x000076F5 File Offset: 0x000058F5
	private void OnEnable()
	{
		if (GameData.instance != null && GameData.instance != this)
		{
			return;
		}
		GameData.instance = this;
		this.savePath = GameData.FilePath;
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00007723 File Offset: 0x00005923
	private void OnDestroy()
	{
		Game.onEnterDialogue -= this.OnEnterDialogue;
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00007736 File Offset: 0x00005936
	private void OnApplicationQuit()
	{
		FileUtil.isApplicationQuitting = true;
		if (Game.AllowedToSave)
		{
			this.WriteToDisk();
		}
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x0000774B File Offset: 0x0000594B
	private void Save()
	{
		if (!Game.AllowedToSave)
		{
			return;
		}
		this.WriteToDisk();
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x0000775B File Offset: 0x0000595B
	public void SetSaveFile(int saveFileIndex)
	{
		this.saveFileSlot = saveFileIndex;
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x00007764 File Offset: 0x00005964
	public void LoadSaveFile(int saveFileIndex)
	{
		this.saveFileSlot = saveFileIndex;
		this.ReadFromDisk();
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x00007773 File Offset: 0x00005973
	public void WriteToDisk()
	{
		this.WriteToDisk(false);
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00034C14 File Offset: 0x00032E14
	public void WriteToDisk(bool isAutoSave)
	{
		if (!this.save)
		{
			return;
		}
		if (this.beforeSave != null)
		{
			this.beforeSave();
		}
		FileUtil.WriteSaveData(this.gameSaveData, this.saveFileSlot, isAutoSave);
		this.gameSaveWrapper.gameSaveData = this.gameSaveData;
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x00034C60 File Offset: 0x00032E60
	public void ReadFromDisk()
	{
		GameSaveData gameSaveData = FileUtil.ReadSaveData(this.saveFileSlot, false);
		if (gameSaveData != null)
		{
			this.gameSaveData = gameSaveData;
		}
		if (this.gameSaveData == null)
		{
			this.gameSaveData = new GameSaveData();
		}
		this.gameSaveWrapper.gameSaveData = this.gameSaveData;
		this.RefreshLoadedData();
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00034CB0 File Offset: 0x00032EB0
	private void RefreshLoadedData()
	{
		foreach (QuestProfile questProfile in QuestProfile.loadedQuestProfiles)
		{
			questProfile.Load();
		}
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00034D00 File Offset: 0x00032F00
	public void LoadSaveData(GameSaveData gameSaveData)
	{
		GameSaveData gameSaveData2 = gameSaveData.Clone();
		this.gameSaveWrapper.gameSaveData = gameSaveData2;
		this.gameSaveData = gameSaveData2;
		this.WriteToDisk();
		this.RefreshLoadedData();
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x00034D34 File Offset: 0x00032F34
	private void Update()
	{
		bool flag = false;
		if (Game.AllowedToSave)
		{
			this.canSaveCounter++;
			if (Time.time - this.lastAutoSaveTime > 5f)
			{
				if (Game.HasControl)
				{
					this.wasInControl = true;
					flag = Time.time - this.lastAutoSaveTime >= 30f;
				}
				else
				{
					flag = this.lastAutoSaveInControl || this.wasInControl;
				}
			}
			if (flag)
			{
				this.canAutoSaveCounter++;
			}
			else
			{
				this.canAutoSaveCounter = 0;
			}
			if (this.canAutoSaveCounter > 20)
			{
				this.canAutoSaveCounter = 0;
				if (Game.AllowedToSave && this.save)
				{
					this.WriteToDisk(true);
				}
				this.lastAutoSaveTime = Time.time;
				this.lastAutoSaveInControl = Game.HasControl;
				this.wasSaveRestricted = false;
				this.wasInControl = false;
			}
			return;
		}
		this.wasSaveRestricted = true;
		if (this.canSaveCounter > 0)
		{
			this.canSaveCounter = -1;
			return;
		}
		this.canSaveCounter--;
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x00002229 File Offset: 0x00000429
	private void OnEnterDialogue()
	{
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x0000777C File Offset: 0x0000597C
	public void Write(string key, bool value)
	{
		if (this.gameSaveData.bools.ContainsKey(key))
		{
			this.gameSaveData.bools[key] = value;
			return;
		}
		this.gameSaveData.bools.Add(key, value);
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x00034E30 File Offset: 0x00033030
	public bool ReadBool(string key, bool defaultValue = false)
	{
		bool flag;
		if (this.gameSaveData.bools.TryGetValue(key, out flag))
		{
			return flag;
		}
		this.gameSaveData.bools.Add(key, defaultValue);
		return defaultValue;
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x000077B6 File Offset: 0x000059B6
	public void Write(string key, int value)
	{
		if (this.gameSaveData.ints.ContainsKey(key))
		{
			this.gameSaveData.ints[key] = value;
			return;
		}
		this.gameSaveData.ints.Add(key, value);
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x00034E68 File Offset: 0x00033068
	public int ReadInt(string key, int defaultValue = 0)
	{
		int num;
		if (this.gameSaveData.ints.TryGetValue(key, out num))
		{
			return num;
		}
		this.gameSaveData.ints.Add(key, defaultValue);
		return defaultValue;
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x000077F0 File Offset: 0x000059F0
	public void Write(string key, float value)
	{
		this.Write(key, GameData.FloatToInt(value));
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x000077FF File Offset: 0x000059FF
	public float ReadFloat(string key, float defaultValue = 0f)
	{
		return GameData.IntToFloat(this.ReadInt(key, GameData.FloatToInt(defaultValue)));
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x00034EA0 File Offset: 0x000330A0
	public void Write(string key, Vector3 value)
	{
		this.Write(string.Format("{0}_X", key), value.x);
		this.Write(string.Format("{0}_Y", key), value.y);
		this.Write(string.Format("{0}_Z", key), value.z);
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x00007813 File Offset: 0x00005A13
	public Vector3 ReadVector3(string key)
	{
		return this.ReadVector3(key, Vector3.zero);
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x00034EF4 File Offset: 0x000330F4
	public Vector3 ReadVector3(string key, Vector3 defaultValue)
	{
		return new Vector3(this.ReadFloat(string.Format("{0}_X", key), defaultValue.x), this.ReadFloat(string.Format("{0}_Y", key), defaultValue.y), this.ReadFloat(string.Format("{0}_Z", key), defaultValue.z));
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x00007821 File Offset: 0x00005A21
	[ContextMenu("Debug Json Value")]
	private void DebugJson()
	{
		Debug.Log(JsonUtility.ToJson(this.gameSaveData));
	}

	private const float floatToIntScale = 1000f;

	public static bool hasPreloadedSave;

	public static GameData instance;

	public bool loadOnStart;

	public bool save;

	public bool isMainScene;

	public GameSaveWrapper gameSaveWrapper;

	public GameSaveData gameSaveData;

	public int saveFileSlot;

	private float lastAutoSaveTime = -100f;

	private bool lastAutoSaveInControl;

	private int canAutoSaveCounter;

	private bool wasSaveRestricted;

	private bool wasInControl;

	private const float minAutoSaveInterval = 5f;

	private const float maxAutoSaveInterval = 30f;

	private string savePath;

	private int canSaveCounter;
}
