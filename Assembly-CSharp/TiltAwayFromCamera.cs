using System;
using UnityEngine;

// Token: 0x02000281 RID: 641
public class TiltAwayFromCamera : MonoBehaviour
{
	// Token: 0x06000DAE RID: 3502 RVA: 0x0004246B File Offset: 0x0004066B
	private void Start()
	{
	}

	// Token: 0x040011FF RID: 4607
	public float tiltDistance = 3f;

	// Token: 0x04001200 RID: 4608
	public float tiltAmount = 30f;

	// Token: 0x04001201 RID: 4609
	private Quaternion rotation;
}
