using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class TerrainCleanupManager : MonoBehaviour
{
	// Token: 0x06000839 RID: 2105 RVA: 0x00027430 File Offset: 0x00025630
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

	// Token: 0x0600083A RID: 2106 RVA: 0x000274C0 File Offset: 0x000256C0
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

	// Token: 0x0600083B RID: 2107 RVA: 0x00027564 File Offset: 0x00025764
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

	// Token: 0x0600083C RID: 2108 RVA: 0x000275D5 File Offset: 0x000257D5
	public float[,,] GetAlphamaps(TerrainData sourceData, int id)
	{
		if (!this.cleanDataDic.ContainsKey(id))
		{
			this.AddTerrain(sourceData, id);
		}
		return this.cleanDataDic[id].alphamaps;
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x000275FE File Offset: 0x000257FE
	public int[][,] GetDetails(TerrainData sourceData, int id)
	{
		if (!this.cleanDataDic.ContainsKey(id))
		{
			this.AddTerrain(sourceData, id);
		}
		return this.cleanDataDic[id].details;
	}

	// Token: 0x04000A62 RID: 2658
	public static TerrainCleanupManager t;

	// Token: 0x04000A63 RID: 2659
	public static bool hasInstance;

	// Token: 0x04000A64 RID: 2660
	public TerrainData[] preloadedTerrainData;

	// Token: 0x04000A65 RID: 2661
	private Dictionary<int, TerrainCleanupManager.CleanData> cleanDataDic;

	// Token: 0x020003D2 RID: 978
	private struct CleanData
	{
		// Token: 0x04001C18 RID: 7192
		public bool isClean;

		// Token: 0x04001C19 RID: 7193
		public int[][,] details;

		// Token: 0x04001C1A RID: 7194
		public float[,,] alphamaps;

		// Token: 0x04001C1B RID: 7195
		public TerrainData terrainData;
	}
}
