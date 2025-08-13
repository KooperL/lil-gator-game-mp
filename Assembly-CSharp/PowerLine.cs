using System;
using UnityEngine;
using UnityEngine.U2D;

// Token: 0x02000200 RID: 512
public class PowerLine : MonoBehaviour
{
	// Token: 0x06000B1B RID: 2843 RVA: 0x000376FC File Offset: 0x000358FC
	private void Awake()
	{
		if (this.lineRenderers != null && this.lineRenderers.Length != 0)
		{
			this.positions = new Vector3[this.positionCount];
			this.keys = new Keyframe[this.positionCount];
			this.widthCurve = new AnimationCurve(this.keys);
			this.widthCurves = new AnimationCurve[this.positionCount * 2];
			for (int i = 0; i < this.lineRenderers.Length; i++)
			{
				this.widthCurves[i] = this.lineRenderers[i].widthCurve;
				this.widthCurves[i + this.lineRenderers.Length] = new AnimationCurve(this.widthCurves[i].keys);
			}
		}
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x000377B1 File Offset: 0x000359B1
	private void Start()
	{
		this.mainCamera = Camera.main;
		this.pixelCamera = this.mainCamera.GetComponent<PixelPerfectCamera>();
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x000377D0 File Offset: 0x000359D0
	public void FastLateUpdate()
	{
		if (this.lineRenderers != null && this.lineRenderers.Length != 0)
		{
			this.time = Time.time;
			for (int i = 0; i < this.lineRenderers.Length; i++)
			{
				this.UpdateLineRenderer(this.lineRenderers[i], this.linkedPowerLine.anchorPoints[i].position, this.seeds[i], i);
			}
		}
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x00037838 File Offset: 0x00035A38
	private void UpdateLineRenderer(LineRenderer lineRenderer, Vector3 endPoint, float seed, int index)
	{
		if (!lineRenderer.isVisible)
		{
			return;
		}
		lineRenderer.GetPositions(this.positions);
		if (this.pixelSpaceWidth)
		{
			this.keys = this.widthCurves[index + (this.toggleWidthCurve ? this.lineRenderers.Length : 0)].keys;
		}
		Vector3 vector = this.windMovement * new Vector3(Mathf.PerlinNoise(seed, this.time * this.windSpeed) - 0.5f, Mathf.PerlinNoise(seed + 1000f, this.time * this.windSpeed) - 0.5f, Mathf.PerlinNoise(seed - 1000f, this.time * this.windSpeed) - 0.5f);
		for (int i = 1; i < this.positionCount - 1; i++)
		{
			float num = (float)i / (float)(this.positionCount - 1);
			this.positions[i] = this.GetPointPosition(lineRenderer.transform.position, endPoint, num, vector);
			if (this.pixelSpaceWidth)
			{
				this.keys[i].value = Mathf.Max(this.ScaleForWorldPosition(this.positions[i]), this.worldWidth);
			}
		}
		if (this.pixelSpaceWidth)
		{
			AnimationCurve animationCurve = this.widthCurves[index + (this.toggleWidthCurve ? this.lineRenderers.Length : 0)];
			animationCurve.keys = this.keys;
			lineRenderer.widthCurve = animationCurve;
			this.toggleWidthCurve = !this.toggleWidthCurve;
		}
		else
		{
			lineRenderer.startWidth = Mathf.Max(this.ScaleForWorldPosition(this.positions[0]), this.worldWidth);
			lineRenderer.endWidth = Mathf.Max(this.ScaleForWorldPosition(this.positions[this.positionCount - 1]), this.worldWidth);
		}
		lineRenderer.SetPositions(this.positions);
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x00037A0C File Offset: 0x00035C0C
	public float ScaleForWorldPosition(Vector3 position)
	{
		float num = (this.mainCamera.WorldToScreenPoint(position) - this.mainCamera.WorldToScreenPoint(position + this.mainCamera.transform.up / 1000f)).magnitude * 1000f;
		return (float)this.pixelCamera.pixelRatio / num;
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x00037A74 File Offset: 0x00035C74
	public Vector3 GetPointPosition(Vector3 start, Vector3 end, float t, Vector3 sway)
	{
		Vector3 vector = Vector3.Lerp(start, end, t);
		float num = 2f * Mathf.Abs(t - 0.5f);
		vector.y += this.sag * (1f - num * num);
		vector += (1f - num) * sway;
		return vector;
	}

	// Token: 0x04000ECD RID: 3789
	public Transform[] anchorPoints;

	// Token: 0x04000ECE RID: 3790
	public LineRenderer[] lineRenderers;

	// Token: 0x04000ECF RID: 3791
	private AnimationCurve[] widthCurves;

	// Token: 0x04000ED0 RID: 3792
	private bool toggleWidthCurve;

	// Token: 0x04000ED1 RID: 3793
	public PowerLine linkedPowerLine;

	// Token: 0x04000ED2 RID: 3794
	public int positionCount = 6;

	// Token: 0x04000ED3 RID: 3795
	public Material lineMaterial;

	// Token: 0x04000ED4 RID: 3796
	public float sag = -4f;

	// Token: 0x04000ED5 RID: 3797
	public float catA = 1f;

	// Token: 0x04000ED6 RID: 3798
	public float windMovement = 1f;

	// Token: 0x04000ED7 RID: 3799
	public float windSpeed = 1f;

	// Token: 0x04000ED8 RID: 3800
	public float[] seeds;

	// Token: 0x04000ED9 RID: 3801
	public bool pixelSpaceWidth = true;

	// Token: 0x04000EDA RID: 3802
	public float pixelWidth = 1f;

	// Token: 0x04000EDB RID: 3803
	public float worldWidth = 0.05f;

	// Token: 0x04000EDC RID: 3804
	private Camera mainCamera;

	// Token: 0x04000EDD RID: 3805
	private PixelPerfectCamera pixelCamera;

	// Token: 0x04000EDE RID: 3806
	private Vector3[] positions;

	// Token: 0x04000EDF RID: 3807
	private float time;

	// Token: 0x04000EE0 RID: 3808
	private Keyframe[] keys;

	// Token: 0x04000EE1 RID: 3809
	private AnimationCurve widthCurve;
}
