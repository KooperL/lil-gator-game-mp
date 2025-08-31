using System;
using System.Collections.Generic;
using UnityEngine;

public class SyncTreeShadows : MonoBehaviour
{
	// Token: 0x06000D9D RID: 3485 RVA: 0x00041F44 File Offset: 0x00040144
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

	// Token: 0x06000D9E RID: 3486 RVA: 0x00041F80 File Offset: 0x00040180
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
				list.Add(new TreeInstance
				{
					prototypeIndex = num,
					heightScale = list[k].heightScale,
					position = list[k].position,
					rotation = list[k].rotation,
					widthScale = list[k].widthScale
				});
			}
		}
		component.terrainData.SetTreeInstances(list.ToArray(), true);
	}

	public int[] treeShadowIndices;
}
