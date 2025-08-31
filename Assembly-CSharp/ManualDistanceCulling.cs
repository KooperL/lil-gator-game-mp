using System;
using System.Collections.Generic;
using UnityEngine;

public class ManualDistanceCulling : MonoBehaviour
{
	// Token: 0x060007B7 RID: 1975 RVA: 0x000259FC File Offset: 0x00023BFC
	private static Vector2Int IndexToCoordinate(int index, int chunkDivisions)
	{
		Vector2Int zero = Vector2Int.zero;
		zero.x = index % chunkDivisions;
		zero.y = Mathf.FloorToInt((float)index / (float)chunkDivisions);
		return zero;
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x00025A2C File Offset: 0x00023C2C
	private static float InterpolateCenterAxis(int coordinate, int chunkDivisions, float lowEdge, float highEdge)
	{
		float num = (2f * (float)coordinate + 1f) / (2f * (float)chunkDivisions);
		return Mathf.Lerp(lowEdge, highEdge, num);
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x00025A5C File Offset: 0x00023C5C
	private static Vector3 GetCenterForCoordinate(Vector2Int coordinate, int chunkDivisions, Bounds bounds)
	{
		return new Vector3(ManualDistanceCulling.InterpolateCenterAxis(coordinate.x, chunkDivisions, bounds.min.x, bounds.max.x), 0f, ManualDistanceCulling.InterpolateCenterAxis(coordinate.y, chunkDivisions, bounds.min.z, bounds.max.z));
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x00025AC0 File Offset: 0x00023CC0
	[ContextMenu("1. Collect Culled Objects")]
	private void CollectCulledObjects()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (Transform transform in this.culledObjectParents)
		{
			for (int j = 0; j < transform.childCount; j++)
			{
				GameObject gameObject = transform.GetChild(j).gameObject;
				if (gameObject.activeSelf)
				{
					list.Add(gameObject);
				}
			}
		}
		foreach (GameObject gameObject2 in this.extraCulledObjects)
		{
			list.Add(gameObject2);
		}
		this.culledObjects = list.ToArray();
		List<GameObject> list2 = new List<GameObject>();
		foreach (Transform transform2 in this.expensiveCulledObjectParents)
		{
			for (int k = 0; k < transform2.childCount; k++)
			{
				GameObject gameObject3 = transform2.GetChild(k).gameObject;
				if (gameObject3.activeSelf)
				{
					list2.Add(gameObject3);
				}
			}
		}
		foreach (GameObject gameObject4 in this.extraExpensiveCulledObjects)
		{
			list2.Add(gameObject4);
		}
		this.expensiveCulledObjects = list2.ToArray();
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x00025BDD File Offset: 0x00023DDD
	private Bounds ExpandEdgeBounds(Bounds bounds, Vector3 direction)
	{
		bounds.Encapsulate(bounds.center + 200f * direction);
		return bounds;
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x00025BFE File Offset: 0x00023DFE
	[ContextMenu("2. Sort Culled Objects Into Chunks")]
	private void SortCulledObjectsIntoChunks()
	{
	}

	// Token: 0x060007BD RID: 1981 RVA: 0x00025C00 File Offset: 0x00023E00
	private void VerifyObjectsAreSorted()
	{
		int num = 0;
		foreach (ManualDistanceCulling.CulledObjectChunk culledObjectChunk in this.chunks)
		{
			num += culledObjectChunk.chunkObjects.Length;
		}
		Debug.Log("Total chunk objects: " + num.ToString() + " Culled Objects: " + this.culledObjects.Length.ToString());
	}

	// Token: 0x060007BE RID: 1982 RVA: 0x00025C62 File Offset: 0x00023E62
	private void OnValidate()
	{
		this.totalChunks = this.chunkDivisions * this.chunkDivisions;
		this.chunkWidth = this.bounds.extents.x / (float)this.chunkDivisions;
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x00025C95 File Offset: 0x00023E95
	private void LateUpdate()
	{
		if (Game.WorldState == WorldState.Flashback)
		{
			return;
		}
		this.chunksNeedingChanges = 0;
		this.chunksNeedingUrgentChanges = 0;
		this.UpdateChunkVisibility();
		if (this.chunksNeedingUrgentChanges > 0)
		{
			this.UpdateUrgentChunkObjects();
			return;
		}
		this.UpdateChunkObjects();
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x00025CCC File Offset: 0x00023ECC
	private void UpdateChunkVisibility()
	{
		Vector3 position = MainCamera.t.position;
		for (int i = 0; i < this.chunks.Length; i++)
		{
			float num = this.chunks[i].bounds.SqrDistance(position);
			if (this.chunks[i].isVisible)
			{
				if (num > 14400f)
				{
					this.chunks[i].isVisible = false;
				}
			}
			else if (num < 12100f)
			{
				this.chunks[i].isVisible = true;
			}
			this.chunks[i].needsUrgentUpdate = false;
			this.chunks[i].needsUpdate = false;
			if (this.chunks[i].isVisible)
			{
				if (!this.chunks[i].IsEverythingVisible)
				{
					this.chunks[i].needsUpdate = true;
					this.chunksNeedingChanges++;
					if (num < 3025f)
					{
						this.chunksNeedingUrgentChanges++;
						this.chunks[i].needsUrgentUpdate = true;
					}
				}
			}
			else if (!this.chunks[i].IsEverythingCulled)
			{
				this.chunks[i].needsUpdate = true;
				this.chunksNeedingChanges++;
			}
		}
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x00025E28 File Offset: 0x00024028
	private void UpdateChunkObjects()
	{
		bool flag = true;
		for (int i = 0; i < this.chunks.Length; i++)
		{
			if (this.chunks[i].needsUpdate)
			{
				flag = this.chunks[i].UpdateObjectVisibility(Mathf.CeilToInt(10f / (float)this.chunksNeedingChanges), flag);
			}
		}
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00025E84 File Offset: 0x00024084
	private void UpdateUrgentChunkObjects()
	{
		for (int i = 0; i < this.chunks.Length; i++)
		{
			if (this.chunks[i].needsUrgentUpdate)
			{
				this.chunks[i].UpdateObjectVisibility(Mathf.CeilToInt(200f / (float)this.chunksNeedingChanges), true);
			}
		}
	}

	public Transform[] culledObjectParents;

	public Transform[] expensiveCulledObjectParents;

	public GameObject[] extraCulledObjects;

	public GameObject[] extraExpensiveCulledObjects;

	public GameObject[] culledObjects;

	public GameObject[] expensiveCulledObjects;

	public Bounds bounds;

	public int chunkDivisions = 4;

	[ReadOnly]
	public int totalChunks;

	[ReadOnly]
	public float chunkWidth;

	private const float beginCullingDistance = 120f;

	private const float stopCullingDistance = 110f;

	private const float fogDistance = 55f;

	public ManualDistanceCulling.CulledObjectChunk[] chunks;

	private int chunksNeedingChanges;

	private int chunksNeedingUrgentChanges;

	private const int setActiveAllowance = 10;

	private const int setActiveUrgentAllowance = 200;

	[Serializable]
	public struct CulledObjectChunk
	{
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x0006D633 File Offset: 0x0006B833
		public bool IsEverythingVisible
		{
			get
			{
				return this.visibilityIndex == 0 && this.expensiveVisibilityIndex == 0 && this.resistantIndex == 0;
			}
		}

		// (get) Token: 0x06001994 RID: 6548 RVA: 0x0006D650 File Offset: 0x0006B850
		public bool IsEverythingCulled
		{
			get
			{
				return this.visibilityIndex == this.chunkObjects.Length && this.expensiveVisibilityIndex == this.expensiveChunkObjects.Length && this.resistantIndex == this.resistantObjects.Length;
			}
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0006D684 File Offset: 0x0006B884
		public bool UpdateObjectVisibility(int allowance, bool allowExpensive)
		{
			while (allowance > 0)
			{
				if (this.isVisible)
				{
					if (this.visibilityIndex == 0 && this.expensiveVisibilityIndex > 0)
					{
						this.expensiveVisibilityIndex--;
						this.expensiveChunkObjects[this.expensiveVisibilityIndex].SetActive(true);
						return false;
					}
					if (this.resistantIndex > 0)
					{
						this.resistantIndex--;
						this.resistantObjects[this.resistantIndex].gameObject.SetActive(true);
						continue;
					}
					if (this.visibilityIndex > 0)
					{
						this.visibilityIndex--;
						this.chunkObjects[this.visibilityIndex].SetActive(true);
					}
					if (this.visibilityIndex == 0)
					{
						break;
					}
				}
				else
				{
					if (this.visibilityIndex == this.chunkObjects.Length && this.expensiveVisibilityIndex < this.expensiveChunkObjects.Length)
					{
						this.expensiveChunkObjects[this.expensiveVisibilityIndex].SetActive(false);
						this.expensiveVisibilityIndex++;
						return false;
					}
					if (this.resistantIndex < this.resistantObjects.Length && !this.resistantObjects[this.resistantIndex].enabled)
					{
						this.resistantObjects[this.resistantIndex].gameObject.SetActive(false);
						this.resistantIndex++;
						continue;
					}
					if (this.visibilityIndex < this.chunkObjects.Length)
					{
						this.chunkObjects[this.visibilityIndex].SetActive(false);
						this.visibilityIndex++;
					}
					if (this.visibilityIndex == this.chunkObjects.Length)
					{
						break;
					}
				}
				allowance--;
			}
			return true;
		}

		public Vector2Int coordinate;

		public GameObject[] chunkObjects;

		public GameObject[] expensiveChunkObjects;

		public ResistCulling[] resistantObjects;

		public Bounds bounds;

		[Space]
		public bool isVisible;

		public int visibilityIndex;

		public int expensiveVisibilityIndex;

		public int resistantIndex;

		public bool needsUpdate;

		public bool needsUrgentUpdate;
	}
}
