using System;
using UnityEngine;

// Token: 0x020001E9 RID: 489
public class DynamicResolutionScaler : MonoBehaviour
{
	// Token: 0x06000906 RID: 2310 RVA: 0x00008CF6 File Offset: 0x00006EF6
	private void Start()
	{
		this.frameTimings = new FrameTiming[10];
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00008D05 File Offset: 0x00006F05
	private void OnEnable()
	{
		this.updateCounter = 0;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00008D0E File Offset: 0x00006F0E
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

	// Token: 0x06000909 RID: 2313 RVA: 0x00038870 File Offset: 0x00036A70
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

	// Token: 0x0600090A RID: 2314 RVA: 0x000389A4 File Offset: 0x00036BA4
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

	// Token: 0x04000B9F RID: 2975
	private const float target = 60f;

	// Token: 0x04000BA0 RID: 2976
	private int updateCounter;

	// Token: 0x04000BA1 RID: 2977
	private const int updateInterval = 10;

	// Token: 0x04000BA2 RID: 2978
	private FrameTiming[] frameTimings;

	// Token: 0x04000BA3 RID: 2979
	private const int frameTimingCount = 10;

	// Token: 0x04000BA4 RID: 2980
	[Range(0f, 1f)]
	public float scale = 1f;

	// Token: 0x04000BA5 RID: 2981
	private float scaleVel;

	// Token: 0x04000BA6 RID: 2982
	public float scalingSpeed = 0.1f;

	// Token: 0x04000BA7 RID: 2983
	private const float minScale = 0.65f;

	// Token: 0x04000BA8 RID: 2984
	private const float maxScale = 1f;

	// Token: 0x04000BA9 RID: 2985
	private const int scaleIntervalCount = 10;

	// Token: 0x04000BAA RID: 2986
	private Vector2Int currentResolution = Vector2Int.zero;

	// Token: 0x04000BAB RID: 2987
	private float currentPixelCount;

	// Token: 0x04000BAC RID: 2988
	public DynamicResolutionScaler.ScaleInterval[] intervals;

	// Token: 0x020001EA RID: 490
	[Serializable]
	public struct ScaleInterval
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x00008D63 File Offset: 0x00006F63
		public ScaleInterval(float scale, float unscaledPixelCount)
		{
			this.scale = scale;
			this.pixelCount = scale * scale * unscaledPixelCount;
			this.perPixelBudget = 1.0 / ((double)this.pixelCount * 60.0);
		}

		// Token: 0x04000BAD RID: 2989
		public float scale;

		// Token: 0x04000BAE RID: 2990
		public float pixelCount;

		// Token: 0x04000BAF RID: 2991
		public double perPixelBudget;
	}
}
