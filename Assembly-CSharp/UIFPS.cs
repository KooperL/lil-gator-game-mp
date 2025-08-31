using System;
using UnityEngine;
using UnityEngine.UI;

public class UIFPS : MonoBehaviour
{
	// Token: 0x06000ED0 RID: 3792 RVA: 0x00046C99 File Offset: 0x00044E99
	private void Start()
	{
		this.frameConsistency = new bool[10];
		this.frameTimings = new FrameTiming[20];
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x00046CB5 File Offset: 0x00044EB5
	private void OnEnable()
	{
		this.display.text = "";
		this.updateDisplayCounter = 0;
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x00046CCE File Offset: 0x00044ECE
	private void Update()
	{
		FrameTimingManager.CaptureFrameTimings();
		this.updateDisplayCounter++;
		if (this.updateDisplayCounter >= 10)
		{
			this.UpdateDisplay();
			this.updateDisplayCounter = 0;
		}
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x00046CFC File Offset: 0x00044EFC
	private void UpdateDisplay()
	{
		uint latestTimings = FrameTimingManager.GetLatestTimings(20U, this.frameTimings);
		if (latestTimings == 0U)
		{
			this.display.text = "No timing stats found";
			return;
		}
		this.fps = 0f;
		double num = 0.0;
		double num2 = 0.0;
		int num3 = 0;
		while ((long)num3 < (long)((ulong)latestTimings))
		{
			num += this.frameTimings[num3].cpuFrameTime;
			num2 += this.frameTimings[num3].gpuFrameTime;
			num3++;
		}
		num /= latestTimings;
		num2 /= latestTimings;
		this.fps = 1000f / Mathf.Max((float)num, (float)num2);
		double num4;
		double num5;
		if (UIFPS.isCumulative)
		{
			this.cumulativeCount++;
			this.cCpuTime += num;
			this.cGpuTime += num2;
			num4 = this.cCpuTime / (double)this.cumulativeCount;
			num5 = this.cGpuTime / (double)this.cumulativeCount;
		}
		else
		{
			this.cumulativeCount = 0;
			this.cCpuTime = (this.cGpuTime = 0.0);
			num4 = num;
			num5 = num2;
		}
		this.display.color = (UIFPS.isCumulative ? Color.red : Color.black);
		if (MainCamera.c.allowDynamicResolution)
		{
			float heightScaleFactor = ScalableBufferManager.heightScaleFactor;
			int num6 = (int)Mathf.Ceil((float)Screen.currentResolution.height * heightScaleFactor);
			this.display.text = string.Format("FPS: {0:0.0}\nCPU: {1:0.0}ms\nGPU: {2:0.0}ms\nScale: {3:0.00}x\n({4}p)", new object[] { this.fps, num4, num5, heightScaleFactor, num6 });
			return;
		}
		this.display.text = string.Format("FPS: {0:0.0}\nCPU: {1:0.0}ms\nGPU: {2:0.0}ms", this.fps, num4, num5);
	}

	public static bool isCumulative;

	public Text display;

	private float fps;

	private float smoothFPS;

	private bool[] frameConsistency;

	private int frameIndex;

	private float consistencyCounter;

	private int updateDisplayCounter;

	private const int updateInterval = 10;

	private FrameTiming[] frameTimings;

	private const int frameTimingCount = 20;

	private double cCpuTime;

	private double cGpuTime;

	private int cumulativeCount;
}
