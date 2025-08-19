using System;
using UnityEngine;

public class SmoothAlterTime : MonoBehaviour
{
	// Token: 0x06000607 RID: 1543 RVA: 0x000064E4 File Offset: 0x000046E4
	public void StartAlteredTime()
	{
		base.enabled = true;
		this.isTimeAltered = true;
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x000064F4 File Offset: 0x000046F4
	public void StopAlteredTime()
	{
		this.isTimeAltered = false;
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x00030448 File Offset: 0x0002E648
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
