using System;
using UnityEngine;

// Token: 0x020001AD RID: 429
public class InteractDestroy : MonoBehaviour, Interaction
{
	// Token: 0x060007F3 RID: 2035 RVA: 0x000047FB File Offset: 0x000029FB
	public void Interact()
	{
		Object.Destroy(base.gameObject);
	}
}
