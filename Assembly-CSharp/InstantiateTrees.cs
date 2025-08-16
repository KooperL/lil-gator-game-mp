using System;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTrees : MonoBehaviour
{
	// Token: 0x060010BB RID: 4283 RVA: 0x000562F8 File Offset: 0x000544F8
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

	// Token: 0x060010BC RID: 4284 RVA: 0x00056400 File Offset: 0x00054600
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

	// Token: 0x060010BD RID: 4285 RVA: 0x00056464 File Offset: 0x00054664
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
				GameObject gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(gameObject, base.transform);
				gameObject2.transform.position = this.TerrainToWorld(treeInstance.position);
				gameObject2.transform.localScale = new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale);
			}
		}
	}

	public InstantiateTrees.TreePrefab[] prefabs;

	public Terrain terrain;

	public Transform lodParent;

	public GameObject[] lodObjects;

	[Serializable]
	public struct TreePrefab
	{
		[HideInInspector]
		public string name;

		public GameObject prototype;

		[ReadOnly]
		public int index;

		public GameObject prefab;

		public GameObject lodPrefab;
	}
}
