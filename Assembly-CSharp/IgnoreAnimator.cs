using System;
using UnityEngine;

// Token: 0x02000189 RID: 393
public class IgnoreAnimator : MonoBehaviour
{
	// Token: 0x06000763 RID: 1891 RVA: 0x000076D0 File Offset: 0x000058D0
	private void Update()
	{
		this.position = base.transform.position;
		this.rotation = base.transform.rotation;
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x000076F4 File Offset: 0x000058F4
	private void LateUpdate()
	{
		base.transform.position = this.position;
		base.transform.rotation = this.rotation;
	}

	// Token: 0x040009D6 RID: 2518
	private Vector3 position;

	// Token: 0x040009D7 RID: 2519
	private Quaternion rotation;
}
