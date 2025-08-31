using System;
using System.Collections.Generic;
using UnityEngine;

public class GliderRuffle : MonoBehaviour
{
	// Token: 0x06000636 RID: 1590 RVA: 0x00020310 File Offset: 0x0001E510
	private void Start()
	{
		List<Transform> list = new List<Transform>(base.transform.GetComponentsInChildren<Transform>());
		this.children = list.ToArray();
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0002033A File Offset: 0x0001E53A
	private void OnEnable()
	{
		this.fadeIn = -1f;
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x00020348 File Offset: 0x0001E548
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
