using System;

[Serializable]
public class GameSaveData
{
	// (get) Token: 0x06000756 RID: 1878 RVA: 0x00034C10 File Offset: 0x00032E10
	public bool IsInitialized
	{
		get
		{
			return this.ints != null && ((this.ints.ContainsKey("WorldState") && this.ints["WorldState"] != 0) || (this.ints.ContainsKey("NewGameIndex") && this.ints["NewGameIndex"] != 0));
		}
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00034C74 File Offset: 0x00032E74
	public GameSaveData Clone()
	{
		GameSaveData gameSaveData = (GameSaveData)base.MemberwiseClone();
		gameSaveData.objectStates = (bool[])gameSaveData.objectStates.Clone();
		gameSaveData.bools = gameSaveData.bools.Clone();
		gameSaveData.ints = gameSaveData.ints.Clone();
		gameSaveData.playerName = (string)gameSaveData.playerName.Clone();
		return gameSaveData;
	}

	public const int currentVersion = 10;

	public int v = 10;

	public string playerName;

	public BoolDictionary bools = new BoolDictionary();

	public IntDictionary ints = new IntDictionary();

	public bool[] objectStates = new bool[0];
}
