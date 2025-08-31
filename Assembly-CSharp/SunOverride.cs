using System;
using UnityEngine;

public class SunOverride : MonoBehaviour
{
	// Token: 0x0600055A RID: 1370 RVA: 0x0001C6AC File Offset: 0x0001A8AC
	private void Awake()
	{
		this.rotation = base.transform.rotation;
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0001C6C0 File Offset: 0x0001A8C0
	private void OnEnable()
	{
		if (Sun.s == null)
		{
			Sun.s = Object.FindObjectOfType<Sun>();
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

	// Token: 0x0600055C RID: 1372 RVA: 0x0001C74C File Offset: 0x0001A94C
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
