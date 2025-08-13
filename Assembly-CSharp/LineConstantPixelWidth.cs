using System;
using UnityEngine;
using UnityEngine.U2D;

// Token: 0x02000157 RID: 343
[RequireComponent(typeof(LineRenderer))]
public class LineConstantPixelWidth : MonoBehaviour
{
	// Token: 0x06000735 RID: 1845 RVA: 0x000240C5 File Offset: 0x000222C5
	private void Awake()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x000240D4 File Offset: 0x000222D4
	private void Start()
	{
		this.mainCamera = Camera.main;
		this.pixelCamera = this.mainCamera.GetComponent<PixelPerfectCamera>();
		this.lineRenderer.widthMultiplier = 1f;
		this.lineRenderer.startWidth = 1f;
		this.lineRenderer.endWidth = 1f;
		this.widthCurve = this.lineRenderer.widthCurve;
		this.keys = new Keyframe[this.lineRenderer.positionCount];
		for (int i = 0; i < this.keys.Length; i++)
		{
			this.keys[i].time = (float)i / (float)(this.keys.Length - 1);
			this.keys[i].value = 1f;
			this.keys[i].weightedMode = WeightedMode.None;
		}
		this.widthCurve.keys = this.keys;
		this.lineRenderer.widthCurve = this.widthCurve;
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x000241D0 File Offset: 0x000223D0
	private void LateUpdate()
	{
		if (!this.lineRenderer.isVisible)
		{
			return;
		}
		this.pixelRatio = (float)this.pixelCamera.pixelRatio;
		for (int i = 0; i < this.lineRenderer.positionCount; i++)
		{
			Vector3 vector = this.lineRenderer.GetPosition(i);
			if (!this.lineRenderer.useWorldSpace)
			{
				vector = base.transform.TransformPoint(vector);
			}
			this.keys[i].value = this.ScaleForWorldPosition(vector);
		}
		this.widthCurve.keys = this.keys;
		this.lineRenderer.widthCurve = this.widthCurve;
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x00024274 File Offset: 0x00022474
	public float ScaleForWorldPosition(Vector3 position)
	{
		float num = (this.mainCamera.WorldToScreenPoint(position) - this.mainCamera.WorldToScreenPoint(position + Vector3.up / 1000f)).magnitude * 1000f;
		return this.pixelRatio / num;
	}

	// Token: 0x04000974 RID: 2420
	private LineRenderer lineRenderer;

	// Token: 0x04000975 RID: 2421
	public float pixelWidth = 1f;

	// Token: 0x04000976 RID: 2422
	private Camera mainCamera;

	// Token: 0x04000977 RID: 2423
	private Vector3 start;

	// Token: 0x04000978 RID: 2424
	private Vector3 end;

	// Token: 0x04000979 RID: 2425
	private AnimationCurve widthCurve;

	// Token: 0x0400097A RID: 2426
	private Keyframe[] keys;

	// Token: 0x0400097B RID: 2427
	private float pixelRatio;

	// Token: 0x0400097C RID: 2428
	public int referenceHeight;

	// Token: 0x0400097D RID: 2429
	private PixelPerfectCamera pixelCamera;
}
