using System;
using UnityEngine;

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

	[Serializable]
	public class GameSaveDatav0
	{
		public int v;

		public string playerName;

		public Vector3 playerPosition;

		public float playerRotation;

		public float cameraRotation;

		public BoolDictionary bools;

		public IntDictionary ints;

		public bool[] objectStates;

		public GameDataConversion.GameSaveDatav0.StickerData[] placedStickers;

		[Serializable]
		public struct StickerData
		{
			public string id;

			public Vector2Int position;
		}
	}
}
