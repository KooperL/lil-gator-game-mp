using System;
using UnityEngine;

// Token: 0x020002D0 RID: 720
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

	// Token: 0x040013F8 RID: 5112
	public static int unlockDepth;
}
