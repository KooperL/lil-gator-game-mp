using System;
using UnityEngine;

public class TimeAction : MonoBehaviour
{
	// Token: 0x06000FA8 RID: 4008 RVA: 0x0000D8AE File Offset: 0x0000BAAE
	public void StartTimer()
	{
		this.startTime = Time.unscaledTimeAsDouble;
		this.startFrame = Time.frameCount;
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x00052274 File Offset: 0x00050474
	public void StopTimer()
	{
		Debug.Log("Time: " + (Time.unscaledTimeAsDouble - this.startTime).ToString("0.0000") + "   Frames: " + (Time.frameCount - this.startFrame).ToString());
	}

	private double startTime;

	private int startFrame;
}
