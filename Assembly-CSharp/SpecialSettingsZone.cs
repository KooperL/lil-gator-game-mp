using System;
using UnityEngine;

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
