using System;
using UnityEngine;

public class Move : MonoBehaviour
{
	// Token: 0x06000669 RID: 1641 RVA: 0x00006A03 File Offset: 0x00004C03
	private void Update()
	{
		base.transform.position += Time.deltaTime * base.transform.TransformDirection(this.velocity);
	}

	public Vector3 velocity;
}
