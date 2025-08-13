using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000143 RID: 323
public class LightPower : MonoBehaviour
{
	// Token: 0x06000610 RID: 1552 RVA: 0x00006539 File Offset: 0x00004739
	private void OnValidate()
	{
		if (this.fogLight == null)
		{
			this.fogLight = base.GetComponent<FogLight>();
		}
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x00006555 File Offset: 0x00004755
	private void OnEnable()
	{
		this.lightResource.onAmountChanged.AddListener(new UnityAction<int>(this.LightPowerChanged));
		this.LightPowerChanged(this.lightResource.Amount);
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x0002F7E8 File Offset: 0x0002D9E8
	private void LightPowerChanged(int lightPower)
	{
		float num = Mathf.Lerp(this.unpoweredStrength, 1f, (float)lightPower / (float)this.maxLightPower);
		this.fogLight.SetStrength(num, false);
	}

	// Token: 0x04000824 RID: 2084
	public ItemResource lightResource;

	// Token: 0x04000825 RID: 2085
	private FogLight fogLight;

	// Token: 0x04000826 RID: 2086
	public float unpoweredStrength = 0.25f;

	// Token: 0x04000827 RID: 2087
	public int maxLightPower = 20;
}
