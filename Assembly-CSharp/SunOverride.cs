using System;
using UnityEngine;

// Token: 0x0200015A RID: 346
public class SunOverride : MonoBehaviour
{
	// Token: 0x0600066C RID: 1644 RVA: 0x00006A44 File Offset: 0x00004C44
	private void Awake()
	{
		this.rotation = base.transform.rotation;
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x00030BAC File Offset: 0x0002EDAC
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

	// Token: 0x0600066E RID: 1646 RVA: 0x00006A57 File Offset: 0x00004C57
	private void OnDisable()
	{
		Sun.overrides.Remove(this);
	}

	// Token: 0x040008A4 RID: 2212
	public int priority;

	// Token: 0x040008A5 RID: 2213
	[Range(0f, 1f)]
	public float strength;

	// Token: 0x040008A6 RID: 2214
	[Header("Settings")]
	public Quaternion rotation;
}
