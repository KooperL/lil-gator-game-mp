using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000394 RID: 916
public class UIBlink : MonoBehaviour
{
	// Token: 0x06001167 RID: 4455 RVA: 0x0000EE53 File Offset: 0x0000D053
	private void OnEnable()
	{
		this.image.enabled = true;
		this.nextBlinkTime = Time.time + this.blinkInterval;
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x0000EE73 File Offset: 0x0000D073
	private void Update()
	{
		if (Time.time > this.nextBlinkTime)
		{
			this.image.enabled = !this.image.enabled;
			this.nextBlinkTime += this.blinkInterval;
		}
	}

	// Token: 0x0400166E RID: 5742
	public Image image;

	// Token: 0x0400166F RID: 5743
	public float blinkInterval = 0.5f;

	// Token: 0x04001670 RID: 5744
	private float nextBlinkTime;
}
