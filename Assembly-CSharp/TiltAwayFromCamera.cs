using System;
using UnityEngine;

// Token: 0x02000351 RID: 849
public class TiltAwayFromCamera : MonoBehaviour
{
	// Token: 0x0600106A RID: 4202 RVA: 0x00002229 File Offset: 0x00000429
	private void Start()
	{
	}

	// Token: 0x04001546 RID: 5446
	public float tiltDistance = 3f;

	// Token: 0x04001547 RID: 5447
	public float tiltAmount = 30f;

	// Token: 0x04001548 RID: 5448
	private Quaternion rotation;
}
