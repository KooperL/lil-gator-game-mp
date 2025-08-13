using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200034D RID: 845
public class InstantiateTrees : MonoBehaviour
{
	// Token: 0x06001060 RID: 4192 RVA: 0x00054568 File Offset: 0x00052768
	private void OnValidate()
	{
		for (int i = 0; i < this.prefabs.Length; i++)
		{
			if (this.prefabs[i].prefab != null)
			{
				this.prefabs[i].name = this.prefabs[i].prefab.name;
			}
		}
		if (this.terrain == null)
		{
			this.terrain = base.GetComponentInParent<Terrain>();
		}
		if (this.terrain == null)
		{
			return;
		}
		TerrainData terrainData = this.terrain.terrainData;
		for (int j = 0; j < this.prefabs.Length; j++)
		{
			this.prefabs[j].index = -1;
			for (int k = 0; k < terrainData.treePrototypes.Length; k++)
			{
				if (terrainData.treePrototypes[k].prefab == this.prefabs[j].prototype)
				{
					this.prefabs[j].index = k;
					break;
				}
			}
		}
	}

	// Token: 0x06001061 RID: 4193 RVA: 0x00054670 File Offset: 0x00052870
	private Vector3 TerrainToWorld(Vector3 position)
	{
		Vector3 zero = Vector3.zero;
		Vector3 size = this.terrain.terrainData.size;
		Vector3 position2 = this.terrain.transform.position;
		for (int i = 0; i < 3; i++)
		{
			zero[i] = position[i] * size[i] + position2[i];
		}
		return zero;
	}

	// Token: 0x06001062 RID: 4194 RVA: 0x000546D4 File Offset: 0x000528D4
	private void Start()
	{
		if (this.lodParent != null && this.lodParent.gameObject.activeSelf)
		{
			return;
		}
		TerrainData terrainData = this.terrain.terrainData;
		Dictionary<int, GameObject> dictionary = new Dictionary<int, GameObject>();
		foreach (InstantiateTrees.TreePrefab treePrefab in this.prefabs)
		{
			if (treePrefab.index != -1 && treePrefab.prefab != null)
			{
				dictionary.Add(treePrefab.index, treePrefab.prefab);
			}
		}
		foreach (TreeInstance treeInstance in terrainData.treeInstances)
		{
			GameObject gameObject;
			if (dictionary.TryGetValue(treeInstance.prototypeIndex, out gameObject))
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform);
				gameObject2.transform.position = this.TerrainToWorld(treeInstance.position);
				gameObject2.transform.localScale = new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale);
			}
		}
	}

	// Token: 0x04001539 RID: 5433
	public InstantiateTrees.TreePrefab[] prefabs;

	// Token: 0x0400153A RID: 5434
	public Terrain terrain;

	// Token: 0x0400153B RID: 5435
	public Transform lodParent;

	// Token: 0x0400153C RID: 5436
	public GameObject[] lodObjects;

	// Token: 0x0200034E RID: 846
	[Serializable]
	public struct TreePrefab
	{
		// Token: 0x0400153D RID: 5437
		[HideInInspector]
		public string name;

		// Token: 0x0400153E RID: 5438
		public GameObject prototype;

		// Token: 0x0400153F RID: 5439
		[ReadOnly]
		public int index;

		// Token: 0x04001540 RID: 5440
		public GameObject prefab;

		// Token: 0x04001541 RID: 5441
		public GameObject lodPrefab;
	}
}
