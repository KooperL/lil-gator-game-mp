using System;
using UnityEngine;

public class UnParent : MonoBehaviour
{
	// Token: 0x06000FF7 RID: 4087 RVA: 0x0000DCAF File Offset: 0x0000BEAF
	private void OnValidate()
	{
		if (this.parent == null)
		{
			this.parent = base.transform.parent;
		}
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x0000DCD0 File Offset: 0x0000BED0
	private void Start()
	{
		base.transform.parent = null;
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x00053280 File Offset: 0x00051480
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
