using System;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class AnimatorHeadTracking : MonoBehaviour
{
	// Token: 0x06000230 RID: 560 RVA: 0x00003C9D File Offset: 0x00001E9D
	public void StartHeadTracking()
	{
		this.headTracking = true;
	}

	// Token: 0x06000231 RID: 561 RVA: 0x00003CA6 File Offset: 0x00001EA6
	public void StopHeadTracking()
	{
		this.headTracking = false;
	}

	// Token: 0x04000323 RID: 803
	public bool headTracking = true;
}
