using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200034B RID: 843
public class SyncTreeShadows : MonoBehaviour
{
	// Token: 0x06001059 RID: 4185 RVA: 0x00054358 File Offset: 0x00052558
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

	// Token: 0x0600105A RID: 4186 RVA: 0x00054394 File Offset: 0x00052594
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

	// Token: 0x04001536 RID: 5430
	public int[] treeShadowIndices;
}
