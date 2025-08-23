using System;
using UnityEngine;

public class PoofObject : MonoBehaviour
{
	// Token: 0x06000D29 RID: 3369 RVA: 0x0000C292 File Offset: 0x0000A492
	public void Poof()
	{
		global::UnityEngine.Object.Instantiate<GameObject>(this.poofObject, base.transform.position, this.poofObject.transform.rotation);
		global::UnityEngine.Object.Destroy(this.destroyObject);
	}

	public GameObject poofObject;

	public GameObject destroyObject;
}
