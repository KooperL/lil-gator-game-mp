using System;
using UnityEngine;

// Token: 0x0200031D RID: 797
public class UnParent : MonoBehaviour
{
	// Token: 0x06000F9C RID: 3996 RVA: 0x0000D946 File Offset: 0x0000BB46
	private void OnValidate()
	{
		if (this.parent == null)
		{
			this.parent = base.transform.parent;
		}
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x0000D967 File Offset: 0x0000BB67
	private void Start()
	{
		base.transform.parent = null;
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x0005135C File Offset: 0x0004F55C
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

	// Token: 0x04001429 RID: 5161
	public Transform parent;
}
