using System;
using UnityEngine;

public class DynamicResolutionScaler : MonoBehaviour
{
	// Token: 0x06000947 RID: 2375 RVA: 0x00009027 File Offset: 0x00007227
	private void Start()
	{
		this.frameTimings = new FrameTiming[10];
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x00009036 File Offset: 0x00007236
	private void OnEnable()
	{
		this.updateCounter = 0;
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x0000903F File Offset: 0x0000723F
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

	// Token: 0x0600094A RID: 2378 RVA: 0x0003A1E0 File Offset: 0x000383E0
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

	// Token: 0x0600094B RID: 2379 RVA: 0x0003A314 File Offset: 0x00038514
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
		// Token: 0x0600094D RID: 2381 RVA: 0x00009094 File Offset: 0x00007294
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
