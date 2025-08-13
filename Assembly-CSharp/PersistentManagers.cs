using System;
using UnityEngine;

// Token: 0x0200018E RID: 398
public class PersistentManagers : MonoBehaviour
{
	// Token: 0x06000827 RID: 2087 RVA: 0x00027121 File Offset: 0x00025321
	private void Awake()
	{
		if (PersistentManagers.p != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		PersistentManagers.p = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x04000A53 RID: 2643
	public static PersistentManagers p;
}
