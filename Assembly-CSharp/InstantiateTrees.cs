using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200027F RID: 639
public class InstantiateTrees : MonoBehaviour
{
	// Token: 0x06000DA4 RID: 3492 RVA: 0x0004219C File Offset: 0x0004039C
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

	// Token: 0x06000DA5 RID: 3493 RVA: 0x000422A4 File Offset: 0x000404A4
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

	// Token: 0x06000DA6 RID: 3494 RVA: 0x00042308 File Offset: 0x00040508
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

	// Token: 0x040011F9 RID: 4601
	public InstantiateTrees.TreePrefab[] prefabs;

	// Token: 0x040011FA RID: 4602
	public Terrain terrain;

	// Token: 0x040011FB RID: 4603
	public Transform lodParent;

	// Token: 0x040011FC RID: 4604
	public GameObject[] lodObjects;

	// Token: 0x0200042C RID: 1068
	[Serializable]
	public struct TreePrefab
	{
		// Token: 0x04001D74 RID: 7540
		[HideInInspector]
		public string name;

		// Token: 0x04001D75 RID: 7541
		public GameObject prototype;

		// Token: 0x04001D76 RID: 7542
		[ReadOnly]
		public int index;

		// Token: 0x04001D77 RID: 7543
		public GameObject prefab;

		// Token: 0x04001D78 RID: 7544
		public GameObject lodPrefab;
	}
}
