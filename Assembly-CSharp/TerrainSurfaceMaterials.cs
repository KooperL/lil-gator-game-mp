using System;
using UnityEngine;

public class TerrainSurfaceMaterials : MonoBehaviour, ISurface
{
	// Token: 0x06000FD6 RID: 4054 RVA: 0x00052824 File Offset: 0x00050A24
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

	// Token: 0x06000FD7 RID: 4055 RVA: 0x00052884 File Offset: 0x00050A84
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

	// Token: 0x06000FD8 RID: 4056 RVA: 0x0005291C File Offset: 0x00050B1C
	private Vector2 WorldToAlpha(Vector3 worldPosition)
	{
		Vector2 vector = new Vector2(worldPosition.x - base.transform.position.x, worldPosition.z - base.transform.position.z);
		vector.x /= this.terrain.terrainData.size.x;
		vector.y /= this.terrain.terrainData.size.z;
		int length = this.alphamaps.GetLength(0);
		vector *= (float)length;
		vector.x = Mathf.Clamp(vector.x, 0f, (float)(length - 1));
		vector.y = Mathf.Clamp(vector.y, 0f, (float)(length - 1));
		return new Vector2(vector.y, vector.x);
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x0000DAB9 File Offset: 0x0000BCB9
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position)
	{
		return this.GetSurfaceMaterial(position, Vector3.up);
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x000529FC File Offset: 0x00050BFC
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position, Vector3 normal)
	{
		Vector2 vector = new Vector2(position.x - base.transform.position.x, position.z - base.transform.position.z);
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
		Vector2 vector2 = new Vector2(vector.y, vector.x);
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

	// Token: 0x06000FDB RID: 4059 RVA: 0x00052B94 File Offset: 0x00050D94
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

	private Terrain terrain;

	public SurfaceMaterial[] splatMaterials;

	public TerrainSurfaceMaterials.DetailMaterial[] detailMaterials;

	public SurfaceMaterial treeMaterial;

	public float maxWallY;

	public SurfaceMaterial wallMaterial;

	private TerrainDetails terrainDetails;

	private float[,,] alphamaps;

	[Serializable]
	public struct DetailMaterial
	{
		public string name;

		public bool overrideMaterial;

		public float minDensity;

		public SurfaceMaterial material;
	}
}
