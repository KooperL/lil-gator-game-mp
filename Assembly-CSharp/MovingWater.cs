using System;
using Unity.Collections;
using UnityEngine;

// Token: 0x02000352 RID: 850
public class MovingWater : Water
{
	// Token: 0x0600106C RID: 4204 RVA: 0x0000E1C2 File Offset: 0x0000C3C2
	private void OnValidate()
	{
		if (this.waterMaterial == null)
		{
			return;
		}
		this._flow = this.waterMaterial.GetVector(this._Flow);
		this._displacement = this.waterMaterial.GetFloat(this._Displacement);
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x000547FC File Offset: 0x000529FC
	private void Awake()
	{
		NativeArray<Color32> rawTextureData = this._derivHeightMap.GetRawTextureData<Color32>();
		this.heights = new float[this._derivHeightMap.width, this._derivHeightMap.height];
		this.textureResolution = new Vector2Int(this._derivHeightMap.width, this._derivHeightMap.height);
		int num = 0;
		for (int i = 0; i < this._derivHeightMap.height; i++)
		{
			for (int j = 0; j < this._derivHeightMap.width; j++)
			{
				this.heights[j, i] = (float)rawTextureData[num++].r / 255f;
			}
		}
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x000548AC File Offset: 0x00052AAC
	private float SamplePoint(Vector2Int pixel)
	{
		pixel.x = (pixel.x % this.textureResolution.x + this.textureResolution.x) % this.textureResolution.x;
		pixel.y = (pixel.y % this.textureResolution.y + this.textureResolution.y) % this.textureResolution.y;
		return this.heights[pixel.x, pixel.y];
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x00054938 File Offset: 0x00052B38
	private float SampleHeight(Vector2 uv)
	{
		Vector2 vector;
		vector..ctor(uv.x * (float)this.textureResolution.x, uv.y * (float)this.textureResolution.y);
		return 0f + this.SamplePoint(new Vector2Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y)));
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x0005499C File Offset: 0x00052B9C
	public override float GetWaterPlaneHeight(Vector3 referencePosition)
	{
		Vector2 vector = Time.time * this._flow.zw();
		Vector2 vector2 = (referencePosition.xz() + vector) * this._flow.xy();
		Vector2 vector3 = (1.5f * referencePosition.xz() - vector) * this._flow.xy();
		float num = this.SampleHeight(vector2);
		float num2 = this.SampleHeight(vector3);
		return this._displacement * (num + num2 - 0.75f) - 0.15f;
	}

	// Token: 0x04001549 RID: 5449
	public Material waterMaterial;

	// Token: 0x0400154A RID: 5450
	public Texture2D _derivHeightMap;

	// Token: 0x0400154B RID: 5451
	public float _displacement;

	// Token: 0x0400154C RID: 5452
	public Vector4 _flow;

	// Token: 0x0400154D RID: 5453
	private readonly int _Flow = Shader.PropertyToID("_Flow");

	// Token: 0x0400154E RID: 5454
	private readonly int _Displacement = Shader.PropertyToID("_Displacement");

	// Token: 0x0400154F RID: 5455
	private float[,] heights;

	// Token: 0x04001550 RID: 5456
	private Vector2Int textureResolution;
}
