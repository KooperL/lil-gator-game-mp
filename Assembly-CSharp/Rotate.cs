using System;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	// Token: 0x0600067F RID: 1663 RVA: 0x00006AFB File Offset: 0x00004CFB
	private void Update()
	{
		base.transform.Rotate(this.rotation * Time.deltaTime);
	}

	public Vector3 rotation;
}
