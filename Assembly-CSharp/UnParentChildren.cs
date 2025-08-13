using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000258 RID: 600
public class UnParentChildren : MonoBehaviour
{
	// Token: 0x06000CF3 RID: 3315 RVA: 0x0003E981 File Offset: 0x0003CB81
	public void OnValidate()
	{
		if (this.children == null || this.children.Length == 0)
		{
			this.GetChildren();
		}
		if (this.children != null)
		{
			int num = this.children.Length;
		}
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0003E9AC File Offset: 0x0003CBAC
	[ContextMenu("Get Children")]
	public void GetChildren()
	{
		List<Transform> list = new List<Transform>(base.transform.GetComponentsInChildren<Transform>());
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == base.transform)
			{
				list.RemoveAt(i);
				i--;
			}
		}
		this.children = list.ToArray();
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0003EA08 File Offset: 0x0003CC08
	private void OnEnable()
	{
		Transform[] array = this.children;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].parent = null;
		}
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x0003EA34 File Offset: 0x0003CC34
	private void OnDisable()
	{
		foreach (Transform transform in this.children)
		{
			if (this == null)
			{
				Object.Destroy(transform.gameObject);
			}
			else
			{
				transform.parent = base.transform;
			}
		}
	}

	// Token: 0x0400110E RID: 4366
	public Transform[] children;
}
