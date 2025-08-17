using System;
using UnityEngine;

public class SpecialSettingsZone : MonoBehaviour
{
	// Token: 0x0600106F RID: 4207 RVA: 0x0000E18B File Offset: 0x0000C38B
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

	// Token: 0x06001070 RID: 4208 RVA: 0x0000E1B7 File Offset: 0x0000C3B7
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

	public static SpecialSettingsZone.SpecialSettings specialSettings;

	[Header("PC")]
	public bool enableSpecialPCSettings;

	public SpecialSettingsZone.SpecialSettings specialPCSettings;

	[Header("Switch")]
	public bool enableSpecialSwitchSettings;

	public SpecialSettingsZone.SpecialSettings specialSwitchSettings;

	[Serializable]
	public class SpecialSettings
	{
		public int shadowCascades = -1;

		public int shadowResolution = -1;

		public float shadowDistance = -1f;
	}
}
