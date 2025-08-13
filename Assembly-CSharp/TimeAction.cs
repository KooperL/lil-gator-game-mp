using System;
using UnityEngine;

// Token: 0x02000307 RID: 775
public class TimeAction : MonoBehaviour
{
	// Token: 0x06000F4C RID: 3916 RVA: 0x0000D4FC File Offset: 0x0000B6FC
	public void StartTimer()
	{
		this.startTime = Time.unscaledTimeAsDouble;
		this.startFrame = Time.frameCount;
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x00050374 File Offset: 0x0004E574
	public void StopTimer()
	{
		Debug.Log("Time: " + (Time.unscaledTimeAsDouble - this.startTime).ToString("0.0000") + "   Frames: " + (Time.frameCount - this.startFrame).ToString());
	}

	// Token: 0x040013CB RID: 5067
	private double startTime;

	// Token: 0x040013CC RID: 5068
	private int startFrame;
}
