using System;
using UnityEngine;

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

	public PersistentLight.LightSetting[] lightSettings;

	public GameObject[] activateObjects;

	[Serializable]
	public struct LightSetting
	{
		public FogLight light;

		[Range(0f, 1f)]
		public float strength;
	}
}
