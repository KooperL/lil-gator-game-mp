using System;
using UnityEngine;

// Token: 0x0200017F RID: 383
public static class GameDataConversion
{
	// Token: 0x06000743 RID: 1859 RVA: 0x00033A04 File Offset: 0x00031C04
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

	// Token: 0x02000180 RID: 384
	[Serializable]
	public class GameSaveDatav0
	{
		// Token: 0x040009B7 RID: 2487
		public int v;

		// Token: 0x040009B8 RID: 2488
		public string playerName;

		// Token: 0x040009B9 RID: 2489
		public Vector3 playerPosition;

		// Token: 0x040009BA RID: 2490
		public float playerRotation;

		// Token: 0x040009BB RID: 2491
		public float cameraRotation;

		// Token: 0x040009BC RID: 2492
		public BoolDictionary bools;

		// Token: 0x040009BD RID: 2493
		public IntDictionary ints;

		// Token: 0x040009BE RID: 2494
		public bool[] objectStates;

		// Token: 0x040009BF RID: 2495
		public GameDataConversion.GameSaveDatav0.StickerData[] placedStickers;

		// Token: 0x02000181 RID: 385
		[Serializable]
		public struct StickerData
		{
			// Token: 0x040009C0 RID: 2496
			public string id;

			// Token: 0x040009C1 RID: 2497
			public Vector2Int position;
		}
	}
}
