using System;
using UnityEngine;

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

	private float alteredTimeSmooth;

	public float fadeRealTime = 0.5f;

	public float alteredTimeScale = 1f;

	public bool isTimeAltered;
}
