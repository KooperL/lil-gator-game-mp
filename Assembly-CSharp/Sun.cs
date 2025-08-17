using System;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
	// Token: 0x060006A2 RID: 1698 RVA: 0x00006CE5 File Offset: 0x00004EE5
	private void Awake()
	{
		Sun.s = this;
		this.rotation = base.transform.rotation;
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0003221C File Offset: 0x0003041C
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
