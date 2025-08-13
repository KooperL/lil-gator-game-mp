using System;

// Token: 0x02000179 RID: 377
[Serializable]
public class GameSaveData
{
	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06000716 RID: 1814 RVA: 0x00007322 File Offset: 0x00005522
	public bool IsInitialized
	{
		get
		{
			return this.ints != null && this.ints.ContainsKey("WorldState") && this.ints["WorldState"] != 0;
		}
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x0003330C File Offset: 0x0003150C
	public GameSaveData Clone()
	{
		GameSaveData gameSaveData = (GameSaveData)base.MemberwiseClone();
		gameSaveData.objectStates = (bool[])gameSaveData.objectStates.Clone();
		gameSaveData.bools = gameSaveData.bools.Clone();
		gameSaveData.ints = gameSaveData.ints.Clone();
		gameSaveData.playerName = (string)gameSaveData.playerName.Clone();
		return gameSaveData;
	}

	// Token: 0x0400099C RID: 2460
	public const int currentVersion = 10;

	// Token: 0x0400099D RID: 2461
	public int v = 10;

	// Token: 0x0400099E RID: 2462
	public string playerName;

	// Token: 0x0400099F RID: 2463
	public BoolDictionary bools = new BoolDictionary();

	// Token: 0x040009A0 RID: 2464
	public IntDictionary ints = new IntDictionary();

	// Token: 0x040009A1 RID: 2465
	public bool[] objectStates = new bool[0];
}
