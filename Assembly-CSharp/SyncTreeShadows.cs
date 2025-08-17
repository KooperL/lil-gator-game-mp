using System;
using System.Collections.Generic;
using UnityEngine;

public class SyncTreeShadows : MonoBehaviour
{
	// Token: 0x060010B4 RID: 4276 RVA: 0x0005627C File Offset: 0x0005447C
	private void OnValidate()
	{
		for (int i = 0; i < this.treeShadowIndices.Length; i++)
		{
			if (this.treeShadowIndices[i] == 0)
			{
				this.treeShadowIndices[i] = -1;
			}
		}
		this.UpdateTreeShadows();
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x000562B8 File Offset: 0x000544B8
	[ContextMenu("Update Tree Shadows")]
	public void UpdateTreeShadows()
	{
		Terrain component = base.GetComponent<Terrain>();
		List<TreeInstance> list = new List<TreeInstance>(component.terrainData.treeInstances);
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < this.treeShadowIndices.Length; i++)
		{
			if (this.treeShadowIndices[i] != -1)
			{
				dictionary.Add(i, this.treeShadowIndices[i]);
			}
		}
		List<int> list2 = new List<int>(dictionary.Values);
		for (int j = 0; j < list.Count; j++)
		{
			if (list2.Contains(list[j].prototypeIndex))
			{
				list.RemoveAt(j);
				j--;
			}
		}
		for (int k = 0; k < list.Count; k++)
		{
			int num;
			if (dictionary.TryGetValue(list[k].prototypeIndex, out num))
			{
				TreeInstance treeInstance = default(TreeInstance);
				treeInstance.prototypeIndex = num;
				treeInstance.heightScale = list[k].heightScale;
				treeInstance.position = list[k].position;
				treeInstance.rotation = list[k].rotation;
				treeInstance.widthScale = list[k].widthScale;
				list.Add(treeInstance);
			}
		}
		component.terrainData.SetTreeInstances(list.ToArray(), true);
	}

	public int[] treeShadowIndices;
}
