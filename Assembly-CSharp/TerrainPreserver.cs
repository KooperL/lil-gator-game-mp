using System;
using UnityEngine;

public class TerrainPreserver : MonoBehaviour
{
	// Token: 0x06000D8B RID: 3467 RVA: 0x0004187C File Offset: 0x0003FA7C
	private void Awake()
	{
		Terrain component = base.GetComponent<Terrain>();
		TerrainCollider component2 = base.GetComponent<TerrainCollider>();
		TerrainData terrainData;
		if (this.id == -1)
		{
			terrainData = Object.Instantiate<TerrainData>(component.terrainData);
		}
		else
		{
			terrainData = TerrainCleanupManager.t.GetCleanData(component.terrainData, this.id);
		}
		component.terrainData = terrainData;
		component2.terrainData = terrainData;
	}

	public int id = -1;
}
