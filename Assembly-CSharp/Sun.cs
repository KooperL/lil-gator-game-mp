using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000159 RID: 345
public class Sun : MonoBehaviour
{
	// Token: 0x06000668 RID: 1640 RVA: 0x00006A1F File Offset: 0x00004C1F
	private void Awake()
	{
		Sun.s = this;
		this.rotation = base.transform.rotation;
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x00030B20 File Offset: 0x0002ED20
	private void Update()
	{
		Quaternion quaternion = this.rotation;
		foreach (SunOverride sunOverride in Sun.overrides)
		{
			quaternion = Quaternion.Slerp(quaternion, sunOverride.transform.rotation, sunOverride.strength);
		}
		base.transform.rotation = quaternion;
		if (Sun.overrides.Count == 0)
		{
			base.enabled = false;
		}
	}

	// Token: 0x040008A1 RID: 2209
	public static List<SunOverride> overrides = new List<SunOverride>();

	// Token: 0x040008A2 RID: 2210
	public static Sun s;

	// Token: 0x040008A3 RID: 2211
	private Quaternion rotation;
}
