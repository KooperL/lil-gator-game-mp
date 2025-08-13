using System;
using UnityEngine;

// Token: 0x02000319 RID: 793
public class Timeout : MonoBehaviour
{
	// Token: 0x06000F85 RID: 3973 RVA: 0x0000D7B2 File Offset: 0x0000B9B2
	private void Start()
	{
		base.Invoke("Destroy", this.delay);
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x000047FB File Offset: 0x000029FB
	private void Destroy()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04001415 RID: 5141
	public float delay = 0.5f;
}
