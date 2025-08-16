using System;
using System.Collections.Generic;
using UnityEngine;

public class UnParentChildren : MonoBehaviour
{
	// Token: 0x06000FFB RID: 4091 RVA: 0x0000DCC9 File Offset: 0x0000BEC9
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

	// Token: 0x06000FFC RID: 4092 RVA: 0x00053170 File Offset: 0x00051370
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

	// Token: 0x06000FFD RID: 4093 RVA: 0x000531CC File Offset: 0x000513CC
	private void OnEnable()
	{
		Transform[] array = this.children;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].parent = null;
		}
	}

	// Token: 0x06000FFE RID: 4094 RVA: 0x000531F8 File Offset: 0x000513F8
	private void OnDisable()
	{
		foreach (Transform transform in this.children)
		{
			if (this == null)
			{
				global::UnityEngine.Object.Destroy(transform.gameObject);
			}
			else
			{
				transform.parent = base.transform;
			}
		}
	}

	public Transform[] children;
}
