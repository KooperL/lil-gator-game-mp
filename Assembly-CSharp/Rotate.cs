using System;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class Rotate : MonoBehaviour
{
	// Token: 0x06000533 RID: 1331 RVA: 0x0001BD86 File Offset: 0x00019F86
	private void Update()
	{
		base.transform.Rotate(this.rotation * Time.deltaTime);
	}

	// Token: 0x04000724 RID: 1828
	public Vector3 rotation;
}
