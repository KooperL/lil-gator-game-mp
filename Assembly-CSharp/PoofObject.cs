using System;
using UnityEngine;

// Token: 0x02000290 RID: 656
public class PoofObject : MonoBehaviour
{
	// Token: 0x06000CDC RID: 3292 RVA: 0x0000BF80 File Offset: 0x0000A180
	public void Poof()
	{
		Object.Instantiate<GameObject>(this.poofObject, base.transform.position, this.poofObject.transform.rotation);
		Object.Destroy(this.destroyObject);
	}

	// Token: 0x04001129 RID: 4393
	public GameObject poofObject;

	// Token: 0x0400112A RID: 4394
	public GameObject destroyObject;
}
