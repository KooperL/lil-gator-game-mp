using System;
using UnityEngine;

// Token: 0x02000277 RID: 631
[ExecuteInEditMode]
public class TerrainDoublePass : MonoBehaviour
{
	// Token: 0x06000D88 RID: 3464 RVA: 0x0004176D File Offset: 0x0003F96D
	private void OnEnable()
	{
		this.UpdateMaterial();
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x00041778 File Offset: 0x0003F978
	private void UpdateMaterial()
	{
		int num = 4;
		while (num < 8 && num < this.terrain.terrainData.terrainLayers.Length)
		{
			this.doublePassMaterial.SetTexture("_Splat" + num.ToString(), this.terrain.terrainData.terrainLayers[num].diffuseTexture);
			Vector2 zero = Vector2.zero;
			zero.x = this.terrain.terrainData.size.x / this.terrain.terrainData.terrainLayers[num].tileSize.x;
			zero.y = this.terrain.terrainData.size.z / this.terrain.terrainData.terrainLayers[num].tileSize.y;
			this.doublePassMaterial.SetTextureScale("_Splat" + num.ToString(), zero);
			num++;
		}
	}

	// Token: 0x040011DE RID: 4574
	public Terrain terrain;

	// Token: 0x040011DF RID: 4575
	public Material doublePassMaterial;
}
