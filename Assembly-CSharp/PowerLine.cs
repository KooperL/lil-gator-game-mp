using System;
using UnityEngine;
using UnityEngine.U2D;

// Token: 0x02000292 RID: 658
public class PowerLine : MonoBehaviour
{
	// Token: 0x06000CE0 RID: 3296 RVA: 0x0004867C File Offset: 0x0004687C
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

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0000BFE6 File Offset: 0x0000A1E6
	private void Start()
	{
		this.mainCamera = Camera.main;
		this.pixelCamera = this.mainCamera.GetComponent<PixelPerfectCamera>();
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x00048734 File Offset: 0x00046934
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

	// Token: 0x06000CE3 RID: 3299 RVA: 0x0004879C File Offset: 0x0004699C
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

	// Token: 0x06000CE4 RID: 3300 RVA: 0x00048970 File Offset: 0x00046B70
	public float ScaleForWorldPosition(Vector3 position)
	{
		float num = (this.mainCamera.WorldToScreenPoint(position) - this.mainCamera.WorldToScreenPoint(position + this.mainCamera.transform.up / 1000f)).magnitude * 1000f;
		return (float)this.pixelCamera.pixelRatio / num;
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x000489D8 File Offset: 0x00046BD8
	public Vector3 GetPointPosition(Vector3 start, Vector3 end, float t, Vector3 sway)
	{
		Vector3 vector = Vector3.Lerp(start, end, t);
		float num = 2f * Mathf.Abs(t - 0.5f);
		vector.y += this.sag * (1f - num * num);
		vector += (1f - num) * sway;
		return vector;
	}

	// Token: 0x0400112C RID: 4396
	public Transform[] anchorPoints;

	// Token: 0x0400112D RID: 4397
	public LineRenderer[] lineRenderers;

	// Token: 0x0400112E RID: 4398
	private AnimationCurve[] widthCurves;

	// Token: 0x0400112F RID: 4399
	private bool toggleWidthCurve;

	// Token: 0x04001130 RID: 4400
	public PowerLine linkedPowerLine;

	// Token: 0x04001131 RID: 4401
	public int positionCount = 6;

	// Token: 0x04001132 RID: 4402
	public Material lineMaterial;

	// Token: 0x04001133 RID: 4403
	public float sag = -4f;

	// Token: 0x04001134 RID: 4404
	public float catA = 1f;

	// Token: 0x04001135 RID: 4405
	public float windMovement = 1f;

	// Token: 0x04001136 RID: 4406
	public float windSpeed = 1f;

	// Token: 0x04001137 RID: 4407
	public float[] seeds;

	// Token: 0x04001138 RID: 4408
	public bool pixelSpaceWidth = true;

	// Token: 0x04001139 RID: 4409
	public float pixelWidth = 1f;

	// Token: 0x0400113A RID: 4410
	public float worldWidth = 0.05f;

	// Token: 0x0400113B RID: 4411
	private Camera mainCamera;

	// Token: 0x0400113C RID: 4412
	private PixelPerfectCamera pixelCamera;

	// Token: 0x0400113D RID: 4413
	private Vector3[] positions;

	// Token: 0x0400113E RID: 4414
	private float time;

	// Token: 0x0400113F RID: 4415
	private Keyframe[] keys;

	// Token: 0x04001140 RID: 4416
	private AnimationCurve widthCurve;
}
