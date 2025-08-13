using System;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public class SmoothAlterTime : MonoBehaviour
{
	// Token: 0x060004C1 RID: 1217 RVA: 0x0001A02C File Offset: 0x0001822C
	public void StartAlteredTime()
	{
		base.enabled = true;
		this.isTimeAltered = true;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0001A03C File Offset: 0x0001823C
	public void StopAlteredTime()
	{
		this.isTimeAltered = false;
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x0001A048 File Offset: 0x00018248
	private void Update()
	{
		this.alteredTimeSmooth = Mathf.MoveTowards(this.alteredTimeSmooth, this.isTimeAltered ? 1f : 0f, Time.unscaledDeltaTime / this.fadeRealTime);
		Time.timeScale = Mathf.Lerp(1f, this.alteredTimeScale, this.alteredTimeSmooth);
		if (this.alteredTimeSmooth == 0f && !this.isTimeAltered)
		{
			base.enabled = false;
		}
	}

	// Token: 0x04000699 RID: 1689
	private float alteredTimeSmooth;

	// Token: 0x0400069A RID: 1690
	public float fadeRealTime = 0.5f;

	// Token: 0x0400069B RID: 1691
	public float alteredTimeScale = 1f;

	// Token: 0x0400069C RID: 1692
	public bool isTimeAltered;
}
