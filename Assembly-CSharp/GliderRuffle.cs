using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000187 RID: 391
public class GliderRuffle : MonoBehaviour
{
	// Token: 0x0600075B RID: 1883 RVA: 0x00033D64 File Offset: 0x00031F64
	private void Start()
	{
		List<Transform> list = new List<Transform>(base.transform.GetComponentsInChildren<Transform>());
		this.children = list.ToArray();
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00007655 File Offset: 0x00005855
	private void OnEnable()
	{
		this.fadeIn = -1f;
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00033D90 File Offset: 0x00031F90
	private void LateUpdate()
	{
		if (this.fadeIn < 1f)
		{
			this.fadeIn = Mathf.MoveTowards(this.fadeIn, 1f, Time.deltaTime / 0.25f);
		}
		if (this.fadeIn < 0f)
		{
			return;
		}
		float num = this.fadeIn * this.magnitude;
		float num2 = Time.time * this.frequency;
		float num3 = -0.25f * this.frequency;
		for (int i = 0; i < this.children.Length; i++)
		{
			Vector3 zero = Vector3.zero;
			for (int j = 0; j < 3; j++)
			{
				zero[j] = num * (Mathf.PerlinNoise(500f * (float)j, (float)i * num3 + num2) - 0.5f);
			}
			this.children[i].localRotation = Quaternion.Euler(zero) * this.children[i].localRotation;
		}
	}

	// Token: 0x040009C9 RID: 2505
	private Transform[] children;

	// Token: 0x040009CA RID: 2506
	public float frequency = 5f;

	// Token: 0x040009CB RID: 2507
	public float magnitude = 20f;

	// Token: 0x040009CC RID: 2508
	private const float startFadeIn = 0.25f;

	// Token: 0x040009CD RID: 2509
	private float fadeIn;

	// Token: 0x040009CE RID: 2510
	private float lag = 0.5f;
}
