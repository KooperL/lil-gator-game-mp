using System;
using UnityEngine;

// Token: 0x020003B4 RID: 948
public class UIMouseUnlock : MonoBehaviour
{
	// Token: 0x0600120C RID: 4620 RVA: 0x0000F5DE File Offset: 0x0000D7DE
	public void OnEnable()
	{
		UIMouseUnlock.unlockDepth++;
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x0000F5EC File Offset: 0x0000D7EC
	private void OnDisable()
	{
		UIMouseUnlock.unlockDepth--;
	}

	// Token: 0x0400177C RID: 6012
	public static int unlockDepth;
}
