using System;
using UnityEngine;

public class PoofObject : MonoBehaviour
{
	// Token: 0x06000B17 RID: 2839 RVA: 0x00037686 File Offset: 0x00035886
	public void Poof()
	{
		Object.Instantiate<GameObject>(this.poofObject, base.transform.position, this.poofObject.transform.rotation);
		Object.Destroy(this.destroyObject);
	}

	public GameObject poofObject;

	public GameObject destroyObject;
}
