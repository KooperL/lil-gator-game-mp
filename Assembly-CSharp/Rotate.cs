using System;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class Rotate : MonoBehaviour
{
	// Token: 0x06000645 RID: 1605 RVA: 0x00006835 File Offset: 0x00004A35
	private void Update()
	{
		base.transform.Rotate(this.rotation * Time.deltaTime);
	}

	// Token: 0x0400086B RID: 2155
	public Vector3 rotation;
}
