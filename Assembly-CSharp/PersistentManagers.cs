using System;
using UnityEngine;

// Token: 0x02000205 RID: 517
public class PersistentManagers : MonoBehaviour
{
	// Token: 0x06000996 RID: 2454 RVA: 0x000094D2 File Offset: 0x000076D2
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

	// Token: 0x04000C3F RID: 3135
	public static PersistentManagers p;
}
