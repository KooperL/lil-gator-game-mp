using System;
using UnityEngine;

// Token: 0x02000253 RID: 595
public class Timeout : MonoBehaviour
{
	// Token: 0x06000CD8 RID: 3288 RVA: 0x0003E287 File Offset: 0x0003C487
	private void Start()
	{
		base.Invoke("Destroy", this.delay);
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0003E29A File Offset: 0x0003C49A
	private void Destroy()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040010F9 RID: 4345
	public float delay = 0.5f;
}
