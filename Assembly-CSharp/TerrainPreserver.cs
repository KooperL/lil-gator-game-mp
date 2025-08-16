using System;
using UnityEngine;

public class TerrainPreserver : MonoBehaviour
{
	// Token: 0x060010A2 RID: 4258 RVA: 0x00055AE8 File Offset: 0x00053CE8
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
