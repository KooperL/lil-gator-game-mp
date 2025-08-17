using System;
using UnityEngine;
using UnityEngine.UI;

public class UIBlink : MonoBehaviour
{
	// Token: 0x060011C7 RID: 4551 RVA: 0x0000F23C File Offset: 0x0000D43C
	private void OnEnable()
	{
		this.image.enabled = true;
		this.nextBlinkTime = Time.time + this.blinkInterval;
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x0000F25C File Offset: 0x0000D45C
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
