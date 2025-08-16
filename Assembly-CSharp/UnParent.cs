using System;
using UnityEngine;

public class UnParent : MonoBehaviour
{
	// Token: 0x06000FF7 RID: 4087 RVA: 0x0000DC9A File Offset: 0x0000BE9A
	private void OnValidate()
	{
		if (this.parent == null)
		{
			this.parent = base.transform.parent;
		}
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x0000DCBB File Offset: 0x0000BEBB
	private void Start()
	{
		base.transform.parent = null;
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x000530EC File Offset: 0x000512EC
	private void Update()
	{
		if (this.parent == null || this.parent.gameObject == null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (this.parent == base.transform.parent)
		{
			base.transform.parent = null;
		}
		if (!this.parent.gameObject.activeInHierarchy)
		{
			base.transform.parent = this.parent;
		}
	}

	public Transform parent;
}
