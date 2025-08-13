using System;
using UnityEngine;

// Token: 0x02000344 RID: 836
[ExecuteInEditMode]
public class TerrainDoublePass : MonoBehaviour
{
	// Token: 0x06001044 RID: 4164 RVA: 0x0000E0AE File Offset: 0x0000C2AE
	private void OnEnable()
	{
		this.UpdateMaterial();
	}

	// Token: 0x06001045 RID: 4165 RVA: 0x00053C5C File Offset: 0x00051E5C
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

	// Token: 0x0400151A RID: 5402
	public Terrain terrain;

	// Token: 0x0400151B RID: 5403
	public Material doublePassMaterial;
}
