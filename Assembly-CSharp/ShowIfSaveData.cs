using System;
using UnityEngine;

// Token: 0x02000375 RID: 885
public class ShowIfSaveData : MonoBehaviour
{
	// Token: 0x060010F3 RID: 4339 RVA: 0x0000E99D File Offset: 0x0000CB9D
	private void OnEnable()
	{
		if (!FileUtil.HasInitializedSaveData())
		{
			base.enabled = false;
		}
	}

	// Token: 0x040015F2 RID: 5618
	public bool invert;
}
