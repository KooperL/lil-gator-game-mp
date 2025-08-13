using System;
using UnityEngine;

// Token: 0x0200012E RID: 302
public class IgnoreAnimator : MonoBehaviour
{
	// Token: 0x0600063E RID: 1598 RVA: 0x00020543 File Offset: 0x0001E743
	private void Update()
	{
		this.position = base.transform.position;
		this.rotation = base.transform.rotation;
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x00020567 File Offset: 0x0001E767
	private void LateUpdate()
	{
		base.transform.position = this.position;
		base.transform.rotation = this.rotation;
	}

	// Token: 0x0400086C RID: 2156
	private Vector3 position;

	// Token: 0x0400086D RID: 2157
	private Quaternion rotation;
}
