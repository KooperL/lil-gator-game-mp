using System;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class AltitudeFog : MonoBehaviour
{
	// Token: 0x060001ED RID: 493 RVA: 0x0000A932 File Offset: 0x00008B32
	private void LateUpdate()
	{
		RenderSettings.fogDensity = Mathf.Lerp(this.lowFogDensity, this.highFogDensity, Mathf.InverseLerp(this.lowHeight, this.highHeight, base.transform.position.y));
	}

	// Token: 0x0400028F RID: 655
	public float lowFogDensity = 0.014f;

	// Token: 0x04000290 RID: 656
	public float lowHeight = 50f;

	// Token: 0x04000291 RID: 657
	public float highFogDensity = 0.008f;

	// Token: 0x04000292 RID: 658
	public float highHeight = 100f;
}
