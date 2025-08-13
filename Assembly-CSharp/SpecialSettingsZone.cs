using System;
using UnityEngine;

// Token: 0x02000270 RID: 624
public class SpecialSettingsZone : MonoBehaviour
{
	// Token: 0x06000D60 RID: 3424 RVA: 0x00040950 File Offset: 0x0003EB50
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

	// Token: 0x06000D61 RID: 3425 RVA: 0x0004097C File Offset: 0x0003EB7C
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

	// Token: 0x040011A6 RID: 4518
	public static SpecialSettingsZone.SpecialSettings specialSettings;

	// Token: 0x040011A7 RID: 4519
	[Header("PC")]
	public bool enableSpecialPCSettings;

	// Token: 0x040011A8 RID: 4520
	public SpecialSettingsZone.SpecialSettings specialPCSettings;

	// Token: 0x040011A9 RID: 4521
	[Header("Switch")]
	public bool enableSpecialSwitchSettings;

	// Token: 0x040011AA RID: 4522
	public SpecialSettingsZone.SpecialSettings specialSwitchSettings;

	// Token: 0x02000427 RID: 1063
	[Serializable]
	public class SpecialSettings
	{
		// Token: 0x04001D5D RID: 7517
		public int shadowCascades = -1;

		// Token: 0x04001D5E RID: 7518
		public int shadowResolution = -1;

		// Token: 0x04001D5F RID: 7519
		public float shadowDistance = -1f;
	}
}
