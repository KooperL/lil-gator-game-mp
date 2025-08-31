using System;
using UnityEngine;

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

	private Vector3 position;

	private Quaternion rotation;
}
