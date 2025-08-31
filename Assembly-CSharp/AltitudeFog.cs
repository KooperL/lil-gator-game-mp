using System;
using UnityEngine;

public class AltitudeFog : MonoBehaviour
{
	// Token: 0x060001ED RID: 493 RVA: 0x0000A932 File Offset: 0x00008B32
	private void LateUpdate()
	{
		RenderSettings.fogDensity = Mathf.Lerp(this.lowFogDensity, this.highFogDensity, Mathf.InverseLerp(this.lowHeight, this.highHeight, base.transform.position.y));
	}

	public float lowFogDensity = 0.014f;

	public float lowHeight = 50f;

	public float highFogDensity = 0.008f;

	public float highHeight = 100f;
}
