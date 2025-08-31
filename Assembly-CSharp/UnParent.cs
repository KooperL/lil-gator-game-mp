using System;
using UnityEngine;

public class UnParent : MonoBehaviour
{
	// Token: 0x06000CEF RID: 3311 RVA: 0x0003E8C7 File Offset: 0x0003CAC7
	private void OnValidate()
	{
		if (this.parent == null)
		{
			this.parent = base.transform.parent;
		}
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x0003E8E8 File Offset: 0x0003CAE8
	private void Start()
	{
		base.transform.parent = null;
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x0003E8F8 File Offset: 0x0003CAF8
	private void Update()
	{
		if (this.parent == null || this.parent.gameObject == null)
		{
			Object.Destroy(base.gameObject);
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
