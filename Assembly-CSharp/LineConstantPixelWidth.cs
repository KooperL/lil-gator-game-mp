using System;
using UnityEngine;
using UnityEngine.U2D;

// Token: 0x020001C0 RID: 448
[RequireComponent(typeof(LineRenderer))]
public class LineConstantPixelWidth : MonoBehaviour
{
	// Token: 0x0600087F RID: 2175 RVA: 0x00008627 File Offset: 0x00006827
	private void Awake()
	{
		this.lineRenderer = base.GetComponent<LineRenderer>();
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x0003742C File Offset: 0x0003562C
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
			this.keys[i].weightedMode = 0;
		}
		this.widthCurve.keys = this.keys;
		this.lineRenderer.widthCurve = this.widthCurve;
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00037528 File Offset: 0x00035728
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

	// Token: 0x06000882 RID: 2178 RVA: 0x000375CC File Offset: 0x000357CC
	public float ScaleForWorldPosition(Vector3 position)
	{
		float num = (this.mainCamera.WorldToScreenPoint(position) - this.mainCamera.WorldToScreenPoint(position + Vector3.up / 1000f)).magnitude * 1000f;
		return this.pixelRatio / num;
	}

	// Token: 0x04000B12 RID: 2834
	private LineRenderer lineRenderer;

	// Token: 0x04000B13 RID: 2835
	public float pixelWidth = 1f;

	// Token: 0x04000B14 RID: 2836
	private Camera mainCamera;

	// Token: 0x04000B15 RID: 2837
	private Vector3 start;

	// Token: 0x04000B16 RID: 2838
	private Vector3 end;

	// Token: 0x04000B17 RID: 2839
	private AnimationCurve widthCurve;

	// Token: 0x04000B18 RID: 2840
	private Keyframe[] keys;

	// Token: 0x04000B19 RID: 2841
	private float pixelRatio;

	// Token: 0x04000B1A RID: 2842
	public int referenceHeight;

	// Token: 0x04000B1B RID: 2843
	private PixelPerfectCamera pixelCamera;
}
