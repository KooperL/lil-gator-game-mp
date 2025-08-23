using System;
using UnityEngine;

public class TerrainPreserver : MonoBehaviour
{
	// Token: 0x060010A3 RID: 4259 RVA: 0x00055F44 File Offset: 0x00054144
	private void Awake()
	{
		Terrain component = base.GetComponent<Terrain>();
		TerrainCollider component2 = base.GetComponent<TerrainCollider>();
		TerrainData terrainData;
		if (this.id == -1)
		{
			terrainData = global::UnityEngine.Object.Instantiate<TerrainData>(component.terrainData);
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
