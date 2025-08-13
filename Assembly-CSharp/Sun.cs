using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000105 RID: 261
public class Sun : MonoBehaviour
{
	// Token: 0x06000556 RID: 1366 RVA: 0x0001C5F3 File Offset: 0x0001A7F3
	private void Awake()
	{
		Sun.s = this;
		this.rotation = base.transform.rotation;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0001C60C File Offset: 0x0001A80C
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

	// Token: 0x04000756 RID: 1878
	public static List<SunOverride> overrides = new List<SunOverride>();

	// Token: 0x04000757 RID: 1879
	public static Sun s;

	// Token: 0x04000758 RID: 1880
	private Quaternion rotation;
}
