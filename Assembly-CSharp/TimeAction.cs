using System;
using UnityEngine;

public class TimeAction : MonoBehaviour
{
	// Token: 0x06000CA0 RID: 3232 RVA: 0x0003D46F File Offset: 0x0003B66F
	public void StartTimer()
	{
		this.startTime = Time.unscaledTimeAsDouble;
		this.startFrame = Time.frameCount;
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x0003D488 File Offset: 0x0003B688
	public void StopTimer()
	{
		Debug.Log("Time: " + (Time.unscaledTimeAsDouble - this.startTime).ToString("0.0000") + "   Frames: " + (Time.frameCount - this.startFrame).ToString());
	}

	private double startTime;

	private int startFrame;
}
