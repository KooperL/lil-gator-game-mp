using System;
using UnityEngine;

public class Move : MonoBehaviour
{
	// Token: 0x0600051D RID: 1309 RVA: 0x0001B664 File Offset: 0x00019864
	private void Update()
	{
		base.transform.position += Time.deltaTime * base.transform.TransformDirection(this.velocity);
	}

	public Vector3 velocity;
}
