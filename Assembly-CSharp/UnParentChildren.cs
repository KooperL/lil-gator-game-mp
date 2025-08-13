using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200031E RID: 798
public class UnParentChildren : MonoBehaviour
{
	// Token: 0x06000FA0 RID: 4000 RVA: 0x0000D975 File Offset: 0x0000BB75
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

	// Token: 0x06000FA1 RID: 4001 RVA: 0x000513E0 File Offset: 0x0004F5E0
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

	// Token: 0x06000FA2 RID: 4002 RVA: 0x0005143C File Offset: 0x0004F63C
	private void OnEnable()
	{
		Transform[] array = this.children;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].parent = null;
		}
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x00051468 File Offset: 0x0004F668
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

	// Token: 0x0400142A RID: 5162
	public Transform[] children;
}
