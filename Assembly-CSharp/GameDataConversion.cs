using System;
using UnityEngine;

// Token: 0x02000126 RID: 294
public static class GameDataConversion
{
	// Token: 0x0600061F RID: 1567 RVA: 0x0001FEC0 File Offset: 0x0001E0C0
	public static GameSaveData Convert(string rawSaveData, int version)
	{
		if (version == 0)
		{
			GameDataConversion.GameSaveDatav0 gameSaveDatav = JsonUtility.FromJson<GameDataConversion.GameSaveDatav0>(rawSaveData);
			string playerName = gameSaveDatav.playerName;
			BoolDictionary bools = gameSaveDatav.bools;
			IntDictionary ints = gameSaveDatav.ints;
			bool[] objectStates = gameSaveDatav.objectStates;
			return new GameSaveData
			{
				playerName = playerName,
				bools = bools,
				ints = ints,
				objectStates = objectStates
			};
		}
		Debug.LogError("Unknown save version");
		return new GameSaveData();
	}

	// Token: 0x020003B1 RID: 945
	[Serializable]
	public class GameSaveDatav0
	{
		// Token: 0x04001B7C RID: 7036
		public int v;

		// Token: 0x04001B7D RID: 7037
		public string playerName;

		// Token: 0x04001B7E RID: 7038
		public Vector3 playerPosition;

		// Token: 0x04001B7F RID: 7039
		public float playerRotation;

		// Token: 0x04001B80 RID: 7040
		public float cameraRotation;

		// Token: 0x04001B81 RID: 7041
		public BoolDictionary bools;

		// Token: 0x04001B82 RID: 7042
		public IntDictionary ints;

		// Token: 0x04001B83 RID: 7043
		public bool[] objectStates;

		// Token: 0x04001B84 RID: 7044
		public GameDataConversion.GameSaveDatav0.StickerData[] placedStickers;

		// Token: 0x020004BC RID: 1212
		[Serializable]
		public struct StickerData
		{
			// Token: 0x04001FA8 RID: 8104
			public string id;

			// Token: 0x04001FA9 RID: 8105
			public Vector2Int position;
		}
	}
}
