using System;
using UnityEngine;

// Token: 0x020001FE RID: 510
public class PoofObject : MonoBehaviour
{
	// Token: 0x06000B17 RID: 2839 RVA: 0x00037686 File Offset: 0x00035886
	public void Poof()
	{
		Object.Instantiate<GameObject>(this.poofObject, base.transform.position, this.poofObject.transform.rotation);
		Object.Destroy(this.destroyObject);
	}

	// Token: 0x04000ECA RID: 3786
	public GameObject poofObject;

	// Token: 0x04000ECB RID: 3787
	public GameObject destroyObject;
}
