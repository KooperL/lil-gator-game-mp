using System;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class Move : MonoBehaviour
{
	// Token: 0x0600051D RID: 1309 RVA: 0x0001B664 File Offset: 0x00019864
	private void Update()
	{
		base.transform.position += Time.deltaTime * base.transform.TransformDirection(this.velocity);
	}

	// Token: 0x04000705 RID: 1797
	public Vector3 velocity;
}
