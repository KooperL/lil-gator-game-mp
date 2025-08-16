using System;
using Unity.Collections;
using UnityEngine;

public class MovingWater : Water
{
	// Token: 0x060010C7 RID: 4295 RVA: 0x0000E516 File Offset: 0x0000C716
	private void OnValidate()
	{
		if (this.waterMaterial == null)
		{
			return;
		}
		this._flow = this.waterMaterial.GetVector(this._Flow);
		this._displacement = this.waterMaterial.GetFloat(this._Displacement);
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x0005658C File Offset: 0x0005478C
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

	// Token: 0x060010C9 RID: 4297 RVA: 0x0005663C File Offset: 0x0005483C
	private float SamplePoint(Vector2Int pixel)
	{
		pixel.x = (pixel.x % this.textureResolution.x + this.textureResolution.x) % this.textureResolution.x;
		pixel.y = (pixel.y % this.textureResolution.y + this.textureResolution.y) % this.textureResolution.y;
		return this.heights[pixel.x, pixel.y];
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x000566C8 File Offset: 0x000548C8
	private float SampleHeight(Vector2 uv)
	{
		Vector2 vector = new Vector2(uv.x * (float)this.textureResolution.x, uv.y * (float)this.textureResolution.y);
		return 0f + this.SamplePoint(new Vector2Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y)));
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x0005672C File Offset: 0x0005492C
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
