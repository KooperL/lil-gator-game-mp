using System;
using UnityEngine;
using UnityEngine.UI;

public class UIBlink : MonoBehaviour
{
	// Token: 0x06000E95 RID: 3733 RVA: 0x00045BA1 File Offset: 0x00043DA1
	private void OnEnable()
	{
		this.image.enabled = true;
		this.nextBlinkTime = Time.time + this.blinkInterval;
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x00045BC1 File Offset: 0x00043DC1
	private void Update()
	{
		if (Time.time > this.nextBlinkTime)
		{
			this.image.enabled = !this.image.enabled;
			this.nextBlinkTime += this.blinkInterval;
		}
	}

	public Image image;

	public float blinkInterval = 0.5f;

	private float nextBlinkTime;
}
