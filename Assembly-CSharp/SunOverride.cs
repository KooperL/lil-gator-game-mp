using System;
using UnityEngine;

public class SunOverride : MonoBehaviour
{
	// Token: 0x060006A6 RID: 1702 RVA: 0x00006D0A File Offset: 0x00004F0A
	private void Awake()
	{
		this.rotation = base.transform.rotation;
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x0003212C File Offset: 0x0003032C
	private void OnEnable()
	{
		if (Sun.s == null)
		{
			Sun.s = global::UnityEngine.Object.FindObjectOfType<Sun>();
		}
		if (Sun.s == null)
		{
			return;
		}
		Sun.s.enabled = true;
		int num = -1;
		for (int i = 0; i < Sun.overrides.Count; i++)
		{
			if (Sun.overrides[i].priority < this.priority)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			Sun.overrides.Add(this);
			return;
		}
		Sun.overrides.Insert(num, this);
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x00006D1D File Offset: 0x00004F1D
	private void OnDisable()
	{
		Sun.overrides.Remove(this);
	}

	public int priority;

	[Range(0f, 1f)]
	public float strength;

	[Header("Settings")]
	public Quaternion rotation;
}
