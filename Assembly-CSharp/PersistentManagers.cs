using System;
using UnityEngine;

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

	public static PersistentManagers p;
}
