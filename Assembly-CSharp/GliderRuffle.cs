using System;
using System.Collections.Generic;
using UnityEngine;

public class GliderRuffle : MonoBehaviour
{
	// Token: 0x0600079C RID: 1948 RVA: 0x00035994 File Offset: 0x00033B94
	private void Start()
	{
		List<Transform> list = new List<Transform>(base.transform.GetComponentsInChildren<Transform>());
		this.children = list.ToArray();
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x00007964 File Offset: 0x00005B64
	private void OnEnable()
	{
		this.fadeIn = -1f;
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x000359C0 File Offset: 0x00033BC0
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

	private Transform[] children;

	public float frequency = 5f;

	public float magnitude = 20f;

	private const float startFadeIn = 0.25f;

	private float fadeIn;

	private float lag = 0.5f;
}
