using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001EC RID: 492
public class ManualDistanceCulling : MonoBehaviour
{
	// Token: 0x06000916 RID: 2326 RVA: 0x00038AD0 File Offset: 0x00036CD0
	private static Vector2Int IndexToCoordinate(int index, int chunkDivisions)
	{
		Vector2Int zero = Vector2Int.zero;
		zero.x = index % chunkDivisions;
		zero.y = Mathf.FloorToInt((float)index / (float)chunkDivisions);
		return zero;
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x00038B00 File Offset: 0x00036D00
	private static float InterpolateCenterAxis(int coordinate, int chunkDivisions, float lowEdge, float highEdge)
	{
		float num = (2f * (float)coordinate + 1f) / (2f * (float)chunkDivisions);
		return Mathf.Lerp(lowEdge, highEdge, num);
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00038B30 File Offset: 0x00036D30
	private static Vector3 GetCenterForCoordinate(Vector2Int coordinate, int chunkDivisions, Bounds bounds)
	{
		return new Vector3(ManualDistanceCulling.InterpolateCenterAxis(coordinate.x, chunkDivisions, bounds.min.x, bounds.max.x), 0f, ManualDistanceCulling.InterpolateCenterAxis(coordinate.y, chunkDivisions, bounds.min.z, bounds.max.z));
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00038B94 File Offset: 0x00036D94
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

	// Token: 0x0600091A RID: 2330 RVA: 0x00008DEE File Offset: 0x00006FEE
	private Bounds ExpandEdgeBounds(Bounds bounds, Vector3 direction)
	{
		bounds.Encapsulate(bounds.center + 200f * direction);
		return bounds;
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00002229 File Offset: 0x00000429
	[ContextMenu("2. Sort Culled Objects Into Chunks")]
	private void SortCulledObjectsIntoChunks()
	{
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00038CB4 File Offset: 0x00036EB4
	private void VerifyObjectsAreSorted()
	{
		int num = 0;
		foreach (ManualDistanceCulling.CulledObjectChunk culledObjectChunk in this.chunks)
		{
			num += culledObjectChunk.chunkObjects.Length;
		}
		Debug.Log("Total chunk objects: " + num.ToString() + " Culled Objects: " + this.culledObjects.Length.ToString());
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00008E0F File Offset: 0x0000700F
	private void OnValidate()
	{
		this.totalChunks = this.chunkDivisions * this.chunkDivisions;
		this.chunkWidth = this.bounds.extents.x / (float)this.chunkDivisions;
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x00008E42 File Offset: 0x00007042
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

	// Token: 0x0600091F RID: 2335 RVA: 0x00038D18 File Offset: 0x00036F18
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

	// Token: 0x06000920 RID: 2336 RVA: 0x00038E74 File Offset: 0x00037074
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

	// Token: 0x06000921 RID: 2337 RVA: 0x00038ED0 File Offset: 0x000370D0
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

	// Token: 0x04000BB8 RID: 3000
	public Transform[] culledObjectParents;

	// Token: 0x04000BB9 RID: 3001
	public Transform[] expensiveCulledObjectParents;

	// Token: 0x04000BBA RID: 3002
	public GameObject[] extraCulledObjects;

	// Token: 0x04000BBB RID: 3003
	public GameObject[] extraExpensiveCulledObjects;

	// Token: 0x04000BBC RID: 3004
	public GameObject[] culledObjects;

	// Token: 0x04000BBD RID: 3005
	public GameObject[] expensiveCulledObjects;

	// Token: 0x04000BBE RID: 3006
	public Bounds bounds;

	// Token: 0x04000BBF RID: 3007
	public int chunkDivisions = 4;

	// Token: 0x04000BC0 RID: 3008
	[ReadOnly]
	public int totalChunks;

	// Token: 0x04000BC1 RID: 3009
	[ReadOnly]
	public float chunkWidth;

	// Token: 0x04000BC2 RID: 3010
	private const float beginCullingDistance = 120f;

	// Token: 0x04000BC3 RID: 3011
	private const float stopCullingDistance = 110f;

	// Token: 0x04000BC4 RID: 3012
	private const float fogDistance = 55f;

	// Token: 0x04000BC5 RID: 3013
	public ManualDistanceCulling.CulledObjectChunk[] chunks;

	// Token: 0x04000BC6 RID: 3014
	private int chunksNeedingChanges;

	// Token: 0x04000BC7 RID: 3015
	private int chunksNeedingUrgentChanges;

	// Token: 0x04000BC8 RID: 3016
	private const int setActiveAllowance = 10;

	// Token: 0x04000BC9 RID: 3017
	private const int setActiveUrgentAllowance = 200;

	// Token: 0x020001ED RID: 493
	[Serializable]
	public struct CulledObjectChunk
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00008E87 File Offset: 0x00007087
		public bool IsEverythingVisible
		{
			get
			{
				return this.visibilityIndex == 0 && this.expensiveVisibilityIndex == 0 && this.resistantIndex == 0;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00008EA4 File Offset: 0x000070A4
		public bool IsEverythingCulled
		{
			get
			{
				return this.visibilityIndex == this.chunkObjects.Length && this.expensiveVisibilityIndex == this.expensiveChunkObjects.Length && this.resistantIndex == this.resistantObjects.Length;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00038F28 File Offset: 0x00037128
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

		// Token: 0x04000BCA RID: 3018
		public Vector2Int coordinate;

		// Token: 0x04000BCB RID: 3019
		public GameObject[] chunkObjects;

		// Token: 0x04000BCC RID: 3020
		public GameObject[] expensiveChunkObjects;

		// Token: 0x04000BCD RID: 3021
		public ResistCulling[] resistantObjects;

		// Token: 0x04000BCE RID: 3022
		public Bounds bounds;

		// Token: 0x04000BCF RID: 3023
		[Space]
		public bool isVisible;

		// Token: 0x04000BD0 RID: 3024
		public int visibilityIndex;

		// Token: 0x04000BD1 RID: 3025
		public int expensiveVisibilityIndex;

		// Token: 0x04000BD2 RID: 3026
		public int resistantIndex;

		// Token: 0x04000BD3 RID: 3027
		public bool needsUpdate;

		// Token: 0x04000BD4 RID: 3028
		public bool needsUrgentUpdate;
	}
}
