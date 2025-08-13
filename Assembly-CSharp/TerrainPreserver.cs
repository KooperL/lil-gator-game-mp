using System;
using UnityEngine;

// Token: 0x02000345 RID: 837
public class TerrainPreserver : MonoBehaviour
{
	// Token: 0x06001047 RID: 4167 RVA: 0x00053D58 File Offset: 0x00051F58
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

	// Token: 0x0400151C RID: 5404
	public int id = -1;
}
