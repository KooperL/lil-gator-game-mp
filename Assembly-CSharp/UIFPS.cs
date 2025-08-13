using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A3 RID: 931
public class UIFPS : MonoBehaviour
{
	// Token: 0x060011A8 RID: 4520 RVA: 0x0000F148 File Offset: 0x0000D348
	private void Start()
	{
		this.frameConsistency = new bool[10];
		this.frameTimings = new FrameTiming[20];
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x0000F164 File Offset: 0x0000D364
	private void OnEnable()
	{
		this.display.text = "";
		this.updateDisplayCounter = 0;
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x0000F17D File Offset: 0x0000D37D
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

	// Token: 0x060011AB RID: 4523 RVA: 0x00058154 File Offset: 0x00056354
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

	// Token: 0x040016C4 RID: 5828
	public static bool isCumulative;

	// Token: 0x040016C5 RID: 5829
	public Text display;

	// Token: 0x040016C6 RID: 5830
	private float fps;

	// Token: 0x040016C7 RID: 5831
	private float smoothFPS;

	// Token: 0x040016C8 RID: 5832
	private bool[] frameConsistency;

	// Token: 0x040016C9 RID: 5833
	private int frameIndex;

	// Token: 0x040016CA RID: 5834
	private float consistencyCounter;

	// Token: 0x040016CB RID: 5835
	private int updateDisplayCounter;

	// Token: 0x040016CC RID: 5836
	private const int updateInterval = 10;

	// Token: 0x040016CD RID: 5837
	private FrameTiming[] frameTimings;

	// Token: 0x040016CE RID: 5838
	private const int frameTimingCount = 20;

	// Token: 0x040016CF RID: 5839
	private double cCpuTime;

	// Token: 0x040016D0 RID: 5840
	private double cGpuTime;

	// Token: 0x040016D1 RID: 5841
	private int cumulativeCount;
}
