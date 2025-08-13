using System;
using UnityEngine;

// Token: 0x02000175 RID: 373
public class DynamicResolutionScaler : MonoBehaviour
{
	// Token: 0x060007A8 RID: 1960 RVA: 0x000256D4 File Offset: 0x000238D4
	private void Start()
	{
		this.frameTimings = new FrameTiming[10];
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x000256E3 File Offset: 0x000238E3
	private void OnEnable()
	{
		this.updateCounter = 0;
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x000256EC File Offset: 0x000238EC
	private void Update()
	{
		FrameTimingManager.CaptureFrameTimings();
		this.updateCounter++;
		if (this.updateCounter >= 10)
		{
			this.CheckPerformance();
			this.updateCounter = 0;
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00025718 File Offset: 0x00023918
	private void CheckPerformance()
	{
		uint latestTimings = FrameTimingManager.GetLatestTimings(10U, this.frameTimings);
		if (latestTimings == 0U)
		{
			return;
		}
		double num = 0.0;
		int num2 = 0;
		while ((long)num2 < (long)((ulong)latestTimings))
		{
			num = Math.Max(num, this.frameTimings[num2].gpuFrameTime);
			num2++;
		}
		if (Screen.currentResolution.height != this.currentResolution.y || this.intervals == null)
		{
			this.CalculateIntervals();
		}
		if (ItemCamera.isActive)
		{
			this.scale = 1f;
			this.currentPixelCount = this.intervals[0].pixelCount;
			ScalableBufferManager.ResizeBuffers(this.scale, this.scale);
			return;
		}
		num /= 1000.0;
		double num3 = num / (double)this.currentPixelCount;
		for (int i = 0; i < 10; i++)
		{
			if (num3 < this.intervals[i].perPixelBudget)
			{
				this.scale = this.intervals[i].scale;
				this.currentPixelCount = this.intervals[i].pixelCount;
				ScalableBufferManager.ResizeBuffers(this.scale, this.scale);
				return;
			}
		}
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x0002584C File Offset: 0x00023A4C
	[ContextMenu("Calculate Intervals")]
	private void CalculateIntervals()
	{
		if (this.intervals == null || this.intervals.Length != 10)
		{
			this.intervals = new DynamicResolutionScaler.ScaleInterval[10];
		}
		this.currentResolution = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);
		this.currentPixelCount = (float)(this.currentResolution.x * this.currentResolution.y);
		for (int i = 0; i < 10; i++)
		{
			float num = Mathf.Lerp(1f, 0.65f, (float)i / 9f);
			this.intervals[i] = new DynamicResolutionScaler.ScaleInterval(num, this.currentPixelCount);
		}
	}

	// Token: 0x040009D1 RID: 2513
	private const float target = 60f;

	// Token: 0x040009D2 RID: 2514
	private int updateCounter;

	// Token: 0x040009D3 RID: 2515
	private const int updateInterval = 10;

	// Token: 0x040009D4 RID: 2516
	private FrameTiming[] frameTimings;

	// Token: 0x040009D5 RID: 2517
	private const int frameTimingCount = 10;

	// Token: 0x040009D6 RID: 2518
	[Range(0f, 1f)]
	public float scale = 1f;

	// Token: 0x040009D7 RID: 2519
	private float scaleVel;

	// Token: 0x040009D8 RID: 2520
	public float scalingSpeed = 0.1f;

	// Token: 0x040009D9 RID: 2521
	private const float minScale = 0.65f;

	// Token: 0x040009DA RID: 2522
	private const float maxScale = 1f;

	// Token: 0x040009DB RID: 2523
	private const int scaleIntervalCount = 10;

	// Token: 0x040009DC RID: 2524
	private Vector2Int currentResolution = Vector2Int.zero;

	// Token: 0x040009DD RID: 2525
	private float currentPixelCount;

	// Token: 0x040009DE RID: 2526
	public DynamicResolutionScaler.ScaleInterval[] intervals;

	// Token: 0x020003CB RID: 971
	[Serializable]
	public struct ScaleInterval
	{
		// Token: 0x06001992 RID: 6546 RVA: 0x0006D5FE File Offset: 0x0006B7FE
		public ScaleInterval(float scale, float unscaledPixelCount)
		{
			this.scale = scale;
			this.pixelCount = scale * scale * unscaledPixelCount;
			this.perPixelBudget = 1.0 / ((double)this.pixelCount * 60.0);
		}

		// Token: 0x04001BEB RID: 7147
		public float scale;

		// Token: 0x04001BEC RID: 7148
		public float pixelCount;

		// Token: 0x04001BED RID: 7149
		public double perPixelBudget;
	}
}
