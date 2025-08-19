using System;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainDoublePass : MonoBehaviour
{
	// Token: 0x0600109F RID: 4255 RVA: 0x0000E421 File Offset: 0x0000C621
	private void OnEnable()
	{
		this.UpdateMaterial();
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x00055B5C File Offset: 0x00053D5C
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

	public Terrain terrain;

	public Material doublePassMaterial;
}
