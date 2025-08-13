using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020C RID: 524
public class TerrainCleanupManager : MonoBehaviour
{
	// Token: 0x060009B0 RID: 2480 RVA: 0x0003A33C File Offset: 0x0003853C
	private void Awake()
	{
		if (TerrainCleanupManager.t != null || TerrainCleanupManager.hasInstance)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		Object.DontDestroyOnLoad(base.gameObject);
		Object.DontDestroyOnLoad(this);
		foreach (TerrainData terrainData in this.preloadedTerrainData)
		{
			TerrainCleanupManager.t = this;
		}
		TerrainCleanupManager.hasInstance = true;
		this.cleanDataDic = new Dictionary<int, TerrainCleanupManager.CleanData>();
		for (int j = 0; j < this.preloadedTerrainData.Length; j++)
		{
			this.AddTerrain(this.preloadedTerrainData[j], j);
		}
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x0003A3CC File Offset: 0x000385CC
	private void AddTerrain(TerrainData sourceData, int id)
	{
		TerrainCleanupManager.CleanData cleanData = new TerrainCleanupManager.CleanData
		{
			terrainData = Object.Instantiate<TerrainData>(sourceData),
			isClean = true
		};
		Object.DontDestroyOnLoad(cleanData.terrainData);
		cleanData.details = new int[sourceData.detailPrototypes.Length][,];
		for (int i = 0; i < cleanData.details.Length; i++)
		{
			cleanData.details[i] = sourceData.GetDetailLayer(0, 0, sourceData.detailWidth, sourceData.detailHeight, i);
		}
		cleanData.alphamaps = sourceData.GetAlphamaps(0, 0, sourceData.alphamapWidth, sourceData.alphamapHeight);
		this.cleanDataDic.Add(id, cleanData);
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x0003A470 File Offset: 0x00038670
	public TerrainData GetCleanData(TerrainData sourceData, int id)
	{
		if (!this.cleanDataDic.ContainsKey(id))
		{
			this.AddTerrain(sourceData, id);
			return this.cleanDataDic[id].terrainData;
		}
		TerrainCleanupManager.CleanData cleanData = this.cleanDataDic[id];
		for (int i = 0; i < cleanData.details.Length; i++)
		{
			cleanData.terrainData.SetDetailLayer(0, 0, i, cleanData.details[i]);
		}
		return cleanData.terrainData;
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0000961D File Offset: 0x0000781D
	public float[,,] GetAlphamaps(TerrainData sourceData, int id)
	{
		if (!this.cleanDataDic.ContainsKey(id))
		{
			this.AddTerrain(sourceData, id);
		}
		return this.cleanDataDic[id].alphamaps;
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x00009646 File Offset: 0x00007846
	public int[][,] GetDetails(TerrainData sourceData, int id)
	{
		if (!this.cleanDataDic.ContainsKey(id))
		{
			this.AddTerrain(sourceData, id);
		}
		return this.cleanDataDic[id].details;
	}

	// Token: 0x04000C54 RID: 3156
	public static TerrainCleanupManager t;

	// Token: 0x04000C55 RID: 3157
	public static bool hasInstance;

	// Token: 0x04000C56 RID: 3158
	public TerrainData[] preloadedTerrainData;

	// Token: 0x04000C57 RID: 3159
	private Dictionary<int, TerrainCleanupManager.CleanData> cleanDataDic;

	// Token: 0x0200020D RID: 525
	private struct CleanData
	{
		// Token: 0x04000C58 RID: 3160
		public bool isClean;

		// Token: 0x04000C59 RID: 3161
		public int[][,] details;

		// Token: 0x04000C5A RID: 3162
		public float[,,] alphamaps;

		// Token: 0x04000C5B RID: 3163
		public TerrainData terrainData;
	}
}
