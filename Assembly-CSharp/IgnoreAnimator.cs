using System;
using UnityEngine;

public class IgnoreAnimator : MonoBehaviour
{
	// Token: 0x060007A4 RID: 1956 RVA: 0x000079DF File Offset: 0x00005BDF
	private void Update()
	{
		this.position = base.transform.position;
		this.rotation = base.transform.rotation;
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x00007A03 File Offset: 0x00005C03
	private void LateUpdate()
	{
		base.transform.position = this.position;
		base.transform.rotation = this.rotation;
	}

	private Vector3 position;

	private Quaternion rotation;
}
