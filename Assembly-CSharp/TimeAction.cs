using System;
using UnityEngine;

public class TimeAction : MonoBehaviour
{
	// Token: 0x06000FA9 RID: 4009 RVA: 0x0000D8AE File Offset: 0x0000BAAE
	public void StartTimer()
	{
		this.startTime = Time.unscaledTimeAsDouble;
		this.startFrame = Time.frameCount;
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x00052560 File Offset: 0x00050760
	public void StopTimer()
	{
		Debug.Log("Time: " + (Time.unscaledTimeAsDouble - this.startTime).ToString("0.0000") + "   Frames: " + (Time.frameCount - this.startFrame).ToString());
	}

	private double startTime;

	private int startFrame;
}
