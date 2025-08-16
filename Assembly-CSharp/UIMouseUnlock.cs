using System;
using UnityEngine;

public class UIMouseUnlock : MonoBehaviour
{
	// Token: 0x0600126C RID: 4716 RVA: 0x0000F9B2 File Offset: 0x0000DBB2
	public void OnEnable()
	{
		UIMouseUnlock.unlockDepth++;
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
	private void OnDisable()
	{
		UIMouseUnlock.unlockDepth--;
	}

	public static int unlockDepth;
}
