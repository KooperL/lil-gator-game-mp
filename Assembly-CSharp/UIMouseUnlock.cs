using System;
using UnityEngine;

public class UIMouseUnlock : MonoBehaviour
{
	// Token: 0x0600126C RID: 4716 RVA: 0x0000F9D1 File Offset: 0x0000DBD1
	public void OnEnable()
	{
		UIMouseUnlock.unlockDepth++;
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x0000F9DF File Offset: 0x0000DBDF
	private void OnDisable()
	{
		UIMouseUnlock.unlockDepth--;
	}

	public static int unlockDepth;
}
