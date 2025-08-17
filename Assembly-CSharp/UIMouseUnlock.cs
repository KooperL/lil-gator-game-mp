using System;
using UnityEngine;

public class UIMouseUnlock : MonoBehaviour
{
	// Token: 0x0600126C RID: 4716 RVA: 0x0000F9C7 File Offset: 0x0000DBC7
	public void OnEnable()
	{
		UIMouseUnlock.unlockDepth++;
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x0000F9D5 File Offset: 0x0000DBD5
	private void OnDisable()
	{
		UIMouseUnlock.unlockDepth--;
	}

	public static int unlockDepth;
}
