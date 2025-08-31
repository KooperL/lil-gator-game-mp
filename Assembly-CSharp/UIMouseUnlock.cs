using System;
using UnityEngine;

public class UIMouseUnlock : MonoBehaviour
{
	// Token: 0x06000F28 RID: 3880 RVA: 0x000492B2 File Offset: 0x000474B2
	public void OnEnable()
	{
		UIMouseUnlock.unlockDepth++;
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x000492C0 File Offset: 0x000474C0
	private void OnDisable()
	{
		UIMouseUnlock.unlockDepth--;
	}

	public static int unlockDepth;
}
