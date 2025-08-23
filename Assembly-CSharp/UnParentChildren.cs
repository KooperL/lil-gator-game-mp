using System;
using System.Collections.Generic;
using UnityEngine;

public class UnParentChildren : MonoBehaviour
{
	// Token: 0x06000FFC RID: 4092 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
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

	// Token: 0x06000FFD RID: 4093 RVA: 0x000535CC File Offset: 0x000517CC
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

	// Token: 0x06000FFE RID: 4094 RVA: 0x00053628 File Offset: 0x00051828
	private void OnEnable()
	{
		Transform[] array = this.children;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].parent = null;
		}
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x00053654 File Offset: 0x00051854
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
