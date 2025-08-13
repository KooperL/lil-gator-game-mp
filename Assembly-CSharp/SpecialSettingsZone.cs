using System;
using UnityEngine;

// Token: 0x02000339 RID: 825
public class SpecialSettingsZone : MonoBehaviour
{
	// Token: 0x06001014 RID: 4116 RVA: 0x0000DE22 File Offset: 0x0000C022
	public void OnEnable()
	{
		if (this.enableSpecialPCSettings)
		{
			SpecialSettingsZone.specialSettings = this.specialPCSettings;
			if (Settings.s != null)
			{
				Settings.s.LoadSettings();
			}
		}
	}

	// Token: 0x06001015 RID: 4117 RVA: 0x0000DE4E File Offset: 0x0000C04E
	public void OnDisable()
	{
		if (this.enableSpecialPCSettings)
		{
			SpecialSettingsZone.specialSettings = null;
			if (Settings.s != null)
			{
				Settings s = Settings.s;
				if (s == null)
				{
					return;
				}
				s.LoadSettings();
			}
		}
	}

	// Token: 0x040014CF RID: 5327
	public static SpecialSettingsZone.SpecialSettings specialSettings;

	// Token: 0x040014D0 RID: 5328
	[Header("PC")]
	public bool enableSpecialPCSettings;

	// Token: 0x040014D1 RID: 5329
	public SpecialSettingsZone.SpecialSettings specialPCSettings;

	// Token: 0x040014D2 RID: 5330
	[Header("Switch")]
	public bool enableSpecialSwitchSettings;

	// Token: 0x040014D3 RID: 5331
	public SpecialSettingsZone.SpecialSettings specialSwitchSettings;

	// Token: 0x0200033A RID: 826
	[Serializable]
	public class SpecialSettings
	{
		// Token: 0x040014D4 RID: 5332
		public int shadowCascades = -1;

		// Token: 0x040014D5 RID: 5333
		public int shadowResolution = -1;

		// Token: 0x040014D6 RID: 5334
		public float shadowDistance = -1f;
	}
}
