using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCleanupManager : MonoBehaviour
{
	// Token: 0x060009F8 RID: 2552 RVA: 0x0003BDE4 File Offset: 0x00039FE4
	private void Awake()
	{
		if (TerrainCleanupManager.t != null || TerrainCleanupManager.hasInstance)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		global::UnityEngine.Object.DontDestroyOnLoad(this);
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

	// Token: 0x060009F9 RID: 2553 RVA: 0x0003BE74 File Offset: 0x0003A074
	private void AddTerrain(TerrainData sourceData, int id)
	{
		TerrainCleanupManager.CleanData cleanData = new TerrainCleanupManager.CleanData
		{
			terrainData = global::UnityEngine.Object.Instantiate<TerrainData>(sourceData),
			isClean = true
		};
		global::UnityEngine.Object.DontDestroyOnLoad(cleanData.terrainData);
		cleanData.details = new int[sourceData.detailPrototypes.Length][,];
		for (int i = 0; i < cleanData.details.Length; i++)
		{
			cleanData.details[i] = sourceData.GetDetailLayer(0, 0, sourceData.detailWidth, sourceData.detailHeight, i);
		}
		cleanData.alphamaps = sourceData.GetAlphamaps(0, 0, sourceData.alphamapWidth, sourceData.alphamapHeight);
		this.cleanDataDic.Add(id, cleanData);
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x0003BF18 File Offset: 0x0003A118
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

	// Token: 0x060009FB RID: 2555 RVA: 0x00009951 File Offset: 0x00007B51
	public float[,,] GetAlphamaps(TerrainData sourceData, int id)
	{
		if (!this.cleanDataDic.ContainsKey(id))
		{
			this.AddTerrain(sourceData, id);
		}
		return this.cleanDataDic[id].alphamaps;
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x0000997A File Offset: 0x00007B7A
	public int[][,] GetDetails(TerrainData sourceData, int id)
	{
		if (!this.cleanDataDic.ContainsKey(id))
		{
			this.AddTerrain(sourceData, id);
		}
		return this.cleanDataDic[id].details;
	}

	public static TerrainCleanupManager t;

	public static bool hasInstance;

	public TerrainData[] preloadedTerrainData;

	private Dictionary<int, TerrainCleanupManager.CleanData> cleanDataDic;

	private struct CleanData
	{
		public bool isClean;

		public int[][,] details;

		public float[,,] alphamaps;

		public TerrainData terrainData;
	}
}
