using System;
using UnityEngine;

// Token: 0x02000315 RID: 789
public class TerrainSurfaceMaterials : MonoBehaviour, ISurface
{
	// Token: 0x06000F7A RID: 3962 RVA: 0x00050A94 File Offset: 0x0004EC94
	private void OnValidate()
	{
		if (this.detailMaterials != null)
		{
			DetailPrototype[] detailPrototypes = base.GetComponent<Terrain>().terrainData.detailPrototypes;
			for (int i = 0; i < Mathf.Min(this.detailMaterials.Length, detailPrototypes.Length); i++)
			{
				this.detailMaterials[i].name = detailPrototypes[i].prototype.name;
			}
		}
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x00050AF4 File Offset: 0x0004ECF4
	private void OnEnable()
	{
		this.terrain = base.GetComponent<Terrain>();
		this.terrainDetails = base.GetComponent<TerrainDetails>();
		TerrainPreserver component = base.GetComponent<TerrainPreserver>();
		if (component != null && component.id != -1)
		{
			this.alphamaps = TerrainCleanupManager.t.GetAlphamaps(this.terrain.terrainData, component.id);
			return;
		}
		this.alphamaps = this.terrain.terrainData.GetAlphamaps(0, 0, this.terrain.terrainData.alphamapWidth, this.terrain.terrainData.alphamapHeight);
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x00050B8C File Offset: 0x0004ED8C
	private Vector2 WorldToAlpha(Vector3 worldPosition)
	{
		Vector2 vector;
		vector..ctor(worldPosition.x - base.transform.position.x, worldPosition.z - base.transform.position.z);
		vector.x /= this.terrain.terrainData.size.x;
		vector.y /= this.terrain.terrainData.size.z;
		int length = this.alphamaps.GetLength(0);
		vector *= (float)length;
		vector.x = Mathf.Clamp(vector.x, 0f, (float)(length - 1));
		vector.y = Mathf.Clamp(vector.y, 0f, (float)(length - 1));
		return new Vector2(vector.y, vector.x);
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x0000D726 File Offset: 0x0000B926
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position)
	{
		return this.GetSurfaceMaterial(position, Vector3.up);
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x00050C6C File Offset: 0x0004EE6C
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position, Vector3 normal)
	{
		Vector2 vector;
		vector..ctor(position.x - base.transform.position.x, position.z - base.transform.position.z);
		vector.x /= this.terrain.terrainData.size.x;
		vector.y /= this.terrain.terrainData.size.z;
		int num;
		float num2;
		if (this.terrainDetails.GetStrongestDetail(position, out num, out num2) && this.detailMaterials.Length > num && this.detailMaterials[num].overrideMaterial && this.detailMaterials[num].minDensity < num2)
		{
			return this.detailMaterials[num].material;
		}
		normal = this.terrain.terrainData.GetInterpolatedNormal(vector.x, vector.y);
		if (normal.y < this.maxWallY)
		{
			return this.wallMaterial;
		}
		vector *= (float)this.alphamaps.GetLength(0);
		Vector2 vector2;
		vector2..ctor(vector.y, vector.x);
		float num3 = this.MultiSample(vector2.x, vector2.y, 0) * 0.5f;
		int num4 = 0;
		for (int i = 1; i < this.alphamaps.GetLength(2); i++)
		{
			float num5 = this.MultiSample(vector2.x, vector2.y, i);
			if (num5 > num3)
			{
				num3 = num5;
				num4 = i;
			}
		}
		return this.splatMaterials[num4];
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x00050E04 File Offset: 0x0004F004
	public float MultiSample(float x, float y, int z)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = Mathf.FloorToInt(x);
		while ((float)num3 < x + 1f)
		{
			int num4 = Mathf.FloorToInt(y);
			while ((float)num4 < y + 1f)
			{
				if (num3 > 0 && num4 > 0 && num3 < this.alphamaps.GetLength(0) && num4 < this.alphamaps.GetLength(1))
				{
					float num5 = this.alphamaps[num3, num4, z];
					float num6 = (1f - Mathf.Abs((float)num3 - x)) * (1f - Mathf.Abs((float)num4 - y));
					num2 += num6;
					num += num6 * num5;
				}
				num4++;
			}
			num3++;
		}
		if (num2 != 0f)
		{
			num /= num2;
		}
		return num;
	}

	// Token: 0x04001408 RID: 5128
	private Terrain terrain;

	// Token: 0x04001409 RID: 5129
	public SurfaceMaterial[] splatMaterials;

	// Token: 0x0400140A RID: 5130
	public TerrainSurfaceMaterials.DetailMaterial[] detailMaterials;

	// Token: 0x0400140B RID: 5131
	public SurfaceMaterial treeMaterial;

	// Token: 0x0400140C RID: 5132
	public float maxWallY;

	// Token: 0x0400140D RID: 5133
	public SurfaceMaterial wallMaterial;

	// Token: 0x0400140E RID: 5134
	private TerrainDetails terrainDetails;

	// Token: 0x0400140F RID: 5135
	private float[,,] alphamaps;

	// Token: 0x02000316 RID: 790
	[Serializable]
	public struct DetailMaterial
	{
		// Token: 0x04001410 RID: 5136
		public string name;

		// Token: 0x04001411 RID: 5137
		public bool overrideMaterial;

		// Token: 0x04001412 RID: 5138
		public float minDensity;

		// Token: 0x04001413 RID: 5139
		public SurfaceMaterial material;
	}
}
