using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class AnimatorHeadTracking : MonoBehaviour
{
	// Token: 0x060001F9 RID: 505 RVA: 0x0000AE07 File Offset: 0x00009007
	public void StartHeadTracking()
	{
		this.headTracking = true;
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0000AE10 File Offset: 0x00009010
	public void StopHeadTracking()
	{
		this.headTracking = false;
	}

	// Token: 0x0400029D RID: 669
	public bool headTracking = true;
}
