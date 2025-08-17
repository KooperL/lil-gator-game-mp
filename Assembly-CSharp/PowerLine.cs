using System;
using UnityEngine;
using UnityEngine.U2D;

public class PowerLine : MonoBehaviour
{
	// Token: 0x06000D2C RID: 3372 RVA: 0x0004A204 File Offset: 0x00048404
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

	// Token: 0x06000D2D RID: 3373 RVA: 0x0000C2EE File Offset: 0x0000A4EE
	private void Start()
	{
		this.mainCamera = Camera.main;
		this.pixelCamera = this.mainCamera.GetComponent<PixelPerfectCamera>();
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0004A2BC File Offset: 0x000484BC
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

	// Token: 0x06000D2F RID: 3375 RVA: 0x0004A324 File Offset: 0x00048524
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

	// Token: 0x06000D30 RID: 3376 RVA: 0x0004A4F8 File Offset: 0x000486F8
	public float ScaleForWorldPosition(Vector3 position)
	{
		float num = (this.mainCamera.WorldToScreenPoint(position) - this.mainCamera.WorldToScreenPoint(position + this.mainCamera.transform.up / 1000f)).magnitude * 1000f;
		return (float)this.pixelCamera.pixelRatio / num;
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0004A560 File Offset: 0x00048760
	public Vector3 GetPointPosition(Vector3 start, Vector3 end, float t, Vector3 sway)
	{
		Vector3 vector = Vector3.Lerp(start, end, t);
		float num = 2f * Mathf.Abs(t - 0.5f);
		vector.y += this.sag * (1f - num * num);
		vector += (1f - num) * sway;
		return vector;
	}

	public Transform[] anchorPoints;

	public LineRenderer[] lineRenderers;

	private AnimationCurve[] widthCurves;

	private bool toggleWidthCurve;

	public PowerLine linkedPowerLine;

	public int positionCount = 6;

	public Material lineMaterial;

	public float sag = -4f;

	public float catA = 1f;

	public float windMovement = 1f;

	public float windSpeed = 1f;

	public float[] seeds;

	public bool pixelSpaceWidth = true;

	public float pixelWidth = 1f;

	public float worldWidth = 0.05f;

	private Camera mainCamera;

	private PixelPerfectCamera pixelCamera;

	private Vector3[] positions;

	private float time;

	private Keyframe[] keys;

	private AnimationCurve widthCurve;
}
