using System;
using UnityEngine;

public class AltitudeFog : MonoBehaviour
{
	// Token: 0x06000231 RID: 561 RVA: 0x00003CEC File Offset: 0x00001EEC
	private void LateUpdate()
	{
		RenderSettings.fogDensity = Mathf.Lerp(this.lowFogDensity, this.highFogDensity, Mathf.InverseLerp(this.lowHeight, this.highHeight, base.transform.position.y));
	}

	public float lowFogDensity = 0.014f;

	public float lowHeight = 50f;

	public float highFogDensity = 0.008f;

	public float highHeight = 100f;
}
