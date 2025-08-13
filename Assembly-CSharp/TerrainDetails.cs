using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000340 RID: 832
public class TerrainDetails : MonoBehaviour
{
	// Token: 0x06001032 RID: 4146 RVA: 0x00053404 File Offset: 0x00051604
	private void OnValidate()
	{
		DetailPrototype[] detailPrototypes = base.GetComponent<Terrain>().terrainData.detailPrototypes;
		for (int i = 0; i < Mathf.Min(this.clippings.Length, detailPrototypes.Length); i++)
		{
			this.clippings[i].name = detailPrototypes[i].prototype.name;
		}
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x0005345C File Offset: 0x0005165C
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

	// Token: 0x06001034 RID: 4148 RVA: 0x000535B8 File Offset: 0x000517B8
	public Vector2Int WorldToDetail(Vector3 worldPosition)
	{
		Vector2 vector;
		vector..ctor(worldPosition.x - base.transform.position.x, worldPosition.z - base.transform.position.z);
		vector.x *= (float)this.detailSize.x / this.terrainSize.x;
		vector.y *= (float)this.detailSize.y / this.terrainSize.y;
		return new Vector2Int(Mathf.Clamp(Mathf.FloorToInt(vector.x), 0, this.detailSize.x), Mathf.Clamp(Mathf.FloorToInt(vector.y), 0, this.detailSize.y));
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x00053680 File Offset: 0x00051880
	private Vector3 DetailToWorld(Vector2Int point)
	{
		Vector2 vector;
		vector..ctor((float)point.x + 0.5f, (float)point.y + 0.5f);
		vector.x /= (float)this.detailSize.x;
		vector.y /= (float)this.detailSize.y;
		float num = this.t.terrainData.GetInterpolatedHeight(vector.x, vector.y) + base.transform.position.y;
		vector.x *= this.terrainSize.x;
		vector.y *= this.terrainSize.y;
		return new Vector3(vector.x + base.transform.position.x, num, vector.y + base.transform.position.z);
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x0005376C File Offset: 0x0005196C
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

	// Token: 0x06001037 RID: 4151 RVA: 0x0000E036 File Offset: 0x0000C236
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

	// Token: 0x06001038 RID: 4152 RVA: 0x000539CC File Offset: 0x00051BCC
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

	// Token: 0x06001039 RID: 4153 RVA: 0x00053A4C File Offset: 0x00051C4C
	private void CheckLoot(Vector3 position, int amountCut)
	{
		this.cumulativeAmountCut += amountCut;
		while (this.cumulativeAmountCut > 10)
		{
			foreach (TerrainDetails.ClippingsLoot clippingsLoot in this.clippingsLoot)
			{
				if (Random.value <= clippingsLoot.chance)
				{
					Object.Instantiate<GameObject>(clippingsLoot.prefab, position + Random.insideUnitSphere + 0.5f * Vector3.up, Quaternion.identity);
					break;
				}
			}
			this.cumulativeAmountCut -= 10;
		}
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x00053AE0 File Offset: 0x00051CE0
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

	// Token: 0x040014FC RID: 5372
	public static UnityEvent<int> onCutDetails = new UnityEvent<int>();

	// Token: 0x040014FD RID: 5373
	private Terrain t;

	// Token: 0x040014FE RID: 5374
	private int[][,] map;

	// Token: 0x040014FF RID: 5375
	public TerrainDetails.DetailLayerClippings[] clippings;

	// Token: 0x04001500 RID: 5376
	public AudioSourceVariance cuttingAudio;

	// Token: 0x04001501 RID: 5377
	private Vector2 terrainSize;

	// Token: 0x04001502 RID: 5378
	private Vector2Int detailSize;

	// Token: 0x04001503 RID: 5379
	public TerrainDetails.ClippingsLoot[] clippingsLoot;

	// Token: 0x04001504 RID: 5380
	private int cutPerPoint = 4;

	// Token: 0x04001505 RID: 5381
	private List<int> cutLayers = new List<int>();

	// Token: 0x04001506 RID: 5382
	private List<AudioSourceVariance> cutAudio = new List<AudioSourceVariance>();

	// Token: 0x04001507 RID: 5383
	private bool isUpdatingDetails;

	// Token: 0x04001508 RID: 5384
	private int cumulativeAmountCut;

	// Token: 0x04001509 RID: 5385
	private const int lootCheckAmount = 10;

	// Token: 0x02000341 RID: 833
	[Serializable]
	public struct DetailLayerClippings
	{
		// Token: 0x0400150A RID: 5386
		public string name;

		// Token: 0x0400150B RID: 5387
		public bool isNotCuttable;

		// Token: 0x0400150C RID: 5388
		public ParticleSystem particleSystem;

		// Token: 0x0400150D RID: 5389
		public int count;

		// Token: 0x0400150E RID: 5390
		public bool overrideColor;

		// Token: 0x0400150F RID: 5391
		[ConditionalHide("overrideColor", true)]
		public Color color;

		// Token: 0x04001510 RID: 5392
		public float size;

		// Token: 0x04001511 RID: 5393
		public bool overrideAudioSource;

		// Token: 0x04001512 RID: 5394
		[ConditionalHide("overrideAudioSource", true)]
		public AudioSourceVariance audioSource;
	}

	// Token: 0x02000342 RID: 834
	[Serializable]
	public struct ClippingsLoot
	{
		// Token: 0x04001513 RID: 5395
		public GameObject prefab;

		// Token: 0x04001514 RID: 5396
		public float chance;
	}
}
