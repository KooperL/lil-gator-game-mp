using System;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class AltitudeFog : MonoBehaviour
{
	// Token: 0x06000224 RID: 548 RVA: 0x00003C00 File Offset: 0x00001E00
	private void LateUpdate()
	{
		RenderSettings.fogDensity = Mathf.Lerp(this.lowFogDensity, this.highFogDensity, Mathf.InverseLerp(this.lowHeight, this.highHeight, base.transform.position.y));
	}

	// Token: 0x04000315 RID: 789
	public float lowFogDensity = 0.014f;

	// Token: 0x04000316 RID: 790
	public float lowHeight = 50f;

	// Token: 0x04000317 RID: 791
	public float highFogDensity = 0.008f;

	// Token: 0x04000318 RID: 792
	public float highHeight = 100f;
}
