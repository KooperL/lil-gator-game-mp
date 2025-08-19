using System;
using UnityEngine;

public class PersistentManagers : MonoBehaviour
{
	// Token: 0x060009DD RID: 2525 RVA: 0x00009810 File Offset: 0x00007A10
	private void Awake()
	{
		if (PersistentManagers.p != null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		PersistentManagers.p = this;
		global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public static PersistentManagers p;
}
