using System;
using UnityEngine;

public class PoofObject : MonoBehaviour
{
	// Token: 0x06000D28 RID: 3368 RVA: 0x0000C273 File Offset: 0x0000A473
	public void Poof()
	{
		global::UnityEngine.Object.Instantiate<GameObject>(this.poofObject, base.transform.position, this.poofObject.transform.rotation);
		global::UnityEngine.Object.Destroy(this.destroyObject);
	}

	public GameObject poofObject;

	public GameObject destroyObject;
}
