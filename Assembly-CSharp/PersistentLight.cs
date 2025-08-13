using System;
using UnityEngine;

// Token: 0x02000144 RID: 324
public class PersistentLight : PersistentObject
{
	// Token: 0x06000614 RID: 1556 RVA: 0x0000659F File Offset: 0x0000479F
	public override void Load(bool state)
	{
		this.SetState(state);
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x000065A8 File Offset: 0x000047A8
	public void Activate()
	{
		this.SetState(true);
		this.SaveTrue();
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0002F820 File Offset: 0x0002DA20
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

	// Token: 0x04000828 RID: 2088
	public PersistentLight.LightSetting[] lightSettings;

	// Token: 0x04000829 RID: 2089
	public GameObject[] activateObjects;

	// Token: 0x02000145 RID: 325
	[Serializable]
	public struct LightSetting
	{
		// Token: 0x0400082A RID: 2090
		public FogLight light;

		// Token: 0x0400082B RID: 2091
		[Range(0f, 1f)]
		public float strength;
	}
}
