using System;
using Unity.Collections;
using UnityEngine;

public class MovingWater : Water
{
	// Token: 0x06000DB0 RID: 3504 RVA: 0x0004248B File Offset: 0x0004068B
	private void OnValidate()
	{
		if (this.waterMaterial == null)
		{
			return;
		}
		this._flow = this.waterMaterial.GetVector(this._Flow);
		this._displacement = this.waterMaterial.GetFloat(this._Displacement);
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x000424CC File Offset: 0x000406CC
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

	// Token: 0x06000DB2 RID: 3506 RVA: 0x0004257C File Offset: 0x0004077C
	private float SamplePoint(Vector2Int pixel)
	{
		pixel.x = (pixel.x % this.textureResolution.x + this.textureResolution.x) % this.textureResolution.x;
		pixel.y = (pixel.y % this.textureResolution.y + this.textureResolution.y) % this.textureResolution.y;
		return this.heights[pixel.x, pixel.y];
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x00042608 File Offset: 0x00040808
	private float SampleHeight(Vector2 uv)
	{
		Vector2 vector = new Vector2(uv.x * (float)this.textureResolution.x, uv.y * (float)this.textureResolution.y);
		return 0f + this.SamplePoint(new Vector2Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y)));
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x0004266C File Offset: 0x0004086C
	public override float GetWaterPlaneHeight(Vector3 referencePosition)
	{
		Vector2 vector = Time.time * this._flow.zw();
		Vector2 vector2 = (referencePosition.xz() + vector) * this._flow.xy();
		Vector2 vector3 = (1.5f * referencePosition.xz() - vector) * this._flow.xy();
		float num = this.SampleHeight(vector2);
		float num2 = this.SampleHeight(vector3);
		return this._displacement * (num + num2 - 0.75f) - 0.15f;
	}

	public Material waterMaterial;

	public Texture2D _derivHeightMap;

	public float _displacement;

	public Vector4 _flow;

	private readonly int _Flow = Shader.PropertyToID("_Flow");

	private readonly int _Displacement = Shader.PropertyToID("_Displacement");

	private float[,] heights;

	private Vector2Int textureResolution;
}
