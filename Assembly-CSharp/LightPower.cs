using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000F1 RID: 241
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

	// Token: 0x040006DF RID: 1759
	public ItemResource lightResource;

	// Token: 0x040006E0 RID: 1760
	private FogLight fogLight;

	// Token: 0x040006E1 RID: 1761
	public float unpoweredStrength = 0.25f;

	// Token: 0x040006E2 RID: 1762
	public int maxLightPower = 20;
}
