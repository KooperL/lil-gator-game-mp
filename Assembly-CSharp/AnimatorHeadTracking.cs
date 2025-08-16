using System;
using UnityEngine;

public class AnimatorHeadTracking : MonoBehaviour
{
	// Token: 0x0600023D RID: 573 RVA: 0x00003D89 File Offset: 0x00001F89
	public void StartHeadTracking()
	{
		this.headTracking = true;
	}

	// Token: 0x0600023E RID: 574 RVA: 0x00003D92 File Offset: 0x00001F92
	public void StopHeadTracking()
	{
		this.headTracking = false;
	}

	public bool headTracking = true;
}
