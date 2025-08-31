using System;
using UnityEngine;

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

	private const float target = 60f;

	private int updateCounter;

	private const int updateInterval = 10;

	private FrameTiming[] frameTimings;

	private const int frameTimingCount = 10;

	[Range(0f, 1f)]
	public float scale = 1f;

	private float scaleVel;

	public float scalingSpeed = 0.1f;

	private const float minScale = 0.65f;

	private const float maxScale = 1f;

	private const int scaleIntervalCount = 10;

	private Vector2Int currentResolution = Vector2Int.zero;

	private float currentPixelCount;

	public DynamicResolutionScaler.ScaleInterval[] intervals;

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

		public float scale;

		public float pixelCount;

		public double perPixelBudget;
	}
}
