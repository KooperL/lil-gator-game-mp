using System;

// Token: 0x02000120 RID: 288
[Serializable]
public class GameSaveData
{
	// Token: 0x17000053 RID: 83
	// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001F530 File Offset: 0x0001D730
	public bool IsInitialized
	{
		get
		{
			return this.ints != null && ((this.ints.ContainsKey("WorldState") && this.ints["WorldState"] != 0) || (this.ints.ContainsKey("NewGameIndex") && this.ints["NewGameIndex"] != 0));
		}
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x0001F594 File Offset: 0x0001D794
	public GameSaveData Clone()
	{
		GameSaveData gameSaveData = (GameSaveData)base.MemberwiseClone();
		gameSaveData.objectStates = (bool[])gameSaveData.objectStates.Clone();
		gameSaveData.bools = gameSaveData.bools.Clone();
		gameSaveData.ints = gameSaveData.ints.Clone();
		gameSaveData.playerName = (string)gameSaveData.playerName.Clone();
		return gameSaveData;
	}

	// Token: 0x0400083D RID: 2109
	public const int currentVersion = 10;

	// Token: 0x0400083E RID: 2110
	public int v = 10;

	// Token: 0x0400083F RID: 2111
	public string playerName;

	// Token: 0x04000840 RID: 2112
	public BoolDictionary bools = new BoolDictionary();

	// Token: 0x04000841 RID: 2113
	public IntDictionary ints = new IntDictionary();

	// Token: 0x04000842 RID: 2114
	public bool[] objectStates = new bool[0];
}
