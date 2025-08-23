using System;
using UnityEngine;

public class ShowIfSaveData : MonoBehaviour
{
	// Token: 0x06001154 RID: 4436 RVA: 0x0000ED90 File Offset: 0x0000CF90
	private void OnEnable()
	{
		if (!FileUtil.HasInitializedSaveData())
		{
			base.enabled = false;
		}
	}

	public bool invert;
}
