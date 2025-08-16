using System;
using UnityEngine;

public class PersistentLight : PersistentObject
{
	// Token: 0x0600064E RID: 1614 RVA: 0x00006865 File Offset: 0x00004A65
	public override void Load(bool state)
	{
		this.SetState(state);
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0000686E File Offset: 0x00004A6E
	public void Activate()
	{
		this.SetState(true);
		this.SaveTrue();
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x00030DA0 File Offset: 0x0002EFA0
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
