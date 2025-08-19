using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerrainDetails : MonoBehaviour
{
	// Token: 0x0600108D RID: 4237 RVA: 0x00055304 File Offset: 0x00053504
	private void OnValidate()
	{
		DetailPrototype[] detailPrototypes = base.GetComponent<Terrain>().terrainData.detailPrototypes;
		for (int i = 0; i < Mathf.Min(this.clippings.Length, detailPrototypes.Length); i++)
		{
			this.clippings[i].name = detailPrototypes[i].prototype.name;
		}
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x0005535C File Offset: 0x0005355C
	private void OnEnable()
	{
		this.t = base.GetComponent<Terrain>();
		TerrainPreserver component = base.GetComponent<TerrainPreserver>();
		if (component != null && component.id != -1)
		{
			int[][,] details = TerrainCleanupManager.t.GetDetails(this.t.terrainData, component.id);
			this.map = new int[details.Length][,];
			for (int i = 0; i < this.map.Length; i++)
			{
				this.map[i] = (int[,])details[i].Clone();
			}
		}
		else
		{
			this.map = new int[this.t.terrainData.detailPrototypes.Length][,];
			for (int j = 0; j < this.t.terrainData.detailPrototypes.Length; j++)
			{
				this.map[j] = this.t.terrainData.GetDetailLayer(0, 0, this.t.terrainData.detailWidth, this.t.terrainData.detailHeight, j);
			}
		}
		this.terrainSize = new Vector2(this.t.terrainData.size.x, this.t.terrainData.size.z);
		this.detailSize = new Vector2Int(this.t.terrainData.detailWidth, this.t.terrainData.detailHeight);
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x000554B8 File Offset: 0x000536B8
	public Vector2Int WorldToDetail(Vector3 worldPosition)
	{
		Vector2 vector = new Vector2(worldPosition.x - base.transform.position.x, worldPosition.z - base.transform.position.z);
		vector.x *= (float)this.detailSize.x / this.terrainSize.x;
		vector.y *= (float)this.detailSize.y / this.terrainSize.y;
		return new Vector2Int(Mathf.Clamp(Mathf.FloorToInt(vector.x), 0, this.detailSize.x), Mathf.Clamp(Mathf.FloorToInt(vector.y), 0, this.detailSize.y));
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x00055580 File Offset: 0x00053780
	private Vector3 DetailToWorld(Vector2Int point)
	{
		Vector2 vector = new Vector2((float)point.x + 0.5f, (float)point.y + 0.5f);
		vector.x /= (float)this.detailSize.x;
		vector.y /= (float)this.detailSize.y;
		float num = this.t.terrainData.GetInterpolatedHeight(vector.x, vector.y) + base.transform.position.y;
		vector.x *= this.terrainSize.x;
		vector.y *= this.terrainSize.y;
		return new Vector3(vector.x + base.transform.position.x, num, vector.y + base.transform.position.z);
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x0005566C File Offset: 0x0005386C
	public void ClearDetailsBox(Transform localPosition, Vector2 corner1, Vector2 corner2, float pointSpacing)
	{
		if (this.isUpdatingDetails)
		{
			return;
		}
		int num = 0;
		Vector3 vector = Vector3.zero;
		this.cutLayers.Clear();
		this.cutAudio.Clear();
		for (float num2 = corner1.x; num2 < corner2.x; num2 += pointSpacing)
		{
			for (float num3 = corner1.y; num3 < corner2.y; num3 += pointSpacing)
			{
				Vector2Int vector2Int = this.WorldToDetail(localPosition.TransformPoint(new Vector3(num2, 0f, num3)));
				for (int i = 0; i < this.map.Length; i++)
				{
					if (i < this.clippings.Length && !this.clippings[i].isNotCuttable && this.map[i][vector2Int.y, vector2Int.x] > 0)
					{
						if (!this.cutLayers.Contains(i))
						{
							this.cutLayers.Add(i);
						}
						AudioSourceVariance audioSourceVariance = (this.clippings[i].overrideAudioSource ? this.clippings[i].audioSource : this.cuttingAudio);
						if (!this.cutAudio.Contains(audioSourceVariance))
						{
							this.cutAudio.Add(audioSourceVariance);
						}
						Vector3 vector2 = this.DetailToWorld(new Vector2Int(vector2Int.x, vector2Int.y));
						num += this.cutPerPoint;
						this.map[i][vector2Int.y, vector2Int.x] -= this.cutPerPoint;
						vector += (float)this.cutPerPoint * vector2;
						this.EmitLayer(vector2 + Vector3.up * 0.5f, this.clippings[i], (float)this.cutPerPoint * 1f);
					}
				}
			}
		}
		if (num > 0)
		{
			base.StartCoroutine(this.UpdateDetails(this.cutLayers));
			foreach (AudioSourceVariance audioSourceVariance2 in this.cutAudio)
			{
				audioSourceVariance2.Play();
			}
			this.CheckLoot(vector / (float)num, num);
			TerrainDetails.onCutDetails.Invoke(num);
		}
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x0000E3A9 File Offset: 0x0000C5A9
	private IEnumerator UpdateDetails(List<int> cutLayers)
	{
		this.isUpdatingDetails = true;
		foreach (int num in cutLayers)
		{
			this.t.terrainData.SetDetailLayer(0, 0, num, this.map[num]);
			yield return null;
		}
		List<int>.Enumerator enumerator = default(List<int>.Enumerator);
		this.isUpdatingDetails = false;
		yield break;
		yield break;
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x000558CC File Offset: 0x00053ACC
	private void EmitLayer(Vector3 position, TerrainDetails.DetailLayerClippings clipping, float density = 1f)
	{
		ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
		emitParams.position = position;
		emitParams.applyShapeToPosition = true;
		if (clipping.overrideColor)
		{
			emitParams.startColor = clipping.color;
		}
		if (clipping.size != 0f)
		{
			emitParams.startSize = clipping.size;
		}
		clipping.particleSystem.Emit(emitParams, Mathf.CeilToInt(density * (float)clipping.count * this.t.detailObjectDensity));
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x0005594C File Offset: 0x00053B4C
	private void CheckLoot(Vector3 position, int amountCut)
	{
		this.cumulativeAmountCut += amountCut;
		while (this.cumulativeAmountCut > 10)
		{
			foreach (TerrainDetails.ClippingsLoot clippingsLoot in this.clippingsLoot)
			{
				if (global::UnityEngine.Random.value <= clippingsLoot.chance)
				{
					global::UnityEngine.Object.Instantiate<GameObject>(clippingsLoot.prefab, position + global::UnityEngine.Random.insideUnitSphere + 0.5f * Vector3.up, Quaternion.identity);
					break;
				}
			}
			this.cumulativeAmountCut -= 10;
		}
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x000559E0 File Offset: 0x00053BE0
	public bool GetStrongestDetail(Vector3 worldPosition, out int detailLayer, out float strength)
	{
		Vector2Int vector2Int = this.WorldToDetail(worldPosition);
		detailLayer = -1;
		strength = 0f;
		for (int i = 0; i < this.t.terrainData.detailPrototypes.Length; i++)
		{
			float num = (float)this.map[i][vector2Int.y, vector2Int.x];
			if (num > strength)
			{
				detailLayer = i;
				strength = num;
			}
		}
		return detailLayer != -1;
	}

	public static UnityEvent<int> onCutDetails = new UnityEvent<int>();

	private Terrain t;

	private int[][,] map;

	public TerrainDetails.DetailLayerClippings[] clippings;

	public AudioSourceVariance cuttingAudio;

	private Vector2 terrainSize;

	private Vector2Int detailSize;

	public TerrainDetails.ClippingsLoot[] clippingsLoot;

	private int cutPerPoint = 4;

	private List<int> cutLayers = new List<int>();

	private List<AudioSourceVariance> cutAudio = new List<AudioSourceVariance>();

	private bool isUpdatingDetails;

	private int cumulativeAmountCut;

	private const int lootCheckAmount = 10;

	[Serializable]
	public struct DetailLayerClippings
	{
		public string name;

		public bool isNotCuttable;

		public ParticleSystem particleSystem;

		public int count;

		public bool overrideColor;

		[ConditionalHide("overrideColor", true)]
		public Color color;

		public float size;

		public bool overrideAudioSource;

		[ConditionalHide("overrideAudioSource", true)]
		public AudioSourceVariance audioSource;
	}

	[Serializable]
	public struct ClippingsLoot
	{
		public GameObject prefab;

		public float chance;
	}
}
