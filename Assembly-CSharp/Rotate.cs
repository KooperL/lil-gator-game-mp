using System;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	// Token: 0x06000533 RID: 1331 RVA: 0x0001BD86 File Offset: 0x00019F86
	private void Update()
	{
		base.transform.Rotate(this.rotation * Time.deltaTime);
	}

	public Vector3 rotation;
}
