using System;
using System.IO;
using UnityEngine;

// Token: 0x0200017A RID: 378
public class GameData : MonoBehaviour
{
	// Token: 0x06000719 RID: 1817 RVA: 0x00007385 File Offset: 0x00005585
	public static int FloatToInt(float f)
	{
		return Mathf.FloorToInt(f * 1000f);
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x00007393 File Offset: 0x00005593
	public static float IntToFloat(int i)
	{
		return (float)i / 1000f;
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x0600071B RID: 1819 RVA: 0x0000739D File Offset: 0x0000559D
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

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x0600071C RID: 1820 RVA: 0x00033374 File Offset: 0x00031574
	// (remove) Token: 0x0600071D RID: 1821 RVA: 0x000333AC File Offset: 0x000315AC
	public event Action beforeSave;

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x0600071E RID: 1822 RVA: 0x000073D9 File Offset: 0x000055D9
	public static string FilePath
	{
		get
		{
			return Path.Combine(Application.persistentDataPath, "saveFile");
		}
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x0600071F RID: 1823 RVA: 0x000073EA File Offset: 0x000055EA
	public static string PlayerName
	{
		get
		{
			return GameData.g.gameSaveData.playerName;
		}
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x000333E4 File Offset: 0x000315E4
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

	// Token: 0x06000721 RID: 1825 RVA: 0x000073FB File Offset: 0x000055FB
	private void OnEnable()
	{
		if (GameData.instance != null && GameData.instance != this)
		{
			return;
		}
		GameData.instance = this;
		this.savePath = GameData.FilePath;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00007429 File Offset: 0x00005629
	private void OnDestroy()
	{
		Game.onEnterDialogue -= this.OnEnterDialogue;
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0000743C File Offset: 0x0000563C
	private void OnApplicationQuit()
	{
		FileUtil.isApplicationQuitting = true;
		if (Game.AllowedToSave)
		{
			this.WriteToDisk();
		}
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00007451 File Offset: 0x00005651
	private void Save()
	{
		if (!Game.AllowedToSave)
		{
			return;
		}
		this.WriteToDisk();
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x00007461 File Offset: 0x00005661
	public void SetSaveFile(int saveFileIndex)
	{
		this.saveFileSlot = saveFileIndex;
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x0000746A File Offset: 0x0000566A
	public void LoadSaveFile(int saveFileIndex)
	{
		this.saveFileSlot = saveFileIndex;
		this.ReadFromDisk();
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x00007479 File Offset: 0x00005679
	public void WriteToDisk()
	{
		this.WriteToDisk(false);
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x0003348C File Offset: 0x0003168C
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

	// Token: 0x06000729 RID: 1833 RVA: 0x000334D8 File Offset: 0x000316D8
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

	// Token: 0x0600072A RID: 1834 RVA: 0x00033528 File Offset: 0x00031728
	private void RefreshLoadedData()
	{
		foreach (QuestProfile questProfile in QuestProfile.loadedQuestProfiles)
		{
			questProfile.Load();
		}
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00033578 File Offset: 0x00031778
	public void LoadSaveData(GameSaveData gameSaveData)
	{
		GameSaveData gameSaveData2 = gameSaveData.Clone();
		this.gameSaveWrapper.gameSaveData = gameSaveData2;
		this.gameSaveData = gameSaveData2;
		this.WriteToDisk();
		this.RefreshLoadedData();
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x000335AC File Offset: 0x000317AC
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

	// Token: 0x0600072D RID: 1837 RVA: 0x00002229 File Offset: 0x00000429
	private void OnEnterDialogue()
	{
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x00007482 File Offset: 0x00005682
	public void Write(string key, bool value)
	{
		if (this.gameSaveData.bools.ContainsKey(key))
		{
			this.gameSaveData.bools[key] = value;
			return;
		}
		this.gameSaveData.bools.Add(key, value);
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x000336A8 File Offset: 0x000318A8
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

	// Token: 0x06000730 RID: 1840 RVA: 0x000074BC File Offset: 0x000056BC
	public void Write(string key, int value)
	{
		if (this.gameSaveData.ints.ContainsKey(key))
		{
			this.gameSaveData.ints[key] = value;
			return;
		}
		this.gameSaveData.ints.Add(key, value);
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x000336E0 File Offset: 0x000318E0
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

	// Token: 0x06000732 RID: 1842 RVA: 0x000074F6 File Offset: 0x000056F6
	public void Write(string key, float value)
	{
		this.Write(key, GameData.FloatToInt(value));
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x00007505 File Offset: 0x00005705
	public float ReadFloat(string key, float defaultValue = 0f)
	{
		return GameData.IntToFloat(this.ReadInt(key, GameData.FloatToInt(defaultValue)));
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x00033718 File Offset: 0x00031918
	public void Write(string key, Vector3 value)
	{
		this.Write(string.Format("{0}_X", key), value.x);
		this.Write(string.Format("{0}_Y", key), value.y);
		this.Write(string.Format("{0}_Z", key), value.z);
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x00007519 File Offset: 0x00005719
	public Vector3 ReadVector3(string key)
	{
		return this.ReadVector3(key, Vector3.zero);
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0003376C File Offset: 0x0003196C
	public Vector3 ReadVector3(string key, Vector3 defaultValue)
	{
		return new Vector3(this.ReadFloat(string.Format("{0}_X", key), defaultValue.x), this.ReadFloat(string.Format("{0}_Y", key), defaultValue.y), this.ReadFloat(string.Format("{0}_Z", key), defaultValue.z));
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x00007527 File Offset: 0x00005727
	[ContextMenu("Debug Json Value")]
	private void DebugJson()
	{
		Debug.Log(JsonUtility.ToJson(this.gameSaveData));
	}

	// Token: 0x040009A2 RID: 2466
	private const float floatToIntScale = 1000f;

	// Token: 0x040009A3 RID: 2467
	public static bool hasPreloadedSave;

	// Token: 0x040009A4 RID: 2468
	public static GameData instance;

	// Token: 0x040009A6 RID: 2470
	public bool loadOnStart;

	// Token: 0x040009A7 RID: 2471
	public bool save;

	// Token: 0x040009A8 RID: 2472
	public bool isMainScene;

	// Token: 0x040009A9 RID: 2473
	public GameSaveWrapper gameSaveWrapper;

	// Token: 0x040009AA RID: 2474
	public GameSaveData gameSaveData;

	// Token: 0x040009AB RID: 2475
	public int saveFileSlot;

	// Token: 0x040009AC RID: 2476
	private float lastAutoSaveTime = -100f;

	// Token: 0x040009AD RID: 2477
	private bool lastAutoSaveInControl;

	// Token: 0x040009AE RID: 2478
	private int canAutoSaveCounter;

	// Token: 0x040009AF RID: 2479
	private bool wasSaveRestricted;

	// Token: 0x040009B0 RID: 2480
	private bool wasInControl;

	// Token: 0x040009B1 RID: 2481
	private const float minAutoSaveInterval = 5f;

	// Token: 0x040009B2 RID: 2482
	private const float maxAutoSaveInterval = 30f;

	// Token: 0x040009B3 RID: 2483
	private string savePath;

	// Token: 0x040009B4 RID: 2484
	private int canSaveCounter;
}
