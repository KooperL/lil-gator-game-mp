using System;
using System.Collections.Generic;
using UnityEngine;

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

	public static List<SunOverride> overrides = new List<SunOverride>();

	public static Sun s;

	private Quaternion rotation;
}
