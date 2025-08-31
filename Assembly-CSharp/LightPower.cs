using System;
using UnityEngine;
using UnityEngine.Events;

public class LightPower : MonoBehaviour
{
	// Token: 0x060004FE RID: 1278 RVA: 0x0001AD76 File Offset: 0x00018F76
	private void OnValidate()
	{
		if (this.fogLight == null)
		{
			this.fogLight = base.GetComponent<FogLight>();
		}
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x0001AD92 File Offset: 0x00018F92
	private void OnEnable()
	{
		this.lightResource.onAmountChanged.AddListener(new UnityAction<int>(this.LightPowerChanged));
		this.LightPowerChanged(this.lightResource.Amount);
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x0001ADC4 File Offset: 0x00018FC4
	private void LightPowerChanged(int lightPower)
	{
		float num = Mathf.Lerp(this.unpoweredStrength, 1f, (float)lightPower / (float)this.maxLightPower);
		this.fogLight.SetStrength(num, false);
	}

	public ItemResource lightResource;

	private FogLight fogLight;

	public float unpoweredStrength = 0.25f;

	public int maxLightPower = 20;
}
