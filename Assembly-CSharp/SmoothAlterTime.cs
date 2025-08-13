using System;
using UnityEngine;

// Token: 0x02000137 RID: 311
public class SmoothAlterTime : MonoBehaviour
{
	// Token: 0x060005CD RID: 1485 RVA: 0x0000621E File Offset: 0x0000441E
	public void StartAlteredTime()
	{
		base.enabled = true;
		this.isTimeAltered = true;
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0000622E File Offset: 0x0000442E
	public void StopAlteredTime()
	{
		this.isTimeAltered = false;
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x0002ED70 File Offset: 0x0002CF70
	private void Update()
	{
		this.alteredTimeSmooth = Mathf.MoveTowards(this.alteredTimeSmooth, this.isTimeAltered ? 1f : 0f, Time.unscaledDeltaTime / this.fadeRealTime);
		Time.timeScale = Mathf.Lerp(1f, this.alteredTimeScale, this.alteredTimeSmooth);
		if (this.alteredTimeSmooth == 0f && !this.isTimeAltered)
		{
			base.enabled = false;
		}
	}

	// Token: 0x040007DB RID: 2011
	private float alteredTimeSmooth;

	// Token: 0x040007DC RID: 2012
	public float fadeRealTime = 0.5f;

	// Token: 0x040007DD RID: 2013
	public float alteredTimeScale = 1f;

	// Token: 0x040007DE RID: 2014
	public bool isTimeAltered;
}
