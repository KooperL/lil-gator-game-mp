using System;
using UnityEngine;

public class IgnoreAnimator : MonoBehaviour
{
	// Token: 0x060007A3 RID: 1955 RVA: 0x000079CA File Offset: 0x00005BCA
	private void Update()
	{
		this.position = base.transform.position;
		this.rotation = base.transform.rotation;
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x000079EE File Offset: 0x00005BEE
	private void LateUpdate()
	{
		base.transform.position = this.position;
		base.transform.rotation = this.rotation;
	}

	private Vector3 position;

	private Quaternion rotation;
}
