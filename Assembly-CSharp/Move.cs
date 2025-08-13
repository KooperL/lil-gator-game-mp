using System;
using UnityEngine;

// Token: 0x0200014A RID: 330
public class Move : MonoBehaviour
{
	// Token: 0x0600062F RID: 1583 RVA: 0x0000673D File Offset: 0x0000493D
	private void Update()
	{
		base.transform.position += Time.deltaTime * base.transform.TransformDirection(this.velocity);
	}

	// Token: 0x0400084C RID: 2124
	public Vector3 velocity;
}
