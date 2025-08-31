using System;
using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
	// Token: 0x060005F5 RID: 1525 RVA: 0x0001F62C File Offset: 0x0001D82C
	public static int FloatToInt(float f)
	{
		return Mathf.FloorToInt(f * 1000f);
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0001F63A File Offset: 0x0001D83A
	public static float IntToFloat(int i)
	{
		return (float)i / 1000f;
	}

	// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0001F644 File Offset: 0x0001D844
	public static GameData g
	{
		get
		{
			if (GameData.instance == null && Application.isPlaying)
			{
				GameData.instance = Object.FindObjectOfType<GameData>();
				if (GameData.instance != null)
				{
					GameData.instance.Awake();
				}
			}
			return GameData.instance;
		}
	}

	// (add) Token: 0x060005F8 RID: 1528 RVA: 0x0001F680 File Offset: 0x0001D880
	// (remove) Token: 0x060005F9 RID: 1529 RVA: 0x0001F6B8 File Offset: 0x0001D8B8
	public event Action beforeSave;

	// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001F6ED File Offset: 0x0001D8ED
	public static string FilePath
	{
		get
		{
			return Path.Combine(Application.persistentDataPath, "saveFile");
		}
	}

	// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001F6FE File Offset: 0x0001D8FE
	public static string PlayerName
	{
		get
		{
			return GameData.g.gameSaveData.playerName;
		}
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0001F710 File Offset: 0x0001D910
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

	// Token: 0x060005FD RID: 1533 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
	private void OnEnable()
	{
		if (GameData.instance != null && GameData.instance != this)
		{
			return;
		}
		GameData.instance = this;
		this.savePath = GameData.FilePath;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0001F7E6 File Offset: 0x0001D9E6
	private void OnDestroy()
	{
		Game.onEnterDialogue -= this.OnEnterDialogue;
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x0001F7F9 File Offset: 0x0001D9F9
	private void OnApplicationQuit()
	{
		FileUtil.isApplicationQuitting = true;
		if (Game.AllowedToSave)
		{
			this.WriteToDisk();
		}
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0001F80E File Offset: 0x0001DA0E
	private void Save()
	{
		if (!Game.AllowedToSave)
		{
			return;
		}
		this.WriteToDisk();
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0001F81E File Offset: 0x0001DA1E
	public void SetSaveFile(int saveFileIndex)
	{
		this.saveFileSlot = saveFileIndex;
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x0001F827 File Offset: 0x0001DA27
	public void LoadSaveFile(int saveFileIndex)
	{
		this.saveFileSlot = saveFileIndex;
		this.ReadFromDisk();
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0001F836 File Offset: 0x0001DA36
	public void WriteToDisk()
	{
		this.WriteToDisk(false);
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x0001F840 File Offset: 0x0001DA40
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

	// Token: 0x06000605 RID: 1541 RVA: 0x0001F88C File Offset: 0x0001DA8C
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

	// Token: 0x06000606 RID: 1542 RVA: 0x0001F8DC File Offset: 0x0001DADC
	private void RefreshLoadedData()
	{
		foreach (QuestProfile questProfile in QuestProfile.loadedQuestProfiles)
		{
			questProfile.Load();
		}
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0001F92C File Offset: 0x0001DB2C
	public void LoadSaveData(GameSaveData gameSaveData)
	{
		GameSaveData gameSaveData2 = gameSaveData.Clone();
		this.gameSaveWrapper.gameSaveData = gameSaveData2;
		this.gameSaveData = gameSaveData2;
		this.WriteToDisk();
		this.RefreshLoadedData();
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0001F960 File Offset: 0x0001DB60
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

	// Token: 0x06000609 RID: 1545 RVA: 0x0001FA5C File Offset: 0x0001DC5C
	private void OnEnterDialogue()
	{
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x0001FA5E File Offset: 0x0001DC5E
	public void Write(string key, bool value)
	{
		if (this.gameSaveData.bools.ContainsKey(key))
		{
			this.gameSaveData.bools[key] = value;
			return;
		}
		this.gameSaveData.bools.Add(key, value);
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0001FA98 File Offset: 0x0001DC98
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

	// Token: 0x0600060C RID: 1548 RVA: 0x0001FACF File Offset: 0x0001DCCF
	public void Write(string key, int value)
	{
		if (this.gameSaveData.ints.ContainsKey(key))
		{
			this.gameSaveData.ints[key] = value;
			return;
		}
		this.gameSaveData.ints.Add(key, value);
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x0001FB0C File Offset: 0x0001DD0C
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

	// Token: 0x0600060E RID: 1550 RVA: 0x0001FB43 File Offset: 0x0001DD43
	public void Write(string key, float value)
	{
		this.Write(key, GameData.FloatToInt(value));
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x0001FB52 File Offset: 0x0001DD52
	public float ReadFloat(string key, float defaultValue = 0f)
	{
		return GameData.IntToFloat(this.ReadInt(key, GameData.FloatToInt(defaultValue)));
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x0001FB68 File Offset: 0x0001DD68
	public void Write(string key, Vector3 value)
	{
		this.Write(string.Format("{0}_X", key), value.x);
		this.Write(string.Format("{0}_Y", key), value.y);
		this.Write(string.Format("{0}_Z", key), value.z);
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x0001FBBA File Offset: 0x0001DDBA
	public Vector3 ReadVector3(string key)
	{
		return this.ReadVector3(key, Vector3.zero);
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
	public Vector3 ReadVector3(string key, Vector3 defaultValue)
	{
		return new Vector3(this.ReadFloat(string.Format("{0}_X", key), defaultValue.x), this.ReadFloat(string.Format("{0}_Y", key), defaultValue.y), this.ReadFloat(string.Format("{0}_Z", key), defaultValue.z));
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0001FC1F File Offset: 0x0001DE1F
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
