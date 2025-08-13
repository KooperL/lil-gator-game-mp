using System;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class PersistentLight : PersistentObject
{
	// Token: 0x06000502 RID: 1282 RVA: 0x0001AE14 File Offset: 0x00019014
	public override void Load(bool state)
	{
		this.SetState(state);
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x0001AE1D File Offset: 0x0001901D
	public void Activate()
	{
		this.SetState(true);
		this.SaveTrue();
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0001AE2C File Offset: 0x0001902C
	private void SetState(bool state)
	{
		if (state)
		{
			foreach (PersistentLight.LightSetting lightSetting in this.lightSettings)
			{
				lightSetting.light.SetStrength(lightSetting.strength, true);
			}
		}
		GameObject[] array2 = this.activateObjects;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].SetActive(state);
		}
	}

	// Token: 0x040006E3 RID: 1763
	public PersistentLight.LightSetting[] lightSettings;

	// Token: 0x040006E4 RID: 1764
	public GameObject[] activateObjects;

	// Token: 0x020003AA RID: 938
	[Serializable]
	public struct LightSetting
	{
		// Token: 0x04001B5F RID: 7007
		public FogLight light;

		// Token: 0x04001B60 RID: 7008
		[Range(0f, 1f)]
		public float strength;
	}
}
