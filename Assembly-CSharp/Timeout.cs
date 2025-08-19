using System;
using UnityEngine;

public class Timeout : MonoBehaviour
{
	// Token: 0x06000FE0 RID: 4064 RVA: 0x0000DB25 File Offset: 0x0000BD25
	private void Start()
	{
		base.Invoke("Destroy", this.delay);
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x000049DF File Offset: 0x00002BDF
	private void Destroy()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	public float delay = 0.5f;
}
