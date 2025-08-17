using System;
using UnityEngine;
using UnityEngine.Events;

public class LightPower : MonoBehaviour
{
	// Token: 0x0600064A RID: 1610 RVA: 0x000067FF File Offset: 0x000049FF
	private void OnValidate()
	{
		if (this.fogLight == null)
		{
			this.fogLight = base.GetComponent<FogLight>();
		}
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0000681B File Offset: 0x00004A1B
	private void OnEnable()
	{
		this.lightResource.onAmountChanged.AddListener(new UnityAction<int>(this.LightPowerChanged));
		this.LightPowerChanged(this.lightResource.Amount);
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x00030EE4 File Offset: 0x0002F0E4
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
