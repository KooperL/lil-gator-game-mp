using System;
using UnityEngine;
using UnityEngine.UI;

public class UIBlink : MonoBehaviour
{
	// Token: 0x060011C7 RID: 4551 RVA: 0x0000F227 File Offset: 0x0000D427
	private void OnEnable()
	{
		this.image.enabled = true;
		this.nextBlinkTime = Time.time + this.blinkInterval;
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x0000F247 File Offset: 0x0000D447
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
